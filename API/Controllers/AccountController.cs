using System.Security.Claims;
using API.DTOs;
using API.Services;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[EnableRateLimiting("FixedWindowPolicy")]
[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly TokenService _tokenService;
    private readonly RoleManager<IdentityRole> _roleManager;
    public AccountController(UserManager<AppUser> userManager, TokenService tokenService, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _roleManager = roleManager;
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost("AssignUserToRole")]
    public async Task<ActionResult<string>> AssignUserToRole(userAssignedToRoleDTO userAssignedToRoleDTO)
    {
        var user = await _userManager.FindByIdAsync(userAssignedToRoleDTO.Id.ToString());

        if(user is null) return BadRequest("User Not Found");

        var result = await _userManager.AddToRoleAsync(user, userAssignedToRoleDTO.Role);

        if(!result.Succeeded) return BadRequest("Error Adding User To Role");

        return Ok("User Assigned To Role Successfully");

    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
    {
        var user = await _userManager.FindByEmailAsync(loginDTO.Email);

        if(user is null) return Unauthorized();

        var result = await _userManager.CheckPasswordAsync(user, loginDTO.Password);

        var userRoles = await _userManager.GetRolesAsync(user);
        
        if(result) 
        {
            return CreateUserObject(user, userRoles);
        }

        return Unauthorized();
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
    {
        if(await _userManager.Users.AnyAsync(u => u.UserName == registerDTO.UserName))
        {
            return BadRequest("Username Is Already Taken");
        }

        if(await _userManager.Users.AnyAsync(u => u.Email == registerDTO.Email))
        {
            return BadRequest("Email Is Already Taken");
        }

        var user = new AppUser
        {
            CustomerName = registerDTO.CustomerName,
            Email = registerDTO.Email,
            UserName = registerDTO.UserName
        };

        var result = await _userManager.CreateAsync(user, registerDTO.Password);

        var userRoles = await _userManager.GetRolesAsync(user);

        if(result.Succeeded)
        {
            return CreateUserObject(user, userRoles);
        }
        else
        {
            return BadRequest(result.Errors);
        }
        
    }


    [Authorize(Policy = "RequireUserRole")]
    [HttpGet]
    public async Task<ActionResult<UserDTO>> GetCurrentUser()
    {
        var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

        var userRoles = await _userManager.GetRolesAsync(user);

        return CreateUserObject(user, userRoles);
    }

    private UserDTO CreateUserObject(AppUser user, IList<string> userRoles)
    {
        return new UserDTO
        {
            CustomerName = user.CustomerName,
            Token = _tokenService.CreateToken(user, userRoles),
            UserName = user.UserName
        };
    }
}

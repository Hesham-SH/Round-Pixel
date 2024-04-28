using API.DTOs;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[EnableRateLimiting("FixedWindowPolicy")]
[ApiController]
[Route("[controller]")]
public class RoleController : ControllerBase
{
    private readonly RoleManager<IdentityRole> _roleManager;
    public RoleController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var roles =  await _roleManager.Roles.Select(r => new RoleDTO { Id = r.Id, Name = r.Name }).ToListAsync();
        if (roles.Count > 0)
            return Ok(roles);

        return NotFound("No Roles Found");
    }

    [HttpPost("AddRole")]
    public async Task<ActionResult<string>> Add(string roleName)
    {
        if(string.IsNullOrEmpty(roleName))
            return BadRequest("Role Name Can't Be Empty ... Please Provide Role To Add !");
            
        var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
        if(result.Succeeded)
            return Ok(result.ToString());

        return BadRequest("Failed To Add Role To Database !");
    }
    
}

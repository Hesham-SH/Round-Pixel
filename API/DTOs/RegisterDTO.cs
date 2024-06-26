using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDTO
{
    [Required]
    public string CustomerName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    // [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z].{4,25}$)", ErrorMessage = "Password Must Be Complex !")]
    public string Password { get; set; }
    [Required]
    public string UserName { get; set; }
}

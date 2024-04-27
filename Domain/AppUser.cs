using Microsoft.AspNetCore.Identity;

namespace Domain;
public class AppUser : IdentityUser
{
    public string CustomerName { get; set; }
    public virtual ICollection<Order> Orders { get; set; }  
}

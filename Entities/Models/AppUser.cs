using Microsoft.AspNet.Identity.EntityFramework;

namespace Entities
{
    public class AppUser : IdentityUser  // IdentityUser has properties - Id, UserName, Claims, Email, PasswordHash, Roles, PhoneNumber, SecurityStamp
    {
    }
}

using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("AppUser")]
    public class AppUser : IdentityUser  // IdentityUser has properties - Id, UserName, Claims, Email, PasswordHash, Roles, PhoneNumber, SecurityStamp
    {
    }
}

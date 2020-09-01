// https://www.yogihosting.com/aspnet-core-identity-setup/#dbcontext


using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class AppUser : IdentityUser  // IdentityUser has properties - Id, UserName, Claims, Email, PasswordHash, Roles, PhoneNumber, SecurityStamp
    {
    }
}

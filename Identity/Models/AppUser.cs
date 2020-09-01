// https://www.yogihosting.com/aspnet-core-identity-setup/#dbcontext


using Microsoft.AspNetCore.Identity;
using System;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class AppUser : IdentityUser  // IdentityUser has properties - Id, UserName, Claims, Email, PasswordHash, Roles, PhoneNumber, SecurityStamp
    {
        private void ApplySort(ref IQueryable<AppUser> users, string orderByQueryString)
        {
            if (!users.Any())
                return;

            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                users = users.OrderBy(x => x.UserName);
                return;
            }

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(AppUser).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {sortingOrder}, ");
            }

            string orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                users = users.OrderBy(x => x.UserName);
                return;
            }

            users = users.OrderBy(orderQuery);
        }
    }
}

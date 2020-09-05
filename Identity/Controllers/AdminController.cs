using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Data;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

/*
 * https://www.yogihosting.com/aspnet-core-identity-roles/#rolemanager
 * 
 * 
Users are managed through the UserManager<T> class, where T is the class chosen to represent users in the database.

The below table describes the most useful UserManager<T> class members.

Name	Description
Users                   	    This property returns a sequence containing the users stored in the Identity database.
FindByIdAsync(id)	            This method queries the database for the user object with the specified ID.
CreateAsync(user, password)   	This method stores a new user in the database using the specified password.
UpdateAsync(user)               This method modifies an existing user in the Identity database.
DeleteAsync(user)	            This method removes the specified user from the Identity database.
AddToRoleAsync(user, name)	    Adds a user to a role
RemoveFromRoleAsync(user, name)	Removes a user from a role
GetRolesAsync(user)	            Gives the names of the roles in which the user is a member
IsInRoleAsync(user, name)	    Returns true is the user is a member of a specified role, else returns false 
*/


namespace Identity.Controllers
{
    public class AdminController : Controller
    {

        private UserManager<AppUser> userManager;
        private IPasswordHasher<AppUser> passwordHasher;
        private IServiceProvider _provider = null;

        public AdminController(UserManager<AppUser> usrMgr, IServiceProvider provider, IPasswordHasher<AppUser> passwordHash)
        {
            userManager = usrMgr;
            passwordHasher = passwordHash;




            // Test
            _provider = provider;
            CreateRoles(_provider);





        }

        //public IActionResult Index()   // https://localhost:44327/Admin
        //{
        //    /*
        //     * All the Users accounts of Identity can be fetched from the Identity database by the use of Users property of the UserManager class. 
        //     * The Users property will return an IEnumerable object.
        //     */

        //    return View(userManager.Users);
        //}
        public async Task<IActionResult> Index(
            string sortOrder, 
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["IdSortParm"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["EmailSortParm"] = sortOrder == "Email" ? "email_desc" : "Email";

            if (!string.IsNullOrEmpty(searchString))
                pageNumber = 1;
            else
                searchString = currentFilter;


            ViewData["CurrentFilter"] = searchString;

            var users = from s in userManager.Users
                           select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(x => x.UserName.Contains(searchString) ||
                                    x.Email.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "id_desc":
                    users = users.OrderByDescending(s => s.Id);
                    break;
                case "name_desc":
                    users = users.OrderByDescending(s => s.UserName);
                    break;
                case "Email":
                    users = users.OrderBy(s => s.Email);
                    break;
                case "email_desc":
                    users = users.OrderByDescending(s => s.Email);
                    break;
                default:
                    users = users.OrderBy(s => s.UserName);
                    break;
            }

            int pageSize = 2;


            //return Ok(await PaginatedList<AppUser>.CreateAsync(users.AsNoTracking(), pageNumber ?? 1, pageSize));
            return View(await PaginatedList<AppUser>.CreateAsync(users.AsNoTracking(), pageNumber ?? 1, pageSize));
            //return View(await users.AsNoTracking().ToListAsync());
        }

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(User user)   // https://localhost:44327/Admin/Create
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    UserName = user.Name,
                    Email = user.Email
                };

                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);

                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }

            return View(user);
        }

        public async Task<IActionResult> Update(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, string email, string password)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(email))
                    user.Email = email;
                else
                    ModelState.AddModelError("", "Email cannot be empty");

                if (!string.IsNullOrEmpty(password))
                    user.PasswordHash = passwordHasher.HashPassword(user, password);
                else
                    ModelState.AddModelError("", "Password cannot be empty");

                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        Errors(result);
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View("Index", userManager.Users);
        }


        // https://stackoverflow.com/questions/42471866/how-to-create-roles-in-asp-net-core-and-assign-them-to-users
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles 
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            var PersonManager = serviceProvider.GetRequiredService<IPersonRepo>();



            var allEmployees = await PersonManager.GetAllAsync();
            var employeesFor = await PersonManager.GetByManagerIdAsync("6cc0fbb4-2637-4ea5-8898-a9b259ef48bf");

            //string[] roleNames = { "Admin", "Manager", "Member" };
            //IdentityResult roleResult;

            //foreach (var roleName in roleNames)
            //{
            //    var roleExist = await RoleManager.RoleExistsAsync(roleName);
            //    if (!roleExist)
            //    {
            //        //create the roles and seed them to the database: Question 1
            //        roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
            //    }
            //}

            //Here you could create a super user who will maintain the web app
            //var poweruser = new AppUser
            //{

            //    UserName = Configuration["AppSettings:UserName"],
            //    Email = Configuration["AppSettings:UserEmail"],
            //};
            ////Ensure you have these values in your appsettings.json file
            //string userPWD = Configuration["AppSettings:UserPassword"];
            //var _user = await UserManager.FindByEmailAsync(Configuration["AppSettings:AdminUserEmail"]);

            //if (_user == null)
            //{
            //    var createPowerUser = await UserManager.CreateAsync(poweruser, userPWD);
            //    if (createPowerUser.Succeeded)
            //    {
            //        //here we tie the new user to the role
            //        await UserManager.AddToRoleAsync(poweruser, "Admin");

            //    }
            //}
        }



        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}

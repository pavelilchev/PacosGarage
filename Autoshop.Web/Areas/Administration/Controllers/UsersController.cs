namespace Autoshop.Web.Areas.Admin.Controllers
{
    using AutoMapper.QueryableExtensions;
    using Autoshop.Models;
    using Autoshop.Web.Areas.Administration.Controllers;
    using Autoshop.Web.Areas.Administration.Models.Users;
    using Autoshop.Web.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    using static Autoshop.Common.Constants.CommonConstants;

    public class UsersController : BaseAdminController
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UsersController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Users()
        {
            var users = userManager
                .Users
                .ProjectTo<UserListingViewModel>()
                .ToList();

            return View(users);
        }

        public async Task<IActionResult> UserDetails(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest();
            }

            var userRoles = await userManager.GetRolesAsync(user);

            var roles = await this.roleManager
            .Roles
            .Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name
            })
            .ToListAsync();

            return View(new UserDetailsViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserRoles = userRoles,
                Roles = roles
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(AddUserToRoleFormModel model)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(model.Role);
            var user = await this.userManager.FindByIdAsync(model.UserId);
            var userExists = user != null;

            if (!roleExists || !userExists || !ModelState.IsValid)
            {
                TempData.AddErrorMessage($"Invalid identity details.");

                return RedirectToAction(nameof(UserDetails), "Users", new { area = Administration, id = model.UserId });
            }

            await this.userManager.AddToRoleAsync(user, model.Role);

            TempData.AddSuccessMessage($"User {user.UserName} successfully added to the {model.Role} role.");

            return RedirectToAction(nameof(UserDetails), "Users", new { area = Administration, id = model.UserId });
        }
    }
}

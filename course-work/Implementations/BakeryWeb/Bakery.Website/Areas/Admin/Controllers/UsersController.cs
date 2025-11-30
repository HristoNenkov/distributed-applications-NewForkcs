using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Bakery.Website.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<IdentityUser> userManager,
                               RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();

            var model = new List<UserWithRolesViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                model.Add(new UserWithRolesViewModel
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Roles = roles
                });
            }

            return View(model);
        }


        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var model = new UserRolesViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                Roles = new List<RoleCheckbox>()
            };

            var allRoles = await _roleManager.Roles.ToListAsync();

            foreach (var role in allRoles)
            {
                var isInRole = await _userManager.IsInRoleAsync(user, role.Name);
                model.Roles.Add(new RoleCheckbox
                {
                    RoleName = role.Name,
                    IsSelected = isInRole
                });
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            foreach (var role in model.Roles)
            {
                if (role.IsSelected)
                    await _userManager.AddToRoleAsync(user, role.RoleName);
                else
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);
            }

            return RedirectToAction("Index");
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UserManangementWithIdentity.Models;
using UserManangementWithIdentity.ViewModels;

namespace UserManangementWithIdentity.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.Select(user => new UserViewModel
            {
                Id = user.Id,
                FirstNmae = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            }).ToListAsync();

            return View(users);
        }

        public async Task<IActionResult> Add()
        {
            var roles = await _roleManager.Roles.Select(r => new RoleViewModel
            {
                RoleId = r.Id,
                RoleName = r.Name,
            }).ToListAsync();
            var viewmodel = new AddUserViewModel
            {
                Roles = roles
            };
            return View(viewmodel);
        }

        public async Task<IActionResult> Add(AddUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if(!model.Roles.Any(r => r.IsSelected))
            {
                ModelState.AddModelError("Roles", "Please choose at least one role");
                return View(model);
            }
            if(await _userManager.FindByEmailAsync(model.Email) != null)
            {
                ModelState.AddModelError("Email", "Email is already Exist");
                return View(model);
            }
            if (await _userManager.FindByEmailAsync(model.UserName) != null)
            {
                ModelState.AddModelError("UserName", "UserName is already Exist");
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
         var result=await _userManager.CreateAsync(user,model.Password);
            if (!result.Succeeded)
            {
                foreach (var Error in result.Errors)
                {
                    ModelState.AddModelError("Roles", Error.Description);
                }
            }
            await _userManager.AddToRolesAsync(user, model.Roles.Where(r => r.IsSelected).Select(r => r.RoleName));
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ManageRoles(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            var roles = await _roleManager.Roles.ToListAsync();
            var viewmodel = new UserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = roles.Select(role => new RoleViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, role.Name).Result
                }).ToList()
            };
            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(UserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return NotFound();
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in model.Roles)
            {
                if (userRoles.Any(r => r == role.RoleName) && !role.IsSelected)
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);
                if (!userRoles.Any(r => r == role.RoleName) && role.IsSelected)
                    await _userManager.AddToRoleAsync(user, role.RoleName);
            }
            return RedirectToAction(nameof(Index));

        }

    }
}

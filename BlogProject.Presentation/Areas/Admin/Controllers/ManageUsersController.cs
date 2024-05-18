using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManageUsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ManageUsersController(UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index(int pageId = 1, 
            string email = null, string userName = null)
        {
            var usersList = _userManager.Users.Select(u => new ShowUserListViewModel()
            {
                Email = u.Email,
                UserId = u.Id,
                UserName = u.UserName
            }).ToList();



            if (userName != null)
            {
                usersList = usersList.Where(u=>u.UserName == userName).ToList();
            }
            else if (email != null)
            {
                usersList = usersList.Where(u=>u.Email == email).ToList();
            }

            var data = CommonMethods<ShowUserListViewModel>.CalculatePagingNumber(usersList, out Pager pager, 10, pageId);

            ViewBag.UserName = userName;
            ViewBag.Email = email;
            ViewBag.Pager = pager;

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            EditUserViewModel editUser = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName
            };
            return View(editUser);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel editUser)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(editUser.Id))
                    return NotFound();

                var user = await _userManager.FindByIdAsync(editUser.Id);
                if (user == null)
                    return NotFound();
                user.UserName = editUser.UserName;
                user.AccountDisabled = editUser.AccountDisabled;
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                    return RedirectToAction("Index");

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(editUser);
        }

        [HttpGet]
        public async Task<IActionResult> AddUserToRole(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var roles = await _roleManager.Roles.AsTracking()
                .Select(r => r.Name).ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);
            var validRoles = roles.Where(r => !userRoles.Contains(r))
                .Select(r => new UserRolesViewModel(r)).ToList();
            var model = new AddUserToRoleViewModel(id, validRoles);

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddUserToRole(AddUserToRoleViewModel model)
        {
            if (model == null)
                return NotFound();

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return NotFound();

            var requestRoles = model.UserRoles.Where(r => r.IsSelected)
                .Select(u => u.RoleName)
                .ToList();

            var result = await _userManager.AddToRolesAsync(user, requestRoles);

            if (result.Succeeded)
                return RedirectToAction("Index");

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveUserFromRole(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);
            var validRoles = userRoles.Select(r => new UserRolesViewModel(r)).ToList();
            var model = new AddUserToRoleViewModel(id, validRoles);

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> RemoveUserFromRole(AddUserToRoleViewModel model)
        {
            if (model == null)
                return NotFound();

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return NotFound();

            var requestRoles = model.UserRoles.Where(r=>r.IsSelected)
                .Select(u=>u.RoleName)
                .ToList();

            var result = await _userManager.RemoveFromRolesAsync(user, requestRoles);

            if(result.Succeeded)
                return RedirectToAction("Index");

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            if(id == null)
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);

            if(user == null)
                return NotFound();

            return PartialView(user);
        }

        [HttpPost,ActionName("DeleteUser")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ConfirmDeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            await _userManager.DeleteAsync(user);

            return RedirectToAction("Index");
        }
    }
}

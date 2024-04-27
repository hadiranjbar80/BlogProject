using BlogProject.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManageRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public ManageRolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _roleManager.Roles.ToListAsync());
        }

        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddRole(AddOrEditRoleViewModel addOrEdit)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole(addOrEdit.Name);
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(role);
            }

            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteRole(string id)
        {
            if(string.IsNullOrEmpty(id)) return NotFound();
            var role = await _roleManager.FindByIdAsync(id);
            if(role == null) return NotFound();
            await _roleManager.DeleteAsync(role);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditRole(string id)
        {
            if(string.IsNullOrEmpty(id)) return NotFound();

            var role = await _roleManager.FindByIdAsync(id);

            if(role == null) return NotFound();

            AddOrEditRoleViewModel model = new()
            {
                Name = role.Name
            };

            return View(model);
        }

        [HttpPost, ActionName("EditRole")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> EditRole(AddOrEditRoleViewModel addOrEdit, string id)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role == null) return NotFound();
                role.Name = addOrEdit.Name;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(role);
            }
            return View(addOrEdit);
        }
    }
}

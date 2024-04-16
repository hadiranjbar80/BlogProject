using BlogProject.Domain.Models;
using BlogProject.Domain.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogProject.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        { 
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost("Register")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if(ModelState.IsValid)
            {
                if(_signInManager.IsSignedIn(User))
                    return RedirectToAction("Index", "Home");

                var user = new ApplicationUser()
                {
                    UserName = register.UserName,
                    Email = register.Email,
                    EmailConfirmed = true,
                };

                var registerResult = await _userManager.CreateAsync(user,register.Password);

                if(registerResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in registerResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View();
        }

        [HttpGet("Login")]
        public IActionResult Login(string returnUrl = null)
        {
            if(_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost("Login")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                if(_signInManager.IsSignedIn(User))
                    return RedirectToAction("Index", "Home");

                ViewBag.ReturnUrl = returnUrl;

                var result = await _signInManager.PasswordSignInAsync
                    (login.UserName, login.Password, login.RememberMe, true);

                var user = await _userManager.FindByNameAsync(login.UserName);
                if (result.Succeeded)
                {
                    if(returnUrl != null && Url.IsLocalUrl(returnUrl))
                    {
                        var userClaim = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Name, user.UserName)
                        };
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }

                if(result.IsLockedOut)
                {
                    ViewBag.Message = "اکانت شما به دلیل پنج بار ورود نا‌موفق به مدت پنج دقیقه قفل شده است";
                }

                ModelState.AddModelError("", "نام کاربری یا کلمه عبور اشتباه است");
            }
            
            return View(login);
        }

        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}

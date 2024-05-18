using BlogProject.Domain.Models;
using BlogProject.Domain.ViewModels;
using BlogProject.Service.EmailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogProject.Presentation.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailSender, IConfiguration configuration)
            : base(configuration)
        {
            _emailService = emailSender;
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
            if (ModelState.IsValid)
            {
                if (_signInManager.IsSignedIn(User))
                    return RedirectToAction("Index", "Home");

                var user = new ApplicationUser()
                {
                    UserName = register.UserName,
                    Email = register.Email,
                    EmailConfirmed = false,
                };

                var registerResult = await _userManager.CreateAsync(user, register.Password);

                if (registerResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");

                    #region Send confirmation rmail

                    // Generate email confirmation token
                    var userToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account",
                        new { token = userToken, email = user.Email }, Request.Scheme);

                    var emailBody = CommonMethods<string>.ReadEmailTemplate(confirmationLink, user.UserName, "EmailActivation.html");

                    await _emailService.SendEmailAsync(user.Email, "فعال سازی ایمیل", emailBody);

                    #endregion
                    Notify("ایمیل فعال سازی برای شما ارسال شد", "ثبت نام", NotificationType.success);
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
            if (_signInManager.IsSignedIn(User))
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
                if (_signInManager.IsSignedIn(User))
                    return RedirectToAction("Index", "Home");

                ViewBag.ReturnUrl = returnUrl;

                var user = await _userManager.FindByNameAsync(login.UserName);

                if (user == null)
                {
                    Notify("کاربری با مشخصات وارد شده یافت نشد", "خطا", NotificationType.error);
                    return RedirectToAction("Login", "Account");
                }

                if (user.AccountDisabled)
                {
                    Notify("حساب کاربری شما غیرفعال است","خطا",NotificationType.error);
                    return RedirectToAction("Login", "Account");
                }

                if (await _userManager.IsEmailConfirmedAsync(user))
                {
                    var result = await _signInManager.PasswordSignInAsync
                        (login.UserName, login.Password, login.RememberMe, true);

                    if (result.Succeeded)
                    {
                        if (returnUrl != null && Url.IsLocalUrl(returnUrl))
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

                    if (result.IsLockedOut)
                    {
                        ViewBag.Message = "اکانت شما به دلیل پنج بار ورود نا‌موفق به مدت پنج دقیقه قفل شده است";
                    }

                    ModelState.AddModelError("", "نام کاربری یا کلمه عبور اشتباه است");
                }
                else
                {
                    ModelState.AddModelError("", "ایمیل شما تایید نشده است");
                }
            }

            return View(login);
        }

        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound();

            if (token == null)
                return NotFound();

            var res = await _userManager.ConfirmEmailAsync(user, token);
            await _userManager.UpdateSecurityStampAsync(user);
            Notify("حساب شما با موفقیت فعال شد", "فعال سازی", NotificationType.success);
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPassword)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgotPassword.Email);
                if (await _userManager.IsEmailConfirmedAsync(user))
                {
                    if (user == null)
                    {
                        Notify("ایمیل وارد شده معتبر نمی‌باشد", "خطا", NotificationType.error);
                        return RedirectToAction("ForgotPassword", "Account");
                    }

                    var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetPasswordUrl = Url.Action("ResetPassword", "Account",
                        new { email = user.Email, token = resetPasswordToken }, Request.Scheme);

                    var emailBody = CommonMethods<string>.ReadEmailTemplate(resetPasswordUrl, user.UserName, "ForgotPassword.html");

                    await _emailService.SendEmailAsync(user.Email, "لینک بازیابی کلمه‌عبور", emailBody);

                    Notify("یک ایمیل حاوی لینک بازیابی برای شما ارسال شد", "بازیابی", NotificationType.success);
                    return View("Login");
                }
                ModelState.AddModelError("", "حساب کاربری شما غیر فعال است");

            }

            return View(forgotPassword);
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new ResetPasswordViewModel
            {
                Email = email,
                Token = token
            };

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPassword)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(resetPassword.Email);
                if (user == null)
                {
                    Notify("ایمیل وارد شده معتبر نمی‌باشد", "خطا", NotificationType.error);
                    return RedirectToAction("Login");
                }

                var result = await _userManager.
                    ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);

                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    Notify("کلمه‌عبور با موفقیت بازیابی شد", "", NotificationType.success);
                    return RedirectToAction("Login");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(resetPassword);
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Presentation.Controllers
{
    public class NewsLetterController : BaseController
    {
        private readonly INewsLetterService _newsletterService;
        private readonly IUnitOfWork _unitOfWork;
        public NewsLetterController(INewsLetterService newsletterService,
            IUnitOfWork unitOfWork, IConfiguration configuration)
            : base(configuration)
        {
            _newsletterService = newsletterService;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> AddToNewsLetter(NewsLetter newsLetter)
        {
            if (ModelState.IsValid)
            {
                var email = _newsletterService.GetByEmailAsync(newsLetter.Email);
                if (email.Result == null)
                {
                    await _newsletterService.AddToNewsLetterAsync(newsLetter);
                    await _unitOfWork.Commit();
                    Notify("عضویت شما ثبت شد", "عضویت در خبرنامه", NotificationType.success);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Notify("این ایمیل قبلا ثبت شده است", "خطا", NotificationType.error);
                    return RedirectToAction("Index", "Home");
                }
            }
            Notify("لطفا ایمیل خود را وارد کنید", "عضویت در خبرنامه", NotificationType.error);
            return View();
        }
    }
}

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Presentation.Controllers
{
    public class ErrorController : Controller
    {

        public ErrorController()
        {
        }

        [Route("Error/{statusCode}")]
        public IActionResult PageNotFound(int statusCode)
        {
            ViewBag.Title = "صفحه مورد نظر یافت نشد";
            ViewBag.ErrorMessage = "صفحه مورد نظر یافت نشد";
            return View("Error");
        }

        [Route("/Error")]
        public IActionResult ExceptionError()
        {
            ViewBag.Title = "خطای ناشناخته‌ای رخ‌داد";
            ViewBag.ErrorMessage = "لطفا این خطا را به مدیر سایت گزارش دهید";
            return View("Error");
        }

    }
}

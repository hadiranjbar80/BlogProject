using BlogProject.Domain.Models;
using BlogProject.Service.EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Presentation.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        public HomeController(ICommentService commentService, IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _emailService = emailService;
            _commentService = commentService;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ConfirmComment(int commentId)
        {
            var comment = await _commentService.GetCommentByIdAsync(commentId);
            comment.IsConfirmed = true;
            await _commentService.UpdateCommentAsync(comment);
            await _unitOfWork.Commit();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var comment = await _commentService.GetCommentByIdAsync(commentId);
            if (comment == null)
                return NotFound();
            await _commentService.DeleteCommentAsync(comment);
            await _unitOfWork.Commit();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmAllComments()
        {
            await _commentService.ConfirmAllComments();
            await _unitOfWork.Commit();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteNotConfirmedComments()
        {
            await _commentService.DeleteNotConfirmedComments();
            await _unitOfWork.Commit();
            return RedirectToAction(nameof(Index));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogProject.Presentation.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IUnitOfWork _unitOfWork;
        public CommentController(ICommentService commentService, IUnitOfWork unitOfWork)
        {
            _commentService = commentService;
            _unitOfWork = unitOfWork;

        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentViewModel comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("لطفا متن نظر را وارد کنید");
            }
            try
            {
                var userName = User.FindFirstValue(ClaimTypes.Name).ToString();
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
                if (userName != null || userId == null)
                {
                    var newComment = new ArticleComment()
                    {
                        ArticleId = comment.ArticleId,
                        DateCreated = DateTime.Now,
                        CommentText = comment.CommentText,
                        UserId = userId,
                        UserName = userName,
                        IsConfirmed = false,
                        ParentId = comment.ParentId,
                    };
                    await _commentService.CreateCommentAsync(newComment);
                    await _unitOfWork.Commit();
                }
                else
                {
                    return Json(".لطفا برای ثبت کامنت ابتدا وارد سایت شوید");
                }
                return Ok(".کامنت شما با موفقیت ثبت شد و پس از تایید نمایش داده می‌شود");
            }
            catch
            {
                return BadRequest("مشکلی در ثبت نظر پیش آمده است");
            }
        }
    }
}

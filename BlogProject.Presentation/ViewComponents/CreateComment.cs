

namespace BlogProject.Presentation.ViewComponents
{
    public class CreateComment : ViewComponent
    {
        private readonly ICommentService _commentService;

        public CreateComment(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int articleId)
        {
            return View("/Views/ViewComponents/CreateComment.cshtml", new CreateCommentViewModel { ArticleId = articleId });
        }
    }
}

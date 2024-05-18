namespace BlogProject.Presentation.ViewComponents
{
    public class ShowComments : ViewComponent
    {
        private readonly ICommentService _commentService;

        public ShowComments(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int articleId)
        {
            return View("/Views/ViewComponents/ShowComments.cshtml", await _commentService.GetCommentByArticleId(articleId));
        }
    }
}

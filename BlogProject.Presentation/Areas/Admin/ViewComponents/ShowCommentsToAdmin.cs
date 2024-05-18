namespace BlogProject.Presentation.Areas.Admin.ViewComponents
{
    public class ShowCommentsToAdmin : ViewComponent
    {
        private readonly ICommentService _commentService;
        public ShowCommentsToAdmin(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("/Areas/Admin/Views/ViewComponents/ShowCommentsToAdmin.cshtml", 
                await _commentService.GetCommentsAsync());
        }
    }
}

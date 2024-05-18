namespace BlogProject.Presentation.ViewComponents
{
    public class NewsLetter : ViewComponent
    {
        public NewsLetter()
        {
            
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("/Views/ViewComponents/NewsLetter.cshtml", new Domain.Models.NewsLetter());
        }
    }
}

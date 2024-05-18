namespace BlogProject.Presentation.ViewComponents
{
    public class ConfirmEmail : ViewComponent
    {
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("/Views/ViewComponents/ConfirmEmail.cshtml");
        }
    }
}

using BlogProject.Presentation.ViewComponents;

namespace BlogProject.Presentation.Areas.Admin.ViewComponents
{
    public class ShowLastQuestions: ViewComponent
    {
        private readonly IQuestionService _questionService;

        public ShowLastQuestions(IQuestionService questionService)
        {
            _questionService = questionService;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var questions = await _questionService.GetLastFiveQuestionsAsync();

            List<ShowLastQuestionsViewModel> lastQuestions = new();

            foreach (var question in questions)
            {
                lastQuestions.Add(new ShowLastQuestionsViewModel
                {
                    DateCreated = CommonMethods<DateTime>.GetPersianDate(question.DateCreated),
                    Id = question.Id,
                    Title = question.Title,
                    UserName = question.ApplicationUser.UserName
                });
            }

            return View("/Areas/Admin/Views/ViewComponents/ShowLastQuestions.cshtml", lastQuestions);
        }
    }
}

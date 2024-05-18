using BlogProject.Domain.Models;

namespace BlogProject.Presentation.ViewComponents
{
    public class ShowAnswers : ViewComponent
    {
        private readonly IAnswerService _answerService;
        public ShowAnswers(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int questionId)
        {
            var answers = await _answerService.GetAnswerByQuestionIdAsync(questionId);
            List<ShowAnswersViewModel> showAnswers = new();
            foreach (var answer in answers)
            {
                showAnswers.Add(new ShowAnswersViewModel
                {
                    DateCreated = CommonMethods<DateTime>.GetPersianDate(answer.DateCreated),
                    Description = answer.Description,
                    UserName = answer.ApplicationUser.UserName
                });
            }

            return View("/Views/ViewComponents/ShowAnswers.cshtml", showAnswers);
        }
    }
}

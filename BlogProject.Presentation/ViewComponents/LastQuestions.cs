
using BlogProject.Domain.Models;
using System.Globalization;

namespace BlogProject.Presentation.ViewComponents
{
    public class LastQuestions : ViewComponent
    {
        private readonly IQuestionService _questionService;

        public LastQuestions(IQuestionService questionService)
        {
            _questionService = questionService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var questions = await _questionService.GetLastCreatedQuestionsAsync();

            List<ShowLastQuestionsViewModel> showQuestions = new();

            foreach (var question in questions)
            {
                showQuestions.Add(new ShowLastQuestionsViewModel
                {
                    DateCreated = CommonMethods<DateTime>.GetPersianDate(question.DateCreated),
                    Id = question.Id,
                    Title = question.Title,
                });
            }

            return View("/Views/ViewComponents/LastQuestions.cshtml", showQuestions);
        }
    }
}

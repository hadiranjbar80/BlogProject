using BlogProject.Service.QuestionService;
using Microsoft.AspNetCore.Mvc;

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
            return View("/Views/ViewComponents/LastQuestions.cshtml",await _questionService.GetLastCreatedQuestionsAsync());
        }
    }
}

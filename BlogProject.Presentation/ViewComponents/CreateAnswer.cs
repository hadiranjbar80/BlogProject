namespace BlogProject.Presentation.ViewComponents
{
    public class CreateAnswer : ViewComponent
    {
        private readonly IQuestionService _questionService;
        public CreateAnswer(IQuestionService questionService)
        {
            _questionService = questionService;
        }


        public async Task<IViewComponentResult> InvokeAsync(string userId, int questionId)
        {
            var question = await _questionService.GetQuestionByIdAsync(questionId);


            return View("/Views/ViewComponents/CreateAnswer.cshtml", new CreateAnswerViewModel
            {
                QuestionId = questionId,
                UserId = userId,
                Title = question.Title,
                UserEmail = question.ApplicationUser.Email
            });
        }
    }
}

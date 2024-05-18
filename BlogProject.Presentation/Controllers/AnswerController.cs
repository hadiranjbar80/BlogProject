using BlogProject.Domain.Models;
using BlogProject.Service.EmailService;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Presentation.Controllers
{
    public class AnswerController : BaseController
    {
        private readonly IAnswerService _answerService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        public AnswerController(IAnswerService answerService,
            IUnitOfWork unitOfWork, IConfiguration configuration
            ,IEmailService emailService)
            : base(configuration)
        {
            _answerService = answerService;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateAnswer(CreateAnswerViewModel createAnswer)
        {
            if (ModelState.IsValid)
            {
                await _answerService.CreateNewAnswerAsync(new Answer
                {
                    DateCreated = DateTime.Now,
                    Description = createAnswer.Description,
                    QuestionId = createAnswer.QuestionId,
                    UserId = createAnswer.UserId,
                });
                await _unitOfWork.Commit();

                var emailBody = CommonMethods<string>.ReadEmailTemplate("","","AnswerNotification.html");

                await _emailService.SendEmailAsync(createAnswer.UserEmail, "پاسخ سوال", emailBody);
                Notify("پاسخ شما با موفقیت ثبت شد", "ثبت پاسخ", NotificationType.success);
                return RedirectToAction("ShowQuestionDetail", "Question", new { quesId = createAnswer.QuestionId });
            }

            Notify("لطفا متن پاسخ را وارد کنید", "خطا", NotificationType.error);
            return RedirectToAction("ShowQuestionDetail", "Question", new { quesId = createAnswer.QuestionId });
        }
    }
}

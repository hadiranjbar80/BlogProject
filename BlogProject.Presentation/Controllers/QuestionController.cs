using BlogProject.Domain.Models;
using BlogProject.Domain.ViewModels;
using BlogProject.Repository;
using BlogProject.Service.ArticleService;
using BlogProject.Service.QuestionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogProject.Presentation.Controllers
{
    //[Authorize("User")]
    public class QuestionController : BaseController
    {
        private readonly IQuestionService _questionService;
        private readonly IArticleService _articleService;
        private readonly IUnitOfWork _unitOfWork;
        public QuestionController(IQuestionService questionService, 
            IUnitOfWork unitOfWork, IArticleService articleService, IConfiguration configuration)
            :base(configuration)
        {
            _questionService = questionService;
            _articleService = articleService;
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public async Task<IActionResult> CreateQuestion(int articleId)
        {
            if(articleId == null) 
                return NotFound();

            if (!await _articleService.DoesArticleExistByIdAsync(articleId))
                return NotFound();
            

            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateQuestion(int articleId, CreateQuestionViewModel createQuestion)
        {
            if(ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
                Question question = new()
                {
                    Description = createQuestion.Description,
                    Title = createQuestion.Title,
                    DateCreated = DateTime.Now,
                    ArticleId = articleId,
                    UserId = userId
                };

                await _questionService.CreateNewQuestionAsync(question);
                await _unitOfWork.Commit();
                Notify("سوال شما با موفقیت ثبت گردید", "پرسش سوال", NotificationType.success);
                return RedirectToAction("ArticleQuestions", "Article", new { articleId = articleId });
            }

            return View(createQuestion);
        }
    }
}

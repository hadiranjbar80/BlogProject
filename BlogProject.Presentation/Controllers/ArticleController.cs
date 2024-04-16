using BlogProject.Domain.Models;
using BlogProject.Domain.ViewModels;
using BlogProject.Service.ArticleService;
using BlogProject.Service.QuestionService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Presentation.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IQuestionService _questionService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ArticleController(IArticleService articleService,
            IQuestionService questionService, UserManager<ApplicationUser> userManager)
        {
            _articleService = articleService;
            _questionService = questionService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> ShowArticleDetail(int articleId)
        {
            if (articleId == null)
            {
                return NotFound();
            }

            var article = await _articleService.GetArticleByIdAsync(articleId);

            if (article == null)
            {
                return NotFound();
            }

            ShowArticleDetailViewModel detail = new()
            {
                DateCreated = article.DateCreated,
                Description = article.Description,
                Id = articleId,
                Tags = article.Tags,
                Title = article.Title,
                ImageName = article.ImageName,
            };

            return View(detail);
        }

        [HttpGet]
        public async Task<IActionResult> ArticleQuestions(int articleId)
        {
            if (articleId == null)
            {
                return NotFound();
            }

            if (!await _articleService.DoesArticleExistByIdAsync(articleId))
            {
                return NotFound();
            }

            ViewBag.ArticleId = articleId;

            var articleQuestions = await _questionService.GetQuestionsByArticleIdAsync(articleId);

            List<ShowQuestionsList> questionsList = new();

            foreach (var question in articleQuestions)
            {
                var user = await _userManager.FindByIdAsync(question.UserId);
                questionsList.Add(new ShowQuestionsList()
                {
                    Title = question.Title,
                    DateCreated = question.DateCreated,
                    Id = question.Id,
                    UserName = user.UserName
                });
            }

            return View(questionsList);
        }
    }
}

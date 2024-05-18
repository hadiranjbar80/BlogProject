using BlogProject.Domain.Models;
using BlogProject.Domain.ViewModels;
using BlogProject.Presentation.Utilities;
using BlogProject.Service.ArticleService;
using BlogProject.Service.QuestionService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

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

        [HttpGet("/ShowArticle/{articleId}")]
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

            #region Generate Persian Date
            PersianCalendar pc = new PersianCalendar();
            int month = pc.GetMonth(DateTime.Now) - 1;
            string monthName = ((PersianMonth)month).ToString();
            string dayOfTheWeek = CommonMethods<DateTime>.GetPersianDayOfTheWeek(article.DateCreated);
            string persianDate = pc.GetYear(article.DateCreated) + " " + dayOfTheWeek + " " + pc.GetDayOfMonth(article.DateCreated) + " " + monthName;
            #endregion

            ShowArticleDetailViewModel detail = new()
            {
                DateCreated = persianDate,
                Description = article.Description,
                Id = articleId,
                Tags = article.Tags.Split('،'),
                Title = article.Title,
                ImageName = article.ImageName,
                Comments = article.ArticleComments,
            };

            ViewData["ArticleQuestionsLink"] = $"/Questions/{article.Id}"; 

            return View(detail);
        }

        [HttpGet("/Questions/{articleId}")]
        public async Task<IActionResult> ArticleQuestions(int articleId, int pageId = 1, string questionTitle = null)
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

            ViewBag.ArticleTitle = article.Title;
            ViewBag.ArticleId = articleId;
            ViewBag.QuestionTitle = questionTitle;

            var articleQuestions = await _questionService.GetQuestionsByArticleIdAsync(articleId);

            List<ShowQuestionsListViewModel> questionsList = new();

            foreach (var question in articleQuestions)
            {
                var user = await _userManager.FindByIdAsync(question.UserId);
                questionsList.Add(new ShowQuestionsListViewModel()
                {
                    Title = question.Title,
                    DateCreated = CommonMethods<DateTime>.GetPersianDate(question.DateCreated),
                    Id = question.Id,
                    UserName = user.UserName,
                    AnswerCount = question.Answers.Count,
                });
            }

            if(questionTitle != null)
            {
                questionsList = questionsList.Where(q => q.Title.ToLower().Contains(questionTitle.ToLower())).ToList();
            }

            // paging
            var data = CommonMethods<ShowQuestionsListViewModel>.CalculatePagingNumber(questionsList, out Pager pager, 10, pageId);

            ViewBag.Pager = pager;

            return View(data);
        }
    }
}

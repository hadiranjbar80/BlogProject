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
    [Authorize]
    public class QuestionController : BaseController
    {
        private readonly IQuestionService _questionService;
        private readonly IArticleService _articleService;
        private readonly IUnitOfWork _unitOfWork;
        public QuestionController(IQuestionService questionService,
            IUnitOfWork unitOfWork, IArticleService articleService, IConfiguration configuration)
            : base(configuration)
        {
            _questionService = questionService;
            _articleService = articleService;
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// This method shows all questions that is in the database
        /// </summary>
        /// <param name="pageId">PageId</param>
        /// <param name="questionTitle">It is defined to filter by Question Title</param>
        /// <returns>Returns a list of all questions to the view</returns>
        [HttpGet("/Questions")]
        [AllowAnonymous]
        public async Task<IActionResult> ShowQuestions(int pageId = 1, string questionTitle = null)
        {
            var questions = await _questionService.GetQuestionsAsync();

            List<ShowQuestionsListViewModel> showQuestions = new();

            ViewBag.QuestionTitle = questionTitle;

            foreach (var question in questions)
            {
                showQuestions.Add(new ShowQuestionsListViewModel
                {
                    AnswerCount = question.Answers.Count,
                    DateCreated = CommonMethods<DateTime>.GetPersianDate(question.DateCreated),
                    Id = question.Id,
                    Title = question.Title,
                    UserName = question.ApplicationUser.UserName,
                    ArticleTitle = question.Article.Title
                });
                
            }

            if (questionTitle != null)
            {
                showQuestions = showQuestions.Where(q => q.Title.ToLower().Contains(questionTitle.ToLower())).ToList();
            }

            var data = CommonMethods<ShowQuestionsListViewModel>.CalculatePagingNumber(showQuestions, out Pager pager, 10, pageId);

            ViewBag.Pager = pager;

            return View(data);
        }


        /// <summary>
        /// Gets an Id related to a specific question and shows its detail
        /// </summary>
        /// <param name="quesId">PageId</param>
        /// <returns></returns>
        [HttpGet("/ShowQuestion/{quesId}")]
        [AllowAnonymous]
        public async Task<IActionResult> ShowQuestionDetail(int quesId)
        {
            var question = await _questionService.GetQuestionByIdAsync(quesId);
            if (question == null)
                return NotFound();
            var userId = User.Identity.IsAuthenticated ? User.FindFirstValue(ClaimTypes.NameIdentifier).ToString() : "";
            ShowQuestionDetialViewModel questionDetail = new()
            {
                Description = question.Description,
                Title = question.Title,
                DateCreated = CommonMethods<DateTime>.GetPersianDate(question.DateCreated),
                UserName = question.ApplicationUser.UserName,
                QuestionId = quesId,
                UserId = userId
            };

            return View(questionDetail);
        }

        #region CreateQuestion

        [HttpGet]
        public async Task<IActionResult> CreateQuestion(int articleId)
        {
            if (articleId == null)
                return NotFound();

            if (!await _articleService.DoesArticleExistByIdAsync(articleId))
                return NotFound();


            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateQuestion(int articleId, CreateQuestionViewModel createQuestion)
        {
            if (ModelState.IsValid || createQuestion.Description != null)
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

            Notify("لطفا توضیحات را وارد کنید", "خطا", NotificationType.error);
            return RedirectToAction("CreateQuestion", "Question", new { articleId = articleId });
        }

        #endregion
    }
}

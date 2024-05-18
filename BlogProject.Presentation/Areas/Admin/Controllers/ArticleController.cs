using BlogProject.Domain.Models;
using BlogProject.Domain.ViewModels;
using BlogProject.Repository;
using BlogProject.Service.ArticleCategoryService;
using BlogProject.Service.ArticleService;
using BlogProject.Service.CategoryToArticleService;
using BlogProject.Service.EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IArticleCategoryService _articleCategoryService;
        private readonly ICategoryToArticleService _categoryToArticleService;
        private readonly INewsLetterService _newsLetterService;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private string imageFileName = string.Empty;

        public ArticleController(IArticleService articleService,
            IUnitOfWork unitOfWork,
            IArticleCategoryService articleCategoryService,
            ICategoryToArticleService categoryToArticleService,
            INewsLetterService newsLetterService,
            IEmailService emailService,
            IConfiguration configuration)
        {
            _articleService = articleService;
            _unitOfWork = unitOfWork;
            _articleCategoryService = articleCategoryService;
            _categoryToArticleService = categoryToArticleService;
            _newsLetterService = newsLetterService;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(int pageId = 1)
        {
            var articles = await _articleService.GetArticlesAsync();
            List<ArticleListViewModel> articleList = new();
            foreach (var article in articles)
            {
                articleList.Add(new ArticleListViewModel
                {
                    ImageName = article.ImageName,
                    ShortDescription = article.ShortDescription,
                    Tags = article.Tags,
                    Title = article.Title,
                    Id = article.Id,
                    DateCreated = CommonMethods<DateTime>.GetPersianDate(article.DateCreated),
                });
            }

            // paging 
            var data = CommonMethods<ArticleListViewModel>.CalculatePagingNumber(articleList, out Pager pager, 10, pageId);

            ViewBag.Pager = pager;

            return View(data);
        }

        #region Create Article

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _articleCategoryService.GetArticleCategoriesAsync();

            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateArticleViewModel createArticle)
        {
            if (ModelState.IsValid && createArticle.SelectedCategories.Count > 0)
            {
                try
                {
                    // Saving article main image
                    if (createArticle.ArticleImage?.Length > 0)
                    {
                        imageFileName = Guid.NewGuid().ToString().Substring(0, 22)
                            .Replace("-", "") + Path.GetExtension(createArticle.ArticleImage.FileName);

                        string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                            "wwwroot", "Images", "Articles", imageFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            createArticle.ArticleImage.CopyTo(stream);
                        }
                    }

                    Article article = new()
                    {
                        ImageName = imageFileName,
                        DateCreated = DateTime.Now,
                        Description = createArticle.Description,
                        ShortDescription = createArticle.ShortDescription,
                        Tags = createArticle.Tags,
                        Title = createArticle.Title,
                    };

                    await _articleService.CreateNewArticleAsync(article);
                    await _unitOfWork.Commit();

                    if (createArticle.SelectedCategories.Any() && createArticle.SelectedCategories.Count > 0)
                    {
                        foreach (var category in createArticle.SelectedCategories)
                        {
                            await _categoryToArticleService.CreateNewRelation(new CategoryToArticle()
                            {
                                ArticleId = article.Id,
                                CategoryId = category
                            });
                        }
                        await _unitOfWork.Commit();
                    }

                    // send news letter
                    if (createArticle.SendNewsLetter)
                    {
                        var newsLetters = await _newsLetterService.GetNewsLetterEmailsAsync();

                        var articleLink = Url.Action("ShowArticleDetail", "Article",
                            new { articleId = article.Id }, Request.Scheme);

                        var emailBody = CommonMethods<string>.ReadEmailTemplate(articleLink, article.Title, "NewsLetter.html");

                        foreach (var newsLetter in newsLetters)
                        {
                            await _emailService.SendEmailAsync(newsLetter.Email, createArticle.Title, emailBody);
                        }
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            ViewBag.Categories = await _articleCategoryService.GetArticleCategoriesAsync();
            ViewBag.CategoryError = "لطفا گروه را انتخاب کنید";
            return View(createArticle);
        }

        #endregion

        #region Update Article

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            if (id == null)
                return NotFound();

            var article = await _articleService.GetArticleByIdAsync(id);

            if (article == null)
                return NotFound();

            UpdateArticleViewModel updateArticle = new()
            {
                Title = article.Title,
                Description = article.Description,
                ShortDescription = article.ShortDescription,
                Tags = article.Tags,

            };

            ViewBag.Categories = await _articleCategoryService.GetArticleCategoriesAsync();

            ViewBag.ArticleCategories = await _categoryToArticleService.GetCategoriesRelatedToArticle(article.Id);

            return View(updateArticle);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(int id, UpdateArticleViewModel updateArticle)
        {
            if (ModelState.IsValid && updateArticle.SelectedCategories.Count > 0)
            {
                var article = await _articleService.GetArticleByIdAsync(id);

                // Saving article main image
                if (updateArticle.ArticleImage?.Length > 0)
                {
                    System.IO.File.Delete(Directory.GetCurrentDirectory() + "/wwwroot/Images/Articles/" + article.ImageName);

                    imageFileName = Guid.NewGuid().ToString().Substring(0, 22)
                        .Replace("-", "") + Path.GetExtension(updateArticle.ArticleImage.FileName);

                    string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot", "Images", "Articles", imageFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        updateArticle.ArticleImage.CopyTo(stream);
                    }
                }

                article.Description = updateArticle.Description;
                article.Title = updateArticle.Title;
                article.ShortDescription = updateArticle.ShortDescription;
                article.Tags = updateArticle.Tags;
                article.ImageName = ((imageFileName != "") ? imageFileName : article.ImageName);


                // Delete all categories from the current article and set new seleted categories.
                await _categoryToArticleService.DeleteRelatedCategoriesByArticleId(article.Id);
                if (updateArticle.SelectedCategories.Any() && updateArticle.SelectedCategories.Count > 0)
                {
                    foreach (var category in updateArticle.SelectedCategories)
                    {
                        await _categoryToArticleService.CreateNewRelation(new CategoryToArticle
                        {
                            ArticleId = updateArticle.Id,
                            CategoryId = category
                        });
                    }
                }

                await _unitOfWork.Commit();
                return RedirectToAction(nameof(Index));

            }

            ViewBag.Categories = await _articleCategoryService.GetArticleCategoriesAsync();
            ViewBag.ArticleCategories = await _categoryToArticleService.GetCategoriesRelatedToArticle(updateArticle.Id);
            ViewBag.CategoryError = "لطفا گروه را انتخاب کنید";
            return View();
        }
        #endregion

        #region Delete Article

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
                return NotFound();

            var article = await _articleService.GetArticleByIdAsync(id);

            if (article == null)
                return NotFound();

            return PartialView(article);
        }

        [HttpPost, ActionName("Delete")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            if (id == null)
                return NotFound();

            var article = await _articleService.GetArticleByIdAsync(id);

            if (article == null)
                return NotFound();

            await _categoryToArticleService.DeleteRelatedCategoriesByArticleId(article.Id);
            await _articleService.DeleteArticleAsync(article);
            await _unitOfWork.Commit();

            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}

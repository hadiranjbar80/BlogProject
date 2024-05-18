using BlogProject.Domain.Models;
using BlogProject.Domain.ViewModels;
using BlogProject.Service.ArticleService;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

namespace BlogProject.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryToArticleService _categoryToArticleService;
        public HomeController(IArticleService articleService, ICategoryToArticleService categoryToArticleService)
        {
            _articleService = articleService;
            _categoryToArticleService = categoryToArticleService;
        }

        public async Task<IActionResult> Index(string articleTitle = null, int pageId = 1, int categoryId = 0)
        {
            List<ShowArticlesListViewModel> showArticlesList = new();
            List<Article> articles = new();

            if (articleTitle != null || categoryId > 0)
            {
                articles = await _articleService.GetArticlesByTitleAndCategoryId(articleTitle,categoryId);
            }
            else
            {
                articles = await _articleService.GetArticlesAsync();
            }


            foreach (var article in articles)
            {
                showArticlesList.Add(new ShowArticlesListViewModel
                {
                    DateCreated = CommonMethods<DateTime>.GetPersianDate(article.DateCreated),
                    ShortDescription = article.ShortDescription,
                    ImageName = article.ImageName,
                    Title = article.Title,
                    Id = article.Id,
                });
            }

            //paging
            var data = CommonMethods<ShowArticlesListViewModel>.CalculatePagingNumber(showArticlesList, out Pager pager, 10, pageId);
            ViewBag.Pager = pager;

            ViewBag.ArticleTitle = articleTitle;

            return View(data);
        }
    }
}
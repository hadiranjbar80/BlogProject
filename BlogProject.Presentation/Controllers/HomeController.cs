using BlogProject.Domain.Models;
using BlogProject.Domain.ViewModels;
using BlogProject.Presentation.Models;
using BlogProject.Service.ArticleService;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogProject.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;
        public HomeController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IActionResult ShowArticlesList()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            List<ShowArticlesListViewModel> showArticlesList = new();

            var articles = await _articleService.GetArticlesAsync(); 

            foreach (var article in articles)
            {
                showArticlesList.Add(new ShowArticlesListViewModel
                {
                    DateCreated = article.DateCreated,
                    ShortDescription = article.ShortDescription,
                    ImageName = article.ImageName,
                    Title = article.Title,
                    Id = article.Id,
                });
            }

            return View(showArticlesList);
        }
    }
}
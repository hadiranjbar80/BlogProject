using BlogProject.Service.ArticleCategoryService;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Presentation.ViewComponents
{
    public class ShowCategories:ViewComponent
    {
        private readonly IArticleCategoryService _articleCategoryService;

        public ShowCategories(IArticleCategoryService articleCategoryService)
        {
            _articleCategoryService = articleCategoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("/Views/ViewComponents/ShowCategories.cshtml", await _articleCategoryService.GetArticleCategoriesAsync());
        }
    }
}

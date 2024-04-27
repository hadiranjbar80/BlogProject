using BlogProject.Domain.Models;
using BlogProject.Repository;
using BlogProject.Service.ArticleCategoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ArticleCategoryController : Controller
    {
        private readonly IArticleCategoryService _articleCategoryService;
        private readonly IUnitOfWork _unitOfWork;
        public ArticleCategoryController(IArticleCategoryService articleCategoryService, IUnitOfWork unitOfWork)
        {
            _articleCategoryService = articleCategoryService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _articleCategoryService.GetCategoryByParentIdNullAsync();
            return View(categories);
        }

        #region Create Category functionality

        [HttpGet]
        public IActionResult Create(int? id)
        {
            return View(new ArticleCategory()
            {
                ParentId = id,
            });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(ArticleCategory category)
        {
            if (ModelState.IsValid)
            {
                await _articleCategoryService.CreateNewCategoryAsync(category);
                await _unitOfWork.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        #endregion

        #region Update Category

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            if (id == null)
                return NotFound();

            var category = await _articleCategoryService.GetCategoryByIdAsync(id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(ArticleCategory category)
        {
            if (ModelState.IsValid)
            {
                await _articleCategoryService.UpdateCategoryAsync(category);
                await _unitOfWork.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        #endregion

        #region Delete Category

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
                return NotFound();

            var category = await _articleCategoryService.GetCategoryByIdAsync(id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var category = await _articleCategoryService.GetCategoryByIdAsync(id);
            if (category.Parent != null &&
                category.Parent.Any())
            {
                foreach (var subCategory in 
                    await _articleCategoryService.GetCategoryByParentIdNullAsync(id))
                {
                    await _articleCategoryService.DeleteCategoryAsync(subCategory);
                }
            }
            await _articleCategoryService.DeleteCategoryAsync(category);
            await _unitOfWork.Commit();
            return RedirectToAction(nameof(Index));
        }

        #endregion

    }
}

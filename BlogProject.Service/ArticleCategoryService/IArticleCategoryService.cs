using BlogProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Service.ArticleCategoryService
{
    public interface IArticleCategoryService
    {
        Task<List<ArticleCategory>> GetArticleCategoriesAsync();
        Task CreateNewCategoryAsync(ArticleCategory category);
        Task UpdateCategoryAsync(ArticleCategory category);
        Task DeleteCategoryAsync(ArticleCategory category);
        Task<ArticleCategory> GetCategoryByIdAsync(int categoryId);
        Task<List<ArticleCategory>> GetCategoryByParentIdNullAsync(int? parentId = null);
    }
}

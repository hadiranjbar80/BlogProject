using BlogProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Service.CategoryToArticleService
{
    public interface ICategoryToArticleService
    {
        Task CreateNewRelation(CategoryToArticle categoryToArticle);
        Task<List<CategoryToArticle>> GetCategoriesRelatedToArticle(int articleId);
        Task DeleteRelatedCategoriesByArticleId(int articleId);
    }
}

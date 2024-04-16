using BlogProject.Domain.Models;
using BlogProject.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Service.CategoryToArticleService
{
    public class CategoryToArticleService : ICategoryToArticleService
    {
        private readonly IRepository<CategoryToArticle> _repository;
        private readonly AppDbContext _context;

        public CategoryToArticleService(IRepository<CategoryToArticle> repository, AppDbContext context)
        {
            _context = context;
            _repository = repository;
        }

        public async Task CreateNewRelation(CategoryToArticle categoryToArticle)
        {
            await _repository.AddAsync(categoryToArticle);
        }

        public async Task DeleteRelatedCategoriesByArticleId(int articleId)
        {
            var categories = await _context.CategoryToArticles.Where(c => c.ArticleId == articleId).ToListAsync();

            foreach (var item in categories)
            {
                _context.CategoryToArticles.Remove(item);
            }
        }

        public async Task<List<CategoryToArticle>> GetCategoriesRelatedToArticle(int articleId)
        {
           return await _context.CategoryToArticles.Where(c => c.ArticleId == articleId).ToListAsync();
        }
    }
}

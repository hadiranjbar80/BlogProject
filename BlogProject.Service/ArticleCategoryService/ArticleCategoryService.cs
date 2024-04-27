using BlogProject.Domain.Models;
using BlogProject.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Service.ArticleCategoryService
{
    public class ArticleCategoryService : IArticleCategoryService
    {
        private readonly IRepository<ArticleCategory> _repository;
        private readonly AppDbContext _context;

        public ArticleCategoryService(IRepository<ArticleCategory> repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task CreateNewCategoryAsync(ArticleCategory category)
        {
            await _repository.AddAsync(category);
        }

        public async Task DeleteCategoryAsync(ArticleCategory category)
        {
            await _repository.Delete(category);
        }

        public async Task<List<ArticleCategory>> GetArticleCategoriesAsync()
        {
            return await _context.ArticleCategories.Include(a => a.CategoryToArticles).ToListAsync();
        }

        public async Task<ArticleCategory> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.ArticleCategories.Where(c=>c.Id == categoryId).Include(c => c.Parent).FirstOrDefaultAsync();
        }

        public async Task<List<ArticleCategory>> GetCategoryByParentIdNullAsync(int? parentId = null)
        {
            return await _context.ArticleCategories.Where(c => c.ParentId == parentId).Include(c=>c.Parent).ToListAsync();
        }

        public async Task UpdateCategoryAsync(ArticleCategory category)
        {
            await _repository.Update(category);
        }
    }
}

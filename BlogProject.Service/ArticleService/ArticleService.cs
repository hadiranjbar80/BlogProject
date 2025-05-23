﻿using BlogProject.Domain.Models;
using BlogProject.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Service.ArticleService
{
    public class ArticleService : IArticleService
    {
        private readonly IRepository<Article> _repository;
        private readonly AppDbContext _context;
        public ArticleService(IRepository<Article> repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task CreateNewArticleAsync(Article article)
        {
            await _repository.AddAsync(article);
        }

        public async Task DeleteArticleAsync(Article article)
        {
            await _repository.Delete(article);
        }

        public async Task<bool> DoesArticleExistByIdAsync(int articleId)
        {
            if(await _context.Articles.AnyAsync(a => a.Id == articleId))
            {
                return true;
            }
            return false;
        }

        public async Task<Article> GetArticleByIdAsync(int articleId)
        {
            return await _context.Articles.Include(x=>x.ArticleComments)
                .Include(a=>a.CategoryToArticles)
                .FirstOrDefaultAsync(c => c.Id == articleId);
        }

        public async Task<List<Article>> GetArticlesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<List<Article>> GetArticlesByTitleAndCategoryId(string title, int categoryId)
        {
            return await _context.Articles.
                Where(a=>(a.Title.Contains(title) || a.ShortDescription.Contains(title)) || a.CategoryToArticles.Any(c=>c.CategoryId == categoryId)).ToListAsync();
        }

        public async Task UpdateArticleAsync(Article article)
        {
            await _repository.Update(article);
        }
    }
}

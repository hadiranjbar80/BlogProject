using BlogProject.Domain.Models;
using BlogProject.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Service.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<ArticleComment> _repository;
        private readonly AppDbContext _context;

        public CommentService(AppDbContext context, 
            IRepository<ArticleComment> repository)
        {
            _repository = repository;
            _context = context;
        }

        public async Task ConfirmAllComments()
        {
            var comments = await _context.ArticleComments.Where(q => !q.IsConfirmed).ToListAsync();
            foreach (var comment in comments)
            {
                comment.IsConfirmed = true;
            }
        }

        public async Task CreateCommentAsync(ArticleComment comment)
        {
            await _repository.AddAsync(comment);
        }

        public async Task DeleteCommentAsync(ArticleComment comment)
        {
            await _repository.Delete(comment);
        }

        public async Task DeleteNotConfirmedComments()
        {
            var comments = await _context.ArticleComments.Where(q => !q.IsConfirmed).ToListAsync();
            foreach(var comment in comments)
            {
                await DeleteCommentAsync(comment);
            }
        }

        public async Task<List<ArticleComment>> GetCommentByArticleId(int articleId)
        {
            return await _context.ArticleComments.Where(c=>c.ArticleId == articleId).ToListAsync();
        }

        public async Task<ArticleComment> GetCommentByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<ArticleComment>> GetCommentsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task UpdateCommentAsync(ArticleComment comment)
        {
            await _repository.Update(comment);
        }
    }
}

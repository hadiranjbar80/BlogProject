using BlogProject.Domain.Models;
using BlogProject.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Service.QuestionService
{
    public class QuestionService : IQuestionService
    {
        private readonly IRepository<Question> _repository;
        private readonly AppDbContext _context;
        public QuestionService(IRepository<Question> repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;

        }

        public async Task CreateNewQuestionAsync(Question question)
        {
            await _repository.AddAsync(question);
        }

        public async Task DeleteQuestionAsync(Question question)
        {
            await _repository.Delete(question);
        }

        public async Task<List<Question>> GetLastCreatedQuestionsAsync()
        {
            return await _context.Questions.OrderByDescending(q=>q.DateCreated).Take(8).ToListAsync();
        }

        public async Task<List<Question>> GetLastFiveQuestionsAsync()
        {
            return await _context.Questions.Include(q=>q.ApplicationUser)
                .OrderByDescending(q=>q.DateCreated).ToListAsync();
        }

        public async Task<Question> GetQuestionByIdAsync(int id)
        {
            return await _context.Questions.
                Include(q => q.ApplicationUser)
                .Include(q=>q.Answers)
                .Where(q=>q.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Question>> GetQuestionByUserIdAsync(string userId)
        {
            return await _context.Questions.Where(q => q.UserId == userId).ToListAsync();
        }

        public async Task<List<Question>> GetQuestionsAsync()
        {
            return await _context.Questions
                .Include(q => q.Answers)
                .Include(q => q.ApplicationUser)
                .Include(q=>q.Article).ToListAsync();
        }

        public async Task<List<Question>> GetQuestionsByArticleIdAsync(int articleId)
        {
            return await _context.Questions.Include(q=>q.Answers)
                .Where(q=>q.ArticleId == articleId).ToListAsync();
        }

        public async Task<List<Question>> SearchQuestionAsync(string q)
        {
            return await _context.Questions.Where(q => q.Title.Contains(q.ToString())).ToListAsync();
        }

        public async Task UpdateQuestion(Question question)
        {
            await _repository.Update(question);
        }
    }
}

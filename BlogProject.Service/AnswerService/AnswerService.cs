using BlogProject.Domain.Models;
using BlogProject.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Service.AnswerService
{
    public class AnswerService : IAnswerService
    {
        private readonly IRepository<Answer> _repository;
        private readonly AppDbContext _context;
        public AnswerService(IRepository<Answer> repository,AppDbContext context)
        { 
            _repository = repository;
            _context = context;
        }

        public async Task CreateNewAnswerAsync(Answer answer)
        {
            await _repository.AddAsync(answer);
        }

        public async Task DeleteAnswerAsync(Answer answer)
        {
            await _repository.Delete(answer);
        }

        public async Task<List<Answer>> GetAnswerByQuestionIdAsync(int questionId)
        {
            return await _context.Answers.Include(a=>a.ApplicationUser).
                Where(a => a.QuestionId == questionId).ToListAsync();
        }

        public async Task UpdateAnswerAsync(Answer answer)
        {
            await _repository.Update(answer);
        }
    }
}

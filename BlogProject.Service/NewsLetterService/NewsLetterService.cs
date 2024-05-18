using BlogProject.Domain.Models;
using BlogProject.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Service.NewsLetterService
{
    public class NewsLetterService : INewsLetterService
    {
        private readonly IRepository<NewsLetter> _repository;
        private readonly AppDbContext _context;

        public NewsLetterService(IRepository<NewsLetter> repository, AppDbContext context)
        {
            _context = context;
            _repository = repository;
        }

        public async Task AddToNewsLetterAsync(NewsLetter newsLetter)
        {
            await _repository.AddAsync(newsLetter);
        }

        public async Task<NewsLetter> GetByEmailAsync(string email)
        {
           return await _context.NewsLetters.SingleOrDefaultAsync(n => n.Email.ToLower() == email.ToLower());
        }

        public async Task<List<NewsLetter>> GetNewsLetterEmailsAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}

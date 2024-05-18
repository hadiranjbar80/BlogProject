using BlogProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Service.NewsLetterService
{
    public interface INewsLetterService
    {
        Task AddToNewsLetterAsync(NewsLetter newsLetter);
        Task<List<NewsLetter>> GetNewsLetterEmailsAsync();
        Task<NewsLetter> GetByEmailAsync(string email);
    }
}

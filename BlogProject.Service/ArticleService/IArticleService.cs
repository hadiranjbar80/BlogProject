using BlogProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Service.ArticleService
{
    public interface IArticleService
    {
        Task<List<Article>> GetArticlesAsync();
        Task CreateNewArticleAsync(Article article);
        Task UpdateArticleAsync(Article article);
        Task DeleteArticleAsync(Article article);
        Task<Article> GetArticleByIdAsync(int articleId);
        Task<bool> DoesArticleExistByIdAsync(int articleId);
    }
}

using BlogProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Service.CommentService
{
    public interface ICommentService
    {
        Task<List<ArticleComment>> GetCommentsAsync();
        Task<ArticleComment> GetCommentByIdAsync(int id);
        Task CreateCommentAsync(ArticleComment comment);
        Task DeleteCommentAsync(ArticleComment comment);
        Task DeleteNotConfirmedComments();
        Task ConfirmAllComments();
        Task UpdateCommentAsync(ArticleComment comment);
        Task<List<ArticleComment>> GetCommentByArticleId(int articleId);
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Domain.Models
{
    public class Article
    {
        public Article()
        {
            CategoryToArticles = new List<CategoryToArticle>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string Tags { get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;

        // navigation props
        public List<CategoryToArticle> CategoryToArticles { get; set; }
        public List<Question> Questions { get; set; }
        public List<ArticleComment> ArticleComments { get; set; }
    }
}

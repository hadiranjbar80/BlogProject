using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Domain.Models
{
    public class CategoryToArticle
    {
        public int ArticleId { get; set; }
        public int CategoryId { get; set; }

        // Navigation props
        [ForeignKey("CategoryId")]
        public virtual ArticleCategory ArticleCategory { get; set; }
        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }
    }
}

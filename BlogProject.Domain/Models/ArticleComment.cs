using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Domain.Models
{
    public class ArticleComment
    {
        public ArticleComment()
        {
        }

        public int Id { get; set; }
        public int ArticleId { get; set; }
        public string CommentText { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }


        // navigation props
        //[ForeignKey(nameof(ArticleId))]
        //public Article Article { get; set; }
    }
}

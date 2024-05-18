using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string UserId { get; set; } = string.Empty;
        public string  UserName { get; set; } =string.Empty;
        public string CommentText { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public bool IsConfirmed { get; set; } = false;
        public int ParentId { get; set; }


        // navigation props
        [ForeignKey(nameof(ArticleId))]
        public Article Article { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser ApplicationUser { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Domain.Models
{
    public class ArticleCategory
    {
        public ArticleCategory()
        {
        }

        public int Id { get; set; }
        [DisplayName("عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; } = string.Empty;
        public Nullable<int> ParentId { get; set; }

        // navigation props
        [ForeignKey(nameof(ParentId))]
        public virtual List<ArticleCategory>? Parent { get; set; }
        public List<CategoryToArticle>? CategoryToArticles { get; set; }

    }
}

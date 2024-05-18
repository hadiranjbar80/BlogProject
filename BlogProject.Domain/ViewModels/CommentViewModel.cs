using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Domain.ViewModels
{
    public class CreateCommentViewModel
    {
        public int ArticleId { get; set; }
        public int ParentId { get; set; } = 0;
        [DisplayName("متن کامنت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CommentText { get; set; } = string.Empty;
    }
}

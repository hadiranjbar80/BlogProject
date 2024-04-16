using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Domain.ViewModels
{
    public class ShowQuestionsList
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
    }

    public class CreateQuestionViewModel
    {
        public int ArticleId { get; set; }
        [DisplayName("عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; } = string.Empty;
        [DisplayName("توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; } = string.Empty;
    }
}

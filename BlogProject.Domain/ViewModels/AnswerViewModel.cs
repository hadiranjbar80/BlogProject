using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Domain.ViewModels
{
    public class CreateAnswerViewModel
    {
        public int QuestionId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string UserEmail { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class ShowAnswersViewModel
    {
        [DisplayName("توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; } = string.Empty;
        public string DateCreated { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}

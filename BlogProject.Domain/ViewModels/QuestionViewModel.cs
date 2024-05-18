using BlogProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Domain.ViewModels
{
    public class ShowQuestionsListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string DateCreated { get; set; } = string.Empty;
        public string ArticleTitle { get; set; } = string.Empty;
        public int AnswerCount { get; set; }
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

    public class ShowQuestionDetialViewModel
    {
        public int QuestionId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; }= string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string DateCreated { get; set; }
        public string UserId { get; set; } = string.Empty;
    }

    public class ShowLastQuestionsViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string DateCreated { get; set; } = string.Empty;
    }
}

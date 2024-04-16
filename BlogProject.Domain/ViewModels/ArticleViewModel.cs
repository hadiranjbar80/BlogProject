using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Domain.ViewModels
{
    [NotMapped]
    public class CreateArticleViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "عنوان")]
        public string Title { get; set; } = string.Empty;
        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; } = string.Empty;
        [Display(Name = "توضیح کوتاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ShortDescription { get; set; } = string.Empty;
        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public IFormFile ArticleImage { get; set; }
        [Display(Name = "کلمه‌های کلیدی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Tags { get; set; } = string.Empty;
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DisplayName("گروه")]
        public List<int> SelectedCategories { get; set; } = new();
    }

    [NotMapped]
    public class ArticleListViewModel
    {
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        public string Title { get; set; } = string.Empty;
        [Display(Name = "توضیح کوتاه")]
        public string ShortDescription { get; set; } = string.Empty;
        [Display(Name = "تصویر")]
        public string ImageName { get; set; } = string.Empty;
        [Display(Name = "کلمه‌های کلیدی")]
        public string Tags { get; set; } = string.Empty;
        [Display(Name = "‌تاریخ ایجاد")]
        public DateTime DateCreated { get; set; }
    }

    [NotMapped]
    public class UpdateArticleViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "عنوان")]
        public string Title { get; set; } = string.Empty;
        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; } = string.Empty;
        [Display(Name = "توضیح کوتاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ShortDescription { get; set; } = string.Empty;
        [Display(Name = "تصویر")]
        public IFormFile? ArticleImage { get; set; }
        [Display(Name = "کلمه‌های کلیدی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Tags { get; set; } = string.Empty;
        public List<int> SelectedCategories { get; set; }
    }

    [NotMapped]
    public class ShowArticlesListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }=string.Empty;
        public string ShortDescription { get; set; }= string.Empty;
        public string ImageName { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
    }

    [NotMapped]
    public class ShowArticleDetailViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
    }
}

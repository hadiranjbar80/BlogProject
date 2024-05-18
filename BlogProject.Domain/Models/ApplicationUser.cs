using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool AccountDisabled { get; set; } = false;

        public List<Question> Questions { get; set; }
        public List<Answer> Answers { get; set; }
        public List<ArticleComment> ArticleComments { get; set; }
    }
}

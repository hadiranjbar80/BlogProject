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
        public List<Question> Questions { get; set; }
        public List<Answer> Answers { get; set; }
    }
}

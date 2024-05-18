using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Domain.ViewModels
{
    public class EmailSetting
    {
        public string MailServer { get; set; } = string.Empty;
        public int MailPort { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public string Sender { get; set;} = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

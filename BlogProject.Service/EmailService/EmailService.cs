using BlogProject.Domain.ViewModels;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Service.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly EmailSetting _emailSetting;

        public EmailService(IOptions<EmailSetting> emailSetting)
        {
            _emailSetting = emailSetting.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var cridentials = new NetworkCredential(_emailSetting.Sender, _emailSetting.Password);

                var mail = new MailMessage
                {
                    From = new MailAddress(_emailSetting.Sender, _emailSetting.SenderName),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                mail.To.Add(new MailAddress(email));

                var client = new SmtpClient
                {
                    Port = _emailSetting.MailPort,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = _emailSetting.MailServer,
                    EnableSsl = true,
                    Credentials = cridentials
                };

                await client.SendMailAsync(mail);

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}

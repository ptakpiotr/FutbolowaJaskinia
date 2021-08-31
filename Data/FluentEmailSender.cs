using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FutbolowaJaskinia.Data
{
    public class FluentEmailSender : IEmailSender
    {
        public FluentEmailSender(IConfiguration config)
        {
            var sender = new SmtpSender(new SmtpClient("smtp.ethereal.email")
            {
                EnableSsl = true,
                Port = 587,
                Credentials = new NetworkCredential(config.GetSection("Mailing:Email").Value, config.GetSection("Mailing:Password").Value)
            });

            Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer();
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await Email.From("marschall.lesch5@ethereal.email").To(email).Subject(subject)
                .Body(htmlMessage, isHtml: true).SendAsync();
        }
    }
}

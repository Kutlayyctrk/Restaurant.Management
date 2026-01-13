using Microsoft.Extensions.Options;
using Project.Application.MailService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Project.OuterInfrastructure.Tools
{
    public class MailSender : IMailSender
    {
        private readonly SmtpOptions _smtp;

        public MailSender(IOptions<SmtpOptions> smptpOptions)
        {
            _smtp = smptpOptions.Value;
        }

        public async Task SendActivationMailAsync(string Email, string ActivationLink, CancellationToken cancellationToken = default)
        {
            string subject = "Hesap Aktivasyonu";
            string body =
                $"<p>Merhaba,</p>" +
                $"<p>Hesabınızı aktifleştirmek için lütfen aşağıdaki linke tıklayınız:</p>" +
                $"<p><a herf=\"{ActivationLink}\">{ActivationLink}</a></p>";

            using MailMessage messsage = new MailMessage()
            {
                From = new MailAddress(_smtp.Email, _smtp.FromName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            messsage.To.Add(Email);

            using SmtpClient client =new SmtpClient(_smtp.Host, _smtp.Port)
            {
                EnableSsl = _smtp.EnableSSl,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_smtp.UserName, _smtp.UserName),
            };
            await client.SendMailAsync(messsage, cancellationToken);
        }
    }
}

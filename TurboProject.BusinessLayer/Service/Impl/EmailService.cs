using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using TurboProject.BusinessLayer.Model.Smtp;
using TurboProject.BusinessLayer.Service.Interface;

namespace TurboProject.BusinessLayer.Service.Impl
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<SmtpSettings> smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            this.smtpSettings = smtpSettings;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var smtpSettingsValue = smtpSettings.Value;

            using var smtpClient = new SmtpClient(smtpSettingsValue.SmtpServer)
            {
                Port = smtpSettingsValue.SmtpPort,
                Credentials = new NetworkCredential(smtpSettingsValue.SmtpUsername, smtpSettingsValue.SmtpPassword),
                EnableSsl = smtpSettingsValue.EnableSsl
            };

            using var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpSettingsValue.FromEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }

    }
}

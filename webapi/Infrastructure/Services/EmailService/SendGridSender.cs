using System.Linq;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace webapi.Infrastructure.Services.EmailService
{
    public class SendGridSender : IEmailSender
    {
        private readonly EmailConfiguration emailConfig;
        public SendGridSender(EmailConfiguration emailCfg)
        {
            emailConfig = emailCfg;
        }
        public void SendEmail(Message message)
        {
            throw new System.Exception("not implement yet");
        }

        public async Task SendEmailAsync(Message message)
        {
            var client = new SendGridClient(emailConfig.ApiKey);
            var from = new EmailAddress("support@klickon.ca", "Support Klickon.ca");
            var subject = message.Subject;
            var to = message.To.First();
            // var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = message.Content;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, string.Empty, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
    }
}
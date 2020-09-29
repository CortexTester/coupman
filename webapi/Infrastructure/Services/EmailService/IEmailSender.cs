using System.Threading.Tasks;

namespace webapi.Infrastructure.Services.EmailService
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);
    }
}
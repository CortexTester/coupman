using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Mail;

// namespace webapi.Services.EmailService
// {
//     public class Message
//     {
//          public List<MailboxAddress> To { get; set; }
//         public string Subject { get; set; }
//         public string Content { get; set; }

//         public IFormFileCollection Attachments { get; set; }

//         public Message(IEnumerable<string> to, string subject, string content, IFormFileCollection attachments)
//         {
//             To = new List<MailboxAddress>();

//             To.AddRange(to.Select(x => new MailboxAddress(x)));
//             Subject = subject;
//             Content = content;
//             Attachments = attachments;
//         }
//     }
// }

namespace webapi.Infrastructure.Services.EmailService
{
    public class Message
    {
         public List<EmailAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public IFormFileCollection Attachments { get; set; }

        public Message(IEnumerable<string> to, string subject, string content, IFormFileCollection attachments)
        {
            To = new List<EmailAddress>();

            To.AddRange(to.Select(x => new EmailAddress(x)));
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }
    }
}
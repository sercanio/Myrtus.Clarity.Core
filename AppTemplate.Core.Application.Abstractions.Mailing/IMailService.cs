using AppTemplate.Core.Domain.Abstractions.Mailing;

namespace AppTemplate.Core.Application.Abstractions.Mailing
{
    public interface IMailService
    {
        void SendMail(Mail mail);
        Task SendEmailAsync(Mail mail);
    }
}

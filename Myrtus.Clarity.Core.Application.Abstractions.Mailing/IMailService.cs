using Myrtus.Clarity.Core.Domain.Abstractions.Mailing;

namespace Myrtus.Clarity.Core.Application.Abstractions.Mailing
{
    public interface IMailService
    {
        void SendMail(Mail mail);
        Task SendEmailAsync(Mail mail);
    }
}

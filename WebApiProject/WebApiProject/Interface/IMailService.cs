using WebApiProject.Entities;
using WebApiProject.Models;

namespace WebApiProject.Interface
{
    public interface IMailService
    {
        Task<bool> SendMailAsync(RequestMail mail, SmtpSettings smtpSettings);
    }
}

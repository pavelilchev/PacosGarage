namespace Autoshop.Web.Services
{
    using System.Threading.Tasks;

    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(string email, string subject, string message);
    }
}

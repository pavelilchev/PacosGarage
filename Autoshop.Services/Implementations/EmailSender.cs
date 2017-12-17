namespace Autoshop.Services.Implementations
{
    using Microsoft.Extensions.Configuration;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;

    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration configuration;

        public EmailSender(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<bool> SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(this.configuration["EmailConfiguration:Username"]);
                mailMessage.To.Add(email);
                mailMessage.ReplyToList.Add(new MailAddress(email));
                mailMessage.Body = message;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = subject;

                using (SmtpClient client = new SmtpClient(
                    this.configuration["EmailConfiguration:SMTPServer"], 
                    int.Parse(this.configuration["EmailConfiguration:Port"])))
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(
                        this.configuration["EmailConfiguration:Username"], 
                        this.configuration["EmailConfiguration:Password"]);

                    await client.SendMailAsync(mailMessage);

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}

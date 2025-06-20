using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string message)
    {
        var client = new SmtpClient("smtp.your-email-provider.com", 587)
        {
            Credentials = new NetworkCredential("your-email@example.com", "your-email-password"),
            EnableSsl = true
        };

        var mailMessage = new MailMessage("your-email@example.com", email, subject, message)
        {
            IsBodyHtml = true
        };

        return client.SendMailAsync(mailMessage);
    }
}

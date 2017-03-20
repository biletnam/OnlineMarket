using System.Net;
using System.Net.Mail;
using OnlineMarket.Utilities.Interfaces;

namespace OnlineMarket.Utilities.Servicies
{
    public class SendEmailService : ISendEmailService
    {
        private const string Subject = "Confirm your email.";
        private const string Body = "Hello! Please, confirm you email for the OnlineMarket site, clicking next link";
        private readonly string _email;
        private readonly string _password;

        public SendEmailService(string email, string password)
        {
            _email = email;
            _password = password;
        }

        public void Send(string to, string link)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(to));
            message.From = new MailAddress(_email);
            message.Subject = Subject;
            message.Body = $"{Body} {link}";

            using (var smtp = new SmtpClient())
            {
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(_email, _password);
                smtp.Port = 587;
                smtp.Send(message);
            }
        }
    }
}
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;

namespace Joya.Api.Helpers
{
    public class EmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public bool SendEmail(Email email)
        {
            try
            {
                var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(_emailSettings.From, _emailSettings.Password)
                };

                client.Send(_emailSettings.From, email.To, email.Subject, email.Body);
                return true;
            }
            catch (Exception ex)
            {
                // Optionally log the exception or handle it differently
                return false;
            }


    }    }   
}

using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using MailKit.Security;

namespace LAB2
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("App Name", emailSettings["From"]));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(emailSettings["Host"], int.Parse(emailSettings["Port"]), SecureSocketOptions.SslOnConnect);
                
                await client.AuthenticateAsync(emailSettings["Username"], emailSettings["Password"]);
                await client.SendAsync(message);
            }
            catch (Exception ex)
            {
                // Логування помилок або обробка винятків
                Console.WriteLine(ex);
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }
    }
}

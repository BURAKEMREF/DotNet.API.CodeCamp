using MimeKit;
using MailKit.Net.Smtp;

using WebApiProject.Entities;
using WebApiProject.Interface;


namespace WebApiProject.Services
{
    public class MailService : IMailService 
    {
        private readonly SmtpSettings _smtpSettings;
        public MailService(SmtpSettings smtpSettings)
        {
            _smtpSettings = smtpSettings;
        }

        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;

        public MailService(string smtpServer, int smtpPort, string smtpUser, string smtpPass)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUser = smtpUser;
            _smtpPass = smtpPass;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sender Name", _smtpUser));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = body
            };
            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect(_smtpServer, _smtpPort, false);
                    client.Authenticate(_smtpUser, _smtpPass);
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }
}

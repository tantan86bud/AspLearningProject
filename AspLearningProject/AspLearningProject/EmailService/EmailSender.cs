using System.Text;
using EmailService;

using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;


namespace AspLearningProject.EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            this.emailConfig = emailConfig;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            string currentPath = Directory.GetCurrentDirectory();
            var dir =   Directory.CreateDirectory(Path.Combine(currentPath, emailConfig.MailFolder));
           try
           {
                MailMessage mailMessage = new MailMessage(emailConfig.From, email, subject, htmlMessage);
                SmtpClient smtpClient = new SmtpClient
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = emailConfig.SmtpServer,
                    Port = emailConfig.Port,
                    Credentials = new System.Net.NetworkCredential(emailConfig.From, emailConfig.Password),
                    EnableSsl = emailConfig.EnableSsl
                };
               
                mailMessage.IsBodyHtml = true;
                smtpClient.Send(mailMessage);
            }
            catch (Exception  e)
            {
                throw;

            }
         
            


        }

   
    }
}

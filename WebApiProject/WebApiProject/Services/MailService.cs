using System.Net;
using System.Net.Mail;
using System.Text;
using WebApiProject.Entities;
using WebApiProject.Interface;
using WebApiProject.Models;

namespace WebApiProject.Services
{
    public class MailService : IMailService
    {
        public async Task<bool> SendMailAsync(RequestMail mail, SmtpSettings smtpSettings)
        {
            string mailContent = string.Empty;
            if (!string.IsNullOrEmpty(mail.MailTemplateName))
            {
                switch (mail.MailTemplateName)
                {
                    case MailConsts.VirtualHRProfileUpdateMailTemplateName:
                        mailContent = await PrepareProfileUpdateDemandMailTemplateAsync(mail.MailTemplateName, mail.ProfileUpdateDemandStructure);
                        break;
                }
            }

            MailMessage mailMessage = new MailMessage();
            SmtpClient client = new SmtpClient();

            client.Port = Convert.ToInt32(smtpSettings.Port);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Host = smtpSettings.Server;
            client.Credentials = new NetworkCredential(smtpSettings.Username, smtpSettings.Password);
            mailMessage.IsBodyHtml = true;

            PrepareRecipients(mail, mailMessage);

            mailMessage.From = new MailAddress(smtpSettings.Username, mail.MailDisplayName);
            mailMessage.Subject = mail.Subject;
            mailMessage.SubjectEncoding = Encoding.UTF8;

            for (int counter = 0; counter < mail.AttachmentList.Count; counter++)
            {
                mailMessage.Attachments.Add(new Attachment(mail.AttachmentList[counter].OpenReadStream(), mail.AttachmentList[counter].FileName));
            }

            mailMessage.Body = mailContent;
            mailMessage.BodyEncoding = Encoding.UTF8;

            bool mailSendResult = true;
            try
            {
                await client.SendMailAsync(mailMessage);

                return mailSendResult;
            }
            catch (Exception exception)
            {
                //Loglama yapılabilir.

                return !mailSendResult;
            }
        }

        private async Task<string> PrepareProfileUpdateDemandMailTemplateAsync(string mailTemplateName, ProfileUpdateDemandStructure mailStructure)
        {
            string mailTemplatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Services");
            if (!Directory.Exists(mailTemplatePath))
                throw new Exception($"Services klasörü bulunamadı");

            string mailTemplateFile = Path.Combine(mailTemplatePath, mailTemplateName);
            if (!File.Exists(mailTemplateFile))
                throw new Exception($"Mail Template dosyası bulunamadı");

            MemoryStream ms = new MemoryStream();
            using (FileStream fs = new FileStream(mailTemplateFile, FileMode.Open, FileAccess.ReadWrite))
            {
                await fs.CopyToAsync(ms);
            }
            byte[]? fileByteData = ms.ToArray();

            string bodyText = string.Empty;
            bodyText = Encoding.UTF8.GetString(fileByteData);

            if (!string.IsNullOrEmpty(mailStructure.Introduction))
                bodyText = bodyText.Replace("#INTRODUCTION#", mailStructure.Introduction);
            else
                bodyText = bodyText.Replace("#INTRODUCTION#", string.Empty);

            if (!string.IsNullOrEmpty(mailStructure.MainContent))
                bodyText = bodyText.Replace("#MAINCONTENT#", mailStructure.MainContent);
            else
                bodyText = bodyText.Replace("#MAINCONTENT#", string.Empty);

            if (!string.IsNullOrEmpty(mailStructure.Conclusion))
                bodyText = bodyText.Replace("#CONCLUSION#", mailStructure.Conclusion);
            else
                bodyText = bodyText.Replace("#CONCLUSION#", string.Empty);

            if (!string.IsNullOrEmpty(mailStructure.Footer))
                bodyText = bodyText.Replace("#FOOTER#", mailStructure.Footer);
            else
                bodyText = bodyText.Replace("#FOOTER#", "101 IK Bilgi Sistemleri");

            return bodyText;
        }

        private void PrepareRecipients(RequestMail mail, MailMessage mailMessage)
        {
            string to = string.Join(",", mail.Recipients.ListofEmailTo);
            string bcc = string.Join(",", mail.Recipients.ListOfEmailBCC);
            string cc = string.Join(",", mail.Recipients.ListOfEmailCC);

            RecipientsCombined recipientsCombined = new RecipientsCombined()
            {
                ListOfEmailBCC = bcc,
                ListOfEmailCC = cc,
                ListofEmailTo = to
            };

            if (!string.IsNullOrEmpty(recipientsCombined.ListofEmailTo))
                mailMessage.To.Add(recipientsCombined.ListofEmailTo);

            if (!string.IsNullOrEmpty(recipientsCombined.ListOfEmailCC))
                mailMessage.CC.Add(recipientsCombined.ListOfEmailCC);

            if (!string.IsNullOrEmpty(recipientsCombined.ListOfEmailBCC))
                mailMessage.Bcc.Add(recipientsCombined.ListOfEmailBCC);
        }
    }
}
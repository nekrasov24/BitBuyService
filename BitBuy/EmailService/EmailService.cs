using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BitBuy.EmailService
{
    public class EmailService
    {

        private readonly EmailSettingsModel _emailSettingsModel;

        public EmailService(EmailSettingsModel emailSettingsModel)
        {
            _emailSettingsModel = emailSettingsModel;
        }

        public async Task<string> SendEmailAsync(object requestModel)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_emailSettingsModel.Email);
                email.To.Add(MailboxAddress.Parse(requestModel.EMailAdrress));
                email.Subject = requestModel.Topic;
                var builder = new BodyBuilder();
                if (requestModel.Attachments != null)
                {
                    byte[] fileBytes;
                    foreach (var file in requestModel.Attachments)
                    {
                        if (file.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                file.CopyTo(ms);
                                fileBytes = ms.ToArray();
                            }
                            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                        }
                    }
                }
                builder.HtmlBody = requestModel.Body;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_emailSettingsModel.Host, _emailSettingsModel.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_emailSettingsModel.Email, _emailSettingsModel.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                return $"Mail was sent to {requestModel.MailOwner}";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

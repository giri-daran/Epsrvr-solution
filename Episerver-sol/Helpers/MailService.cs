using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Routing;
using EpiserverBH.Models.Pages;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EpiserverBH.Helpers
{
    public class MailService
    {
        public static void AppSettings(IConfiguration Configuration, out string UserID, out string Password, out string SMTPPort, out string Host)
        {
            string hosts = Configuration.GetValue<string>("EPiServer:CMS:Smtp:Network:Host");
            int port = Configuration.GetValue<int>("EPiServer:CMS:Smtp:Network:Port");
            string fromAddress = Configuration.GetValue<string>("EPiServer:CMS:Smtp:Network:NotificationEmailAddress");
            string userName = Configuration.GetValue<string>("EPiServer:CMS:Smtp:Network:UserName");
            string password = Configuration.GetValue<string>("EPiServer:CMS:Smtp:Network:Password");

           
            UserID = userName;
            Password = password;
            SMTPPort = Convert.ToString(port);
            Host = hosts;
        }
        public static void SendEmail(string From, string Subject, string Body, string To, string UserID, string Password, string SMTPPort, string Host)
        {

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(To);
            mail.From = new MailAddress(From);
            mail.Subject = Subject;
            mail.Body = Body;
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = Host;
            smtp.Port = Convert.ToInt16(SMTPPort);
            smtp.Credentials = new NetworkCredential(UserID, Password);
            smtp.EnableSsl = true;

            smtp.Send(mail);
        }
        public static void SendEmailWithAttachment(string From, string Subject, string Body, string To, string CC, string attachmentFilename, string UserID, string Password, string SMTPPort, string Host)
        {
            MemoryStream stream = null;
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(To);
            if (CC != string.Empty)
            {
                mail.CC.Add(CC);
            }

            mail.From = new MailAddress(From);
            mail.Subject = Subject;
            mail.Body = Body;
            mail.BodyEncoding = Encoding.UTF8;
            // if (attachmentFilename != null)
            //  mail.Attachments.Add(new Attachment(attachmentFilename));

            if (attachmentFilename != null)
            {
                var contentBytes = System.IO.File.ReadAllBytes(attachmentFilename);
                stream = new MemoryStream(contentBytes);
                stream.Position = 0;

                mail.Attachments.Add(new Attachment(stream, System.IO.Path.GetFileName(attachmentFilename), MimeKit.MimeTypes.GetMimeType(attachmentFilename)));
            }

            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = Host;
            smtp.Port = Convert.ToInt16(SMTPPort);
            smtp.Credentials = new NetworkCredential(UserID, Password);
            smtp.EnableSsl = true;

            smtp.Send(mail);
            smtp.Dispose();
            if (stream != null)
            {
                stream.Dispose();
            }
        }
        public static void SendAlert(IConfiguration _configuration, string From, string Subject, string Body, string To)
        {
            try
            {
                string UserID, Password, SMTPPort, Host;
                MailService.AppSettings(_configuration, out UserID, out Password, out SMTPPort, out Host);
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.To.Add(To);
                mail.From = new MailAddress(From);
                mail.Subject = Subject;
                mail.Body = Body;
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = Host;
                smtp.Port = Convert.ToInt16(SMTPPort);
                smtp.Credentials = new NetworkCredential(UserID, Password);
                smtp.EnableSsl = true;

                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static void RepSendEmail(string From, string Subject, string Body, string To, string CustSer, string UserID, string Password, string SMTPPort, string Host)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(To);
            mail.To.Add(CustSer);
            mail.From = new MailAddress(From);
            mail.Subject = Subject;
            mail.Body = Body;
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = Host;
            smtp.Port = Convert.ToInt16(SMTPPort);
            smtp.Credentials = new NetworkCredential(UserID, Password);
            smtp.EnableSsl = true;

            smtp.Send(mail);
        }
    }
}
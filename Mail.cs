using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.IO;
using System.Net;

namespace Test001
{
    class Mail
    {
        public string SmtpServer = "mail.pyv.com.vn";

        //without password
        //can not gmail-gmail
        public void SendMailBody(string Title, string Body, string FromMail, string ToMail)
        {
            System.Net.Mail.MailMessage mymail = new System.Net.Mail.MailMessage(FromMail, ToMail);
            mymail.Subject = Title;
            mymail.Body = Body;
            mymail.IsBodyHtml = true;
            mymail.Priority = MailPriority.High;
            mymail.BodyEncoding = Encoding.UTF8;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = SmtpServer;
            smtp.Send(mymail);
        }

        //with password
        public void SendMailBody(string Title, string Body, Dictionary<string, string> FromMail, string ToMail)
        {
            MailMessage mymail = new MailMessage(FromMail["username"], ToMail);
            mymail.Subject = Title;
            mymail.Body = Body;
            mymail.IsBodyHtml = true;
            mymail.Priority = MailPriority.High;
            mymail.BodyEncoding = Encoding.UTF8;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(FromMail["username"], FromMail["password"]);
            smtp.Timeout = 9000;
            smtp.Send(mymail);
        }
    }
}

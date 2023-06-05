using System.Net;
using System.Net.Mail;

namespace Blog.Services
{
    public class EmailService
    {
        public bool Send(
            string toName,
            string toEmail,
            string subject,
            string body,
            string fromName = "Blog do Will",
            string fromEmail = "poderososwaggao@proton.me")
        {
            var smtpClient = new SmtpClient(Configuration.Smtp.Server,Configuration.Smtp.Port);
            
                smtpClient.Credentials = new NetworkCredential(Configuration.Smtp.UserName, Configuration.Smtp.Password);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;
                var mail = new MailMessage();

                mail.From = new MailAddress(fromEmail, fromName);
                mail.To.Add(new MailAddress(toEmail, toName));
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                try
                {
                    smtpClient.Send(mail);
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }

        }
    }
}

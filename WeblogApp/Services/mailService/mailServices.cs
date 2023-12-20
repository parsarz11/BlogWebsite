using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace WeblogApp.Services.mailService
{
    public class mailServices
    {


        public void SendEmail(string url, string emailAddress,string body = null)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("parsablogapp@gmail.com"));
            email.To.Add(MailboxAddress.Parse(emailAddress));
            email.Subject = "test eamil for Identity";
            if(body == null)
            {
                email.Body = new TextPart(TextFormat.Html) { Text = $"<a href='{url}'>this is test</a>" };
            }
            else
            {
                email.Body = new TextPart(TextFormat.Html) { Text = body};
            }
            




            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 465, true);
            smtp.Authenticate("parsablogapp@gmail.com", "aovu orhl xghf rkfw");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}

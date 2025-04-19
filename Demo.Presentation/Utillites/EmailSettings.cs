using System.Net;
using System.Net.Mail;

namespace Demo.Presentation.Utillites
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com", 587);
            Client.EnableSsl = true;
            //Client.Credentials = new NetworkCredential()
        }
    }
}

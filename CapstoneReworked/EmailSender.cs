using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace CapstoneReworked
{
    public class EmailSender
    {
        private const string From = "mullermk521@gmail.com";
        private const string Pass = "ClaimAcademy1!";
        private const string Host = "smtp.gmail.com";
        private const int Port = 587;

        public static void Send(string To, string Subject, string Body)
        {
            using (var client = new SmtpClient(Host, Port))
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(From, Pass);

                using (var message = new MailMessage())
                {
                    message.From = new MailAddress(From);
                    message.Subject = Subject;
                    message.Body = Body;
                    message.IsBodyHtml = false;

                    var toAddresses = To.Split(';');
                    foreach (var address in toAddresses)
                    {
                        message.To.Add(new MailAddress(address));
                    }

                    client.Send(message);
                }
            }
        }
    }
}
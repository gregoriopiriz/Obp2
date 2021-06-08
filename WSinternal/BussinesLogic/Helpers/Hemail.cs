using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Helpers
{
    public class Hemail
    {
        public static void Email(string body, string toEmail)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.live.com");
            MailMessage message = new MailMessage();

            smtpClient.Port = 25;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("alchy-mist.gh@hotmail.com", "EmpanadasDeShampoo");
            //smtpClient.Host = "smtp.live.com";
            //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            
            message.Body = body;
            message.From = new MailAddress("alchy-mist.gh@hotmail.com");
            message.Subject = "Actualizacion de Formularios";
            message.CC.Add(new MailAddress(toEmail));
            //message.SubjectEncoding = Encoding.UTF8;
            //message.BodyEncoding = Encoding.UTF8;
            //message.IsBodyHtml = true; //to make message body as html set true

            smtpClient.Send(message);
            /*try
            {
            }
            catch (Exception)
            {
                Console.WriteLine("error al enviar");
            }*/
        }
    }
}
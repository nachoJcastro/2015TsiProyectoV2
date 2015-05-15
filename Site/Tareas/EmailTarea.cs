using Crosscutting.EntityTareas;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Site.Tareas
{
   /* public class EmailTarea: IJob
    {
        public void EnviarMail (IJobExecutionContext context, Correo correo)
        {
            using (var message = new MailMessage("php.inmobiliaria2014@gmail.com","nachoj.castro@gmail.com"))
            {
                message.Subject = "Pruega";
                message.Body = "Enviado desde .NET.Hora de prueba " + DateTime.Now;
                using (SmtpClient client = new SmtpClient
                {
                    EnableSsl = true,
                    Host = "smtp.gmail.com",
                    Port = 587,
                    Credentials = new NetworkCredential("php.inmobiliaria2014@gmail.com ", "grupophp")
                })
                {
                    client.Send(message);
                }
            }
        }
    }*/


}

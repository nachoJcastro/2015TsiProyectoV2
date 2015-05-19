
using Crosscutting.EntityTareas;
using Quartz;
using System;
using System.Net;
using System.Net.Mail;

namespace Site.Tareas
{
   public class EmailTarea: IJob
    {
        public void Execute (IJobExecutionContext context)
        {
            using (var mensaje = new MailMessage("php.inmobiliaria2014@gmail.com","nachoj.castro@gmail.com"))
            {
                mensaje.Subject = "Pruega";
                mensaje.Body = "Enviado desde .NET.Hora de prueba " + DateTime.Now;
                using (SmtpClient client = new SmtpClient
                {
                    EnableSsl = true,
                    Host = "smtp.gmail.com",
                    Port = 587,
                    Credentials = new NetworkCredential("php.inmobiliaria2014@gmail.com ", "grupophp")
                })
                {
                    client.Send(mensaje);
                }
            }
        }
    }


}

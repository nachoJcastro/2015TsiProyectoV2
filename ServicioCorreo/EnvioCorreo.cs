using Crosscutting.EntityTareas;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace ServicioCorreo
{
    public class EnvioCorreo:IEnvioCorreo
    {
        public void enviarCorreos(List<Correo> correo){

            foreach (var item in correo)
            {
                try
                {
                    MailMessage mensaje = new MailMessage("tsi2015gr11@gmail.com ", item.destinatario);
                   
                    mensaje.Subject = item.asunto;
                    mensaje.Body = item.mensaje;
                    using (SmtpClient client = new SmtpClient
                    {
                        EnableSsl = true,
                        Host = "smtp.gmail.com",
                        Port = 587,
                        Credentials = new NetworkCredential("tsi2015gr11@gmail.com ", "tsi123456")
                    })
                    {
                        client.Send(mensaje);
                    }
                }
                catch (Exception)
                {
                    
                    throw;
                }
               

            }


        
        }
    }
}

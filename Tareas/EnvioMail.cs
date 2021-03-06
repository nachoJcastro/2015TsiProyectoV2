﻿using Crosscutting.EntityTareas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tareas
{
    public class EnvioMail:IEnvioMail
    {
        public void Execute(IJobExecutionContext context)
        {
            using (var message = new MailMessage("user@gmail.com", "user@live.co.uk"))
            {
                message.Subject = "Test";
                message.Body = "Test at " + DateTime.Now;
                using (SmtpClient client = new SmtpClient
                {
                    EnableSsl = true,
                    Host = "smtp.gmail.com",
                    Port = 587,
                    Credentials = new NetworkCredential("user@gmail.com", "password")
                })
                {
                    client.Send(message);
                }
            }
        }


    }
}

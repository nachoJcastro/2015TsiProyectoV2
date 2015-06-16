
using BusinessLogicLayer;
using BusinessLogicLayer.Controllers;
using BusinessLogicLayer.TenantControllers;
using BusinessLogicLayer.TenantInterfaces;
using Crosscutting.EntityTareas;
using Quartz;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace Site.Tareas
{
   public class FinalizarTarea: IJob
    {
        public void Execute (IJobExecutionContext context)
        {

            System.Diagnostics.Debug.WriteLine("entro Finalizar Subasta por tiempo");

            try
            {
                IBLSubasta subIBL = new BLSubasta();
                IBLTiendaVirtual tiendas = new BLTiendaVirtual();

                List<String> tenants = tiendas.ObtenerTenants();
                
                
                System.Diagnostics.Debug.WriteLine("Tenants = "+ tenants.ToString());

                foreach (var item in tenants)
                {
                    System.Diagnostics.Debug.WriteLine("Antes  de finalizar subastas en tenant = " + item.ToString());
                    subIBL.FinalizarSubastasTarea(item.ToString());
                    System.Diagnostics.Debug.WriteLine("Despues de finalizar subastas en tenant = " + item.ToString());
                }


            }
            catch (Exception)
            {
                
                throw;
            } 
            

           /* using (var mensaje = new MailMessage("php.inmobiliaria2014@gmail.com","nachoj.castro@gmail.com"))
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
            }*/
       
        
        }
    }


}

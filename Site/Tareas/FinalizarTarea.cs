
using BusinessLogicLayer;
using BusinessLogicLayer.Controllers;
using BusinessLogicLayer.TenantControllers;
using BusinessLogicLayer.TenantInterfaces;
using Crosscutting.EntityTareas;
using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace Site.Tareas
{
   public class FinalizarTarea: IJob
    {
       private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public void Execute (IJobExecutionContext context)
        {
            log.Info("entro Finalizar Subasta por tiempo");
            //System.Diagnostics.Debug.WriteLine("entro Finalizar Subasta por tiempo");

            try
            {
                IBLSubasta subIBL = new BLSubasta();
                IBLTiendaVirtual tiendas = new BLTiendaVirtual();

                List<String> tenants = tiendas.ObtenerTenants();

                log.Info("Tenants = " + tenants.ToString());
                //System.Diagnostics.Debug.WriteLine("Tenants = "+ tenants.ToString());

                foreach (var item in tenants)
                {
                    log.Info("Antes  de finalizar subastas en tenant = " + item.ToString());
                    subIBL.FinalizarSubastasTarea(item.ToString());
                    log.Info("Despues de finalizar subastas en tenant = " + item.ToString());
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

            log.Info("SALGO Finalizar Subasta por tiempo");
        }
    }


}

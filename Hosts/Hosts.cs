using Microsoft.Web.Administration;
using System;
using System.IO;
using System.Text;

namespace DNSManager
{
    public class Hosts : IHosts
    {
        public void AgregarHost(String dominio)
        {
            try
            {
                Console.WriteLine("Dentro de Hosts");
                var systemPath = Environment.GetFolderPath(Environment.SpecialFolder.System);
                var path = Path.Combine(systemPath, @"drivers\etc\hosts");
                using (var stream = new StreamWriter(path, true, Encoding.Default))
                {
                    stream.WriteLine(@"127.0.0.1    " + dominio + ".chebay.com");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void AgregarSitio(String dominio)
        {
            string ruta = @"C:\inetpub\wwwroot\Site";

            ServerManager manager = new ServerManager();
            try
            {   // Add this site.
                Site hrSite = manager.Sites.Add(dominio.ToUpper(), "http", "*:80:" + dominio + ".chebay.com", ruta);
                // The site will need to be started manually.
                hrSite.ServerAutoStart = true;
                manager.CommitChanges();
                //Console.WriteLine("Site " + dominio.ToUpper() + " added to ApplicationHost.config file.");
            }
            catch (Exception e)
            {
                throw e;
                // A site with this binding already exists. Do not attempt
                // to add a duplicate site.
            }

        }


    }
}

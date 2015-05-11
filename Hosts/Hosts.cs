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
                    stream.WriteLine(@"127.0.0.1    "+dominio+".chebay.com");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }

}

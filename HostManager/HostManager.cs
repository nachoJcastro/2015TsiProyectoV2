using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostManager
{
    class HostManager
    {
        public void AgregarHost(String dominio)
        {
            var systemPath = Environment.GetFolderPath(Environment.SpecialFolder.System);
            Console.WriteLine(systemPath);
            var path = Path.Combine(systemPath, @"drivers\etc\hosts");
            using (var stream = new StreamWriter(path, true, Encoding.Default))
            {
                stream.WriteLine(@"\n 127.0.0.1 \t sitio\"+dominio);
            }
            Console.ReadKey();
        }
    }
}

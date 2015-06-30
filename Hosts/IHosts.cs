
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSManager
{
    public interface IHosts
    {
        //Operaciones 
        void AgregarHost(String dominio); 

        void AgregarSitio(String dominio);
      
    }
}

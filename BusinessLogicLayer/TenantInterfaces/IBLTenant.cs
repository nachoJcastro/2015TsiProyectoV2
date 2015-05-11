using Crosscutting.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.TenantInterfaces
{
    public interface IBLTenant
    {
        //Creo tenant sitio
        void AgregarTenant(String dominio);
        //Agrego tenant a los hosts
        void AgregarHost (String dominio);
        // Existe Sitio
        Boolean ExisteSitio (String dominio);
    }
}

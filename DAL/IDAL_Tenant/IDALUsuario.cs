using Crosscutting.EntityTenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IDAL_Tenant
{
    public interface IDALUsuario
    {
         Boolean ExisteUsuario(String tenant, String email);
         void RegistrarUsuario(String tenant, Usuario usr);
         Usuario LoginUsuario(String tenant, String email, String password);
    }
}

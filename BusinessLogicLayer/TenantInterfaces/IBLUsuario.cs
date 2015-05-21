using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crosscutting.Entity;
using Crosscutting.EntityTenant;

namespace BusinessLogicLayer.TenantInterfaces
{
    public interface IBLUsuario
    {
        Boolean ExisteUsuario(String tenant , String email);
        void RegistrarUsuario(String tenant, Usuario usr);
        Usuario LoginUsuario(String valor_tenant, String email, String password);
        int ObtenerIdByEmail(String tenant, String email);

        string GetNombreUsuario(string valor_tenant, int idUsuario);
        Usuario GetUsuario(string valor_tenant, int idUsuario);
    }
}

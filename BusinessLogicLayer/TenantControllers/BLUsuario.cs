using BusinessLogicLayer.TenantInterfaces;
using DAL.DAL_Tenant;
using DAL.IDAL_Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crosscutting.EntityTenant;

namespace BusinessLogicLayer.TenantControllers
{
    public class BLUsuario : IBLUsuario
    {
        private IDALUsuario _dal = new DALUsuario();

        public Boolean ExisteUsuario(String tenant,String email) {
            return _dal.ExisteUsuario(tenant , email);
        }

        public void RegistrarUsuario(String tenant, Usuario usr)
        {
             _dal.RegistrarUsuario(tenant, usr);
        }

        public Usuario LoginUsuario(String valor_tenant, String email,String password){

            return _dal.LoginUsuario(valor_tenant, email, password);
        }

        public int ObtenerIdByEmail(String tenant, String email)
        {

            return _dal.obtenerIdByEmail(tenant,email);
        }

        public string GetNombreUsuario(string valor_tenant, int idUsuario) {

            return _dal.GetNombreUsuario(valor_tenant, idUsuario);
        }

        public Usuario GetUsuario(string valor_tenant, int idUsuario)
        {
            return _dal.GetUsuario(valor_tenant, idUsuario);
        }

        public void ActualizarUsuario(string tenant, Usuario usuarioNuevo)
        {
            _dal.ActualizarUsuario(tenant, usuarioNuevo);
        }
    }
}

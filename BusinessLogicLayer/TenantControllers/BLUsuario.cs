using BusinessLogicLayer.TenantInterfaces.User;
using DAL.DAL_Tenant;
using DAL.IDAL_Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crosscutting.EntityTenant;

namespace BusinessLogicLayer.TenantControllers.User
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
    }
}

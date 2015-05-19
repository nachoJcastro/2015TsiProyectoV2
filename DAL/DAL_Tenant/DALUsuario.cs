using Crosscutting.EntityTenant;
using DAL.Contextos;
using DAL.IDAL_Tenant;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.DAL_Tenant
{
    public class DALUsuario : IDALUsuario
    {
        //Thread
        //static TenantDB sitio = new TenantDB(Thread.CurrentThread.Name);

        public Boolean ExisteUsuario(String tenant, String email)
        {
            Boolean retorno = false;
            System.Diagnostics.Debug.WriteLine("Registro de usuario en tenant :"+tenant+ "Email" + email);
            try
            {
                TenantDB sitio = new TenantDB(tenant);
                if (sitio != null)
                {
                    var usuario = sitio.Usuario.FirstOrDefault(r => r.email == email);
                    if (usuario == null) retorno = false;
                    else retorno = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        public void RegistrarUsuario(String tenant, Usuario user)
        {
            try
            {
                TenantDB sitio = new TenantDB(tenant);
                if (sitio != null)
                {
                    sitio.Usuario.Add(user);
                    sitio.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario LoginUsuario(String tenant, String email, String password)
        {
            Usuario usr = null;
            try
            {
                TenantDB sitio = new TenantDB(tenant);
                if (sitio != null)
                {
                    var usuario = sitio.Usuario.FirstOrDefault(r => r.email == email && r.password == password);
                    if (usuario != null) usr = (Usuario)usuario;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return usr;
        }

        public int obtenerIdByEmail(String tenant, String email)
        {
            int id = 0;
            try
            {
                TenantDB sitio = new TenantDB(tenant);
                if (sitio != null)
                {
                    var usuario = sitio.Usuario.FirstOrDefault(r => r.email == email);
                    if (usuario != null) id = usuario.id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return id;
        }


        public string GetNombreUsuario(string tenant, int idUsuario) {

            String nombre = "";    
            try
            {
                TenantDB sitio = new TenantDB(tenant);
                if (sitio != null)
                {
                    var usuario = sitio.Usuario.FirstOrDefault(r => r.id == idUsuario);
                    if (usuario != null) nombre = usuario.nick;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return nombre;
        
        
        
        }
    }
}



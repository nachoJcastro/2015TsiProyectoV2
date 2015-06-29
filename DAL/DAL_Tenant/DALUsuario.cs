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

        public Usuario GetUsuario(string valor_tenant, int idUsuario)
        {
            var usuario = new Usuario();
            try
            {
                TenantDB sitio = new TenantDB(valor_tenant);
                if (sitio != null)
                {
                    usuario = sitio.Usuario.FirstOrDefault(r => r.id == idUsuario);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
                return usuario;
            }

        public void ActualizarUsuario(string tenant, Usuario usuarioNuevo)
        {
            try
            {
                TenantDB db = new TenantDB(tenant);
                var usuario = db.Usuario.FirstOrDefault(u => u.id == usuarioNuevo.id);
                if (usuario != null)
                {
                    usuario.apellido = usuarioNuevo.apellido;
                    usuario.billetera = usuarioNuevo.billetera;
                    usuario.Calificacion = usuarioNuevo.Calificacion;
                    //usuario.Comentario = usuarioNuevo.Comentario;
                    usuario.direccion = usuarioNuevo.direccion;
                    usuario.email = usuarioNuevo.email;
                    //usuario.Favorito = usuarioNuevo.Favorito;
                    usuario.fecha_Alta = usuarioNuevo.fecha_Alta;
                    usuario.fecha_Nacimiento = usuarioNuevo.fecha_Nacimiento;
                    usuario.id = usuarioNuevo.id;
                    usuario.imagen = usuarioNuevo.imagen;
                    usuario.nick = usuarioNuevo.nick;
                    usuario.nombre = usuarioNuevo.nombre;
                    usuario.Oferta = usuarioNuevo.Oferta;
                    usuario.password = usuarioNuevo.password;
                    usuario.reputacion_Compra = usuarioNuevo.reputacion_Compra;
                    usuario.reputacion_Venta = usuarioNuevo.reputacion_Venta;
                    //usuario.Subasta = usuarioNuevo.Subasta;

                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}



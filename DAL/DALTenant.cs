using AutoMapper;
using Crosscutting.Entity;
using DAL.Contextos;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crosscutting.EntityTenant;


namespace DAL
{
    public class DALTenant : IDALTenant
    {
        static BackendDB back = new BackendDB();
        // Se crea una DB por tenant
        public void AgregarTenant(String dominio)
        {
            try
            {
                TenantDB db = new TenantDB(dominio);
                db.Database.CreateIfNotExists();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        // Se busca si existe Sitio
        public Boolean ExisteSitio(String dominio)
        {   
            bool retorno=false;
            
            try
            {   
                BackendDB back = new BackendDB();
                var tienda = back.TiendaVirtual.FirstOrDefault(r => r.Dominio == dominio);
                if (tienda == null) retorno = false;
                else retorno = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retorno;
        }

        public int ObtenerIdTenant(String tenant)
        {
            int tiendaID;
           try
            {
                BackendDB back = new BackendDB();
                var tienda = back.TiendaVirtual.FirstOrDefault(r => r.Dominio == tenant);
                tiendaID = tienda.TiendaVitualId;
            }
            catch (Exception ex)
            {
                throw ex;
            }

           return tiendaID;
        }

        public TiendaTenant ObtenerDatosTenant(int idTienda)
        {
            TiendaTenant tiendaInfo = new TiendaTenant();
            try
            {
                BackendDB back = new BackendDB();
                var tienda = back.TiendaVirtual.FirstOrDefault(r => r.TiendaVitualId == idTienda);
                tiendaInfo.Nombre = tienda.Nombre;
                tiendaInfo.Css = tienda.Css;
                tiendaInfo.Descripcion = tienda.Descripcion;
                tiendaInfo.Logo = tienda.Logo;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return tiendaInfo;
        }
    }
}

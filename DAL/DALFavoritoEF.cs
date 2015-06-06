using Crosscutting.EntityTenant;
using DAL.Contextos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALFavoritoEF : IDALFavorito
    {
        public DALFavoritoEF() { }

        public void AgregarFavorito(String tenant, Favorito favorito)
        {
            try
            {   
                TenantDB db = new TenantDB(tenant);
                db.Favorito.Add(favorito);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public Favorito ObtenerFavorito(String tenant, int idSubasta, int idUsuario)
        {
            try
            {
                TenantDB db = new TenantDB(tenant);
                var favorito = db.Favorito.FirstOrDefault(f => f.id_Subasta == idSubasta && f.id_Usuario == idUsuario);

                return favorito;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public bool esFavorito(String tenant, int idSubasta, int idUsuario)
        {
            try
            {
                TenantDB db = new TenantDB(tenant);
                var favorito = db.Favorito.FirstOrDefault(f => f.id_Subasta == idSubasta && f.id_Usuario == idUsuario);

                if (favorito == null)
                    return false;
                else
                    return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<Favorito> FavoritosByUsuario(String tenant, int idUsuario)
        {
            try
            {
                TenantDB db = new TenantDB(tenant);
                List<Favorito> favoritos = db.Favorito.Where(f => f.id_Usuario == idUsuario).ToList();
                
                return favoritos;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void EliminarFavorito(String tenant, int idSubasta, int idUsuario)
        {
            try
            {
                TenantDB db = new TenantDB(tenant);
                var favorito = db.Favorito.FirstOrDefault(f => f.id_Subasta == idSubasta && f.id_Usuario == idUsuario);
                if (favorito != null)
                {
                    db.Favorito.Remove(favorito);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

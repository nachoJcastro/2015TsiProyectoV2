using BusinessLogicLayer.TenantInterfaces;
using Crosscutting.EntityTenant;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.TenantControllers
{
    public class BLFavorito : IBLFavorito
    {
        private IDALFavorito _dal = new DALFavoritoEF();


        public BLFavorito(IDALFavorito dal)
        {
            this._dal = dal;
        }

        public BLFavorito() { }

        public void AgregarFavorito(String tenant, Favorito favorito)
        {
            _dal.AgregarFavorito(tenant, favorito);
        }


        public Favorito ObtenerFavorito(String tenant, int idSbasta, int idUsuario)
        {
            return _dal.ObtenerFavorito(tenant, idSbasta, idUsuario);
        }


        public bool esFavorito(String tenant, int idSbasta, int idUsuario)
        {
            return _dal.esFavorito(tenant, idSbasta, idUsuario);
        }


        public List<Favorito> FavoritosByUsuario(String tenant, int idUsuario)
        {
            return _dal.FavoritosByUsuario(tenant, idUsuario);
        }


        public void EliminarFavorito(String tenant, int idSubasta, int idUsuario)
        {
            _dal.EliminarFavorito(tenant, idSubasta, idUsuario);
        }
        
    }
}

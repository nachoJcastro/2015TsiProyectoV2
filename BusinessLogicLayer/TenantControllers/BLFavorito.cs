using BusinessLogicLayer.TenantInterfaces;
using Crosscutting.EntityTenant;
using Crosscutting.Struct;
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


        public List<Subasta> SubastasFavoritasByUsuario(String tenant, int idUsuario)
        {
            IBLSubasta subIBL = new BLSubasta();
            var favoritos = _dal.FavoritosByUsuario(tenant, idUsuario);

            List<Subasta> subastas = new List<Subasta>();
            foreach (var fav in favoritos)
            {
                subastas.Add(subIBL.ObtenerSubasta(tenant, fav.id_Subasta));
            }
            return subastas;
        }

        public List<TprodXCant> obtenerTopNtipoProdFav(String tenant, int idUsuario, int N)
        {
            IBLFavorito favIBL = new BLFavorito();
            IBLSubasta subIBL = new BLSubasta();

            List<int> tipoProductos = new List<int>();

            var favoritos = favIBL.FavoritosByUsuario(tenant, idUsuario);

            foreach (var item in favoritos)
            {
                var subasta = subIBL.ObtenerSubasta(tenant, item.id_Subasta);
                tipoProductos.Add(subasta.id_Producto);
            }

            List<TprodXCant> ocurrencias = new List<TprodXCant>();

            int idTipoP;

            for (int i = 0; i < tipoProductos.Count; i++)//cuento los tProductos de los favoritos
            {
                idTipoP = tipoProductos[i];
                var pertenece = false;
                List<TprodXCant> ocurrenciasAux = new List<TprodXCant>(ocurrencias); ;

                foreach (var elemento in ocurrenciasAux)
                {
                    if (elemento.idTProd == idTipoP)
                    {
                        int cantidadActual = elemento.cantidad;
                        ocurrencias[elemento.posicion] = (new TprodXCant(elemento.posicion, idTipoP, cantidadActual + 1));
                        pertenece = true;
                    }
                }
                if (!pertenece)
                {
                    ocurrencias.Add(new TprodXCant(ocurrencias.Count(), idTipoP, 1));
                }
            }

            List<TprodXCant> filtrada = new List<TprodXCant>(ocurrencias.OrderByDescending(s => s.cantidad).Take(N));

            return filtrada;
        }
    }
}

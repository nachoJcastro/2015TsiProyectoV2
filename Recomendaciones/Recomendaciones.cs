using BusinessLogicLayer.TenantControllers;
using BusinessLogicLayer.TenantInterfaces;
using Crosscutting.EntityTenant;
using Crosscutting.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recomendaciones
{
    public abstract class Recomendaciones
    {

        public List<Subasta> AlgRecomen(String tenant, int idUsuario)
        {
            IBLFavorito favIBL = new BLFavorito();

            List<TprodXCant> tipoProds = favIBL.obtenerTopNtipoProdFav(tenant, idUsuario, 3);
            
            List<Subasta> prods = getRecomendaciones(tenant, idUsuario, tipoProds, 6);

            return prods;
        }


        public abstract List<Subasta> getRecomendaciones(String tenant, int idUsuario, List<TprodXCant> tipoProds, int cantRetorno);

    }
}

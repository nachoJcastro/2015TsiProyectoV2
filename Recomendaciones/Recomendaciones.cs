using BusinessLogicLayer;
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

        public List<Subasta> AlgRecomen(int tenantId, int idUsuario)
        {
            IBLTenant tiendaIBL = new BLTenant();
            var tenant = tiendaIBL.ObtenerDatosTenant(tenantId);
            IBLFavorito favIBL = new BLFavorito();

            List<TprodXCant> tipoProds = favIBL.obtenerTopNtipoProdFav(tenant.Nombre, idUsuario, 3);
            
            List<Subasta> prods = getRecomendaciones(tenantId, idUsuario, tipoProds, 6);

            return prods;
        }


        public abstract List<Subasta> getRecomendaciones(int tenant, int idUsuario, List<TprodXCant> tipoProds, int cantRetorno);

    }
}

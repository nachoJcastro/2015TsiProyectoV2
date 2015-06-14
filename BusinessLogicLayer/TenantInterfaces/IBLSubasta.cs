using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crosscutting.EntityTenant;
using Crosscutting.EntityTareas;

namespace BusinessLogicLayer.TenantInterfaces
{
    public interface IBLSubasta
    {
        //Operaciones CRUD
        void AgregarSubasta(String tenant,Subasta subasta);
        Subasta ObtenerSubasta(String tenant, int subastaId);
        List<Subasta> ObtenerSubastas(String tenant);
        void ActualizarSubasta(String tenant, Subasta subasta);
        void EliminarSubasta(int subastaId);

        List<Oferta> ObtenerOfertas(int subastaId);
        List<Subasta> ObtenerSubastasByTipoProducto(String tenant, int idTipoProducto);

        void AltaSubasta(string valor_tenant, Subasta subasta);

        Boolean ActualizarMonto(string valor_tenant, int id_subasta, double monto);

        void FinalizarSubastasTarea(String tenant);
        List<Subasta> ObtenerSubastasActivas(String tenant);

        void correoCompraDirecta(String tenant, Subasta sub);

        void AgregarImagen(string tenant, Imagen img);
        List<Imagen> ObtenerImagenes(string tenant, int id);

        int AgregarSubasta_ID(String tenant, Subasta subasta);

        List<Subasta> ObtenerSubastasActivasxCategoria(string tenant, int idCat);
        List<Subasta> ObtenerSubastasPorCriterio(string tenant, int idCat, string criterio, int? tipo,string min,string max);
    }
}

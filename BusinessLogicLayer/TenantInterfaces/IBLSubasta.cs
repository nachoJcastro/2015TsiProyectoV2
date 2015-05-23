using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crosscutting.EntityTenant;

namespace BusinessLogicLayer.TenantInterfaces
{
    public interface IBLSubasta
    {
        //Operaciones CRUD
        void AgregarSubasta(String tenant,Subasta subasta); //Insert
        Subasta ObtenerSubasta(String tenant, int subastaId); //FindOne
        List<Subasta> ObtenerSubastas(String tenant);//FindAllAs
        void ActualizarSubasta(String tenant, Subasta subasta); //Update
        void EliminarSubasta(int subastaId); //Delete

        List<Oferta> ObtenerOfertas(int subastaId);
        //void FinalizarSubastaPorTiempo(String tenant,int subastaId);
        //void FinalizarSubastaCompraDirecta(String tenant, int subastaId);

        void AltaSubasta(string valor_tenant, Subasta subasta);

        Boolean ActualizarMonto(string valor_tenant, int id_subasta, double monto);

        //void FinalizarSubastaTiempo();

        void FinalizarSubastasTarea(String tenant);
    }
}

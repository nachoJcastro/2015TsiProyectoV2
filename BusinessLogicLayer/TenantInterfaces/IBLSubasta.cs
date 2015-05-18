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
        Subasta ObtenerSubasta(int subastaId); //FindOne
        List<Subasta> ObtenerSubastas(String tenant);//FindAllAs
        void ActualizarSubasta(Subasta subasta); //Update
        void EliminarSubasta(int subastaId); //Delete

        List<Oferta> ObtenerOfertas(int subastaId);
        void FinalizarSubastaPorTiempo(int subastaId);
        void FinalizarSubastaCompraDirecta(int subastaId);

        void AltaSubasta(string valor_tenant, Subasta subasta);
    }
}

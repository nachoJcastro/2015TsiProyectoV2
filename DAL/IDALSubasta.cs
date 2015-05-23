using Crosscutting.EntityTenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDALSubasta
    {
        void AgregarSubasta(String tenant,Subasta subasta); //Create
        Subasta ObtenerSubasta(String tenant,int subastaId); //Read
        List<Subasta> ObtenerSubastas(String tenant);
        void ActualizarSubasta(String tenant, Subasta subasta); //Update
        void EliminarSubasta(int subastaId); //Delete

        List<Oferta> ObtenerOfertas(int subastaId);

        void AltaSubasta(string tenant, Subasta subasta);
        Boolean ActualizarMonto(string valor_tenant, int id_subasta, double monto);
    }
}

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
        Subasta ObtenerSubasta(int subastaId); //Read
        List<Subasta> ObtenerSubastas(String tenant);
        void ActualizarSubasta(Subasta subasta); //Update
        void EliminarSubasta(int subastaId); //Delete

        List<Oferta> ObtenerOfertas(int subastaId);

        void AltaSubasta(string tenant, Subasta subasta);
    }
}

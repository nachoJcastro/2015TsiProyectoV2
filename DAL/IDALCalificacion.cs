using Crosscutting.EntityTenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDALCalificacion
    {

        void AgregarCalificacion(String tenant, CalificacionMongo calificacion);
        CalificacionMongo ObtenerCalificacion(String tenant, int calificacionId);
        List<CalificacionMongo> ObtenerCalificacionesComprador(String tenant, int usuarioId);
        List<CalificacionMongo> ObtenerCalificacionesVendedor(String tenant, int usuarioId);

        CalificacionMongo ObtenerCalificacionDelVendedor(String tenant, int subastaId);
        CalificacionMongo ObtenerCalificacionDelComprador(String tenant, int subastaId);

    }
}

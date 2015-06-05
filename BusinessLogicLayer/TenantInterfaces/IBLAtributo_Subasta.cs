using Crosscutting.EntityTenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.TenantInterfaces
{
    public interface IBLAtributo_Subasta
    {
        void AgregarAtributo_Subasta(String tenant, Atributo_Subasta atributo);
        //Atributo_Subasta ObtenerAtributo_Subasta(String tenant, int IdAtributo_Subasta);
        Atributo_Subasta ObtenerAtributo_Subasta(String tenant, int IdSubasta, int IdAtributo);
        List<Atributo_Subasta> ObtenerAtributos_Subasta(String tenant);
        void ActualizarAtributo_Subasta(String tenant, Atributo_Subasta atributo);
        void EliminarAtributo_Subasta(String tenant, int atributoId, int idSubasta);
    }
}

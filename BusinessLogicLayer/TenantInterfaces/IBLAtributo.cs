using Crosscutting.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.TenantInterfaces
{
    public interface IBLAtributo
    {
        void AgregarAtributo(AtributosDTO atributoDTO); //create
        AtributosDTO ObtenerAtributo(int atributoId); //find
        List<AtributosDTO> ObtenerAtributos();//findall
        List<AtributosDTO> ObtenerAtributosPorCategoria(int idCat);
        void ActualizarAtributo(AtributosDTO atributoDTO); //update
        void EliminarAtributo(int atributoId); //Delete
    }
}

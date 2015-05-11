using Crosscutting.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDALAtributos
    {
        void AgregarAtributo(AtributosDTO atributoDTO); //Create
        AtributosDTO ObtenerAtributo(int atributoId); //Read
        List<AtributosDTO> ObtenerAtributos();
        List<AtributosDTO> ObtenerAtributosPorCategoria(int idCat);
        void ActualizarAtributo(AtributosDTO atributoDTO); //Update
        void EliminarAtributo(int atributoId); //Delete

    }
}

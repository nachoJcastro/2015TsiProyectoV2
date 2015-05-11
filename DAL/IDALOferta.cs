using Crosscutting.EntityTenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDALOferta
    {
        void AgregarOferta(Oferta oferta); //Create
        Oferta ObtenerOferta(int ofertaId); //Read
        List<Oferta> ObtenerOfertas();
        void ActualizarOferta(Oferta oferta); //Update
        void EliminarOferta(int ofertaId); //Delete
    }
}

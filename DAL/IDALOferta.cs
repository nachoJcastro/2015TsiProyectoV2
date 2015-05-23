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
        void AgregarOferta(String tenant,Oferta oferta); //Create
        Oferta ObtenerOferta(int ofertaId); //Read
        List<Oferta> ObtenerOfertas();
        void ActualizarOferta(Oferta oferta); //Update
        void EliminarOferta(int ofertaId); //Delete

        List<Oferta> ObtenerOfertasByProducto(String tenant,int id_subasta);
    }
}

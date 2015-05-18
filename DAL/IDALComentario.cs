using Crosscutting.EntityTenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDALComentario
    {
        void AgregarComentario(String tenant, Comentario comenario); //Create
        Comentario ObtenerComentario(String tenant,int comentarioId); //Read

        //List<Comentario> ComentariosByProducto(String tenant ,int productoId);
    }
}

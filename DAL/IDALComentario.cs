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
        void AgregarComentario(Comentario comenario); //Create
        Comentario ObtenerComentario(int comentarioId); //Read

        List<Comentario> ComentariosByProducto(int productoId);
    }
}

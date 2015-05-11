using Crosscutting.EntityTenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.TenantInterfaces
{
    public interface IBLComentario
    {
        void AgregarComentario(Comentario comentario); //Insert
        Comentario ObtenerComentario(int comentarioId); //FindOne

        List<Comentario> ComentariosByProducto(int productoId); 
    }
}

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
        void AgregarComentario(String tenant,Comentario comentario); //Insert
        Comentario ObtenerComentario(String tenant, int comentarioId); //FindOne

        List<Comentario> ComentariosByProducto(String tenant, int productoId); 
    }
}

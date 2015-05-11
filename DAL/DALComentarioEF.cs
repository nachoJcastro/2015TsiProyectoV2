using Crosscutting.EntityTenant;
using DAL.Contextos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALComentarioEF : IDALComentario
    {
        static TenantDB db = new TenantDB("falta pasarle el string database");

        public DALComentarioEF() { }

        public void AgregarComentario(Comentario comentario)
        {
            try
            {
                db.Comentario.Add(comentario);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Comentario ObtenerComentario(int comentarioId)
        {
            try
            {
                var comentario = db.Comentario.FirstOrDefault(c => c.id == comentarioId);

                return comentario;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<Comentario> ComentariosByProducto(int productoId)
        {
            var listaCom = new List<Comentario>();

            try
            {
                listaCom = db.Comentario.Where(c => c.producto.id == productoId).ToList();

                return listaCom;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

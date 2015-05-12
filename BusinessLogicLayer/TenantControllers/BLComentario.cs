using BusinessLogicLayer.TenantInterfaces;
using Crosscutting.EntityTenant;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.TenantControllers
{
    public class BLComentario : IBLComentario
    {
        private IDALComentario _dal = new DALComentarioEF();


        public BLComentario(IDALComentario dal)
        {
            this._dal = dal;
        }

        public BLComentario() { }


        public void AgregarComentario(Comentario comentario)
        {
            _dal.AgregarComentario(comentario);
        }


        public Comentario ObtenerComentario(int comentarioId)
        {
            return _dal.ObtenerComentario(comentarioId);
        }

        public List<Comentario> ComentariosByProducto(int productoId)
        {
            return _dal.ComentariosByProducto(productoId);
        }

    }
}

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


        public void AgregarComentario(String tenant,Comentario comentario)
        {
            _dal.AgregarComentario(tenant,comentario);
        }


        public Comentario ObtenerComentario(String tenant ,int comentarioId)
        {
            return _dal.ObtenerComentario(tenant,comentarioId);
        }

        public List<Comentario> ComentariosByProducto(String tenant, int productoId)
        {
            return new List<Comentario>();
            
            //return _dal.ComentariosByProducto(productoId);
        }

    }
}

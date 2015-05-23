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
    public class BLOferta : IBLOferta
    {
        private IDALOferta _dal = new DALOfertaEF();


        public BLOferta(IDALOferta dal)
        {
            this._dal = dal;
        }

        public BLOferta() { }


        public void AgregarOferta(String tenant,Oferta oferta)
        {
            _dal.AgregarOferta(tenant,oferta);
        }


        public Oferta ObtenerOferta(int ofertaId)
        {
            return _dal.ObtenerOferta(ofertaId);
        }


        public List<Oferta> ObtenerOfertas()
        {
            return _dal.ObtenerOfertas();
        }


        public void ActualizarOferta(Oferta oferta)
        {
            _dal.ActualizarOferta(oferta);
        }


        public void EliminarOferta(int ofertaId)
        {
            _dal.EliminarOferta(ofertaId);
        }

        public List<Oferta> ObtenerOfertasByProducto(String tenant,int id_subasta)
        {
            return _dal.ObtenerOfertasByProducto(tenant,id_subasta);
        }

    }
}

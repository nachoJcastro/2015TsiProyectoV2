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
    class BLOferta : IBLOferta
    {

        private IDALOferta _dal = new DALOfertaEF();


        public BLOferta(IDALOferta dal)
        {
            this._dal = dal;
        }

        public BLOferta() { }


        public void AgregarOferta(Oferta oferta)
        {
            _dal.AgregarOferta(oferta);
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
    }
}

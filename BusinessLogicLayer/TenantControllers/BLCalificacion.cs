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
    public class BLCalificacion : IBLCalificacion
    {

        private IDALCalificacion _dal = new DALCalificacionEF();


        public BLCalificacion(IDALCalificacion dal)
        {
            this._dal = dal;
        }


        public BLCalificacion() { }


        public void AgregarCalificacion(String tenant, CalificacionMongo calificacion)
        {
            _dal.AgregarCalificacion(tenant, calificacion);
        }


        public CalificacionMongo ObtenerCalificacion(String tenant, int calificacionId)
        {
            return _dal.ObtenerCalificacion(tenant, calificacionId);
        }


        public List<CalificacionMongo> ObtenerCalificacionesComprador(String tenant, int usuarioId)
        {
            return _dal.ObtenerCalificacionesComprador(tenant, usuarioId);
        }


        public List<CalificacionMongo> ObtenerCalificacionesVendedor(String tenant, int usuarioId)
        {
            return _dal.ObtenerCalificacionesVendedor(tenant, usuarioId);
        }


        public CalificacionMongo ObtenerCalificacionDelVendedor(String tenant, int subastaId)
        {
            return _dal.ObtenerCalificacionDelVendedor(tenant, subastaId);
        }


        public CalificacionMongo ObtenerCalificacionDelComprador(String tenant, int subastaId)
        {
            return _dal.ObtenerCalificacionDelComprador(tenant, subastaId);
        }
    }
}

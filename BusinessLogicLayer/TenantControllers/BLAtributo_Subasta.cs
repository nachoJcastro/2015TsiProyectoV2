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
    public class BLAtributo_Subasta : IBLAtributo_Subasta
    {
        private IDALAtributo_Subasta _dal = new DALAtributo_SubastaEF();

        public BLAtributo_Subasta(IDALAtributo_Subasta dal)
        {
            this._dal = dal;
        }

        public BLAtributo_Subasta() { }

        public void AgregarAtributo_Subasta(String tenant, Atributo_Subasta atributo)
        {
            _dal.AgregarAtributo_Subasta(tenant, atributo);
        }

        //public Atributo_Subasta ObtenerAtributo_Subasta(String tenant, int IdAtributo_Subasta)
        //{
        //    return _dal.ObtenerAtributo_Subasta(tenant, IdAtributo_Subasta);
        //}

        public Atributo_Subasta ObtenerAtributo_Subasta(String tenant, int IdSubasta, int IdAtributo)
        {
            return _dal.ObtenerAtributo_Subasta(tenant, IdSubasta, IdAtributo);
        }

        public List<Atributo_Subasta> ObtenerAtributos_Subasta(String tenant)
        {
            return _dal.ObtenerAtributos_Subasta(tenant);
        }

        public void ActualizarAtributo_Subasta(String tenant, Atributo_Subasta atributo)
        {
            _dal.ActualizarAtributo_Subasta(tenant, atributo);
        }

        public void EliminarAtributo_Subasta(String tenant, int atributoId, int idSubasta)
        {
            _dal.EliminarAtributo_Subasta(tenant, atributoId, idSubasta);
        }
    }
}

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
    public class BLCategoria : IBLCategoria
    {
        private IDALCategoriaTenant _dal = new DALCategoriaTenantEF();

        public BLCategoria(IDALCategoriaTenant dal)
        {
            this._dal = dal;
        }

        public BLCategoria() { }


        public Categoria ObtenerCategoria(int categoriaId)
        {
            return _dal.ObtenerCategoria(categoriaId);
        }


        public List<Categoria> ObtenerCategorias(String valor_tenant)
        {
            return _dal.ObtenerCategorias(valor_tenant);
        }

    }
}

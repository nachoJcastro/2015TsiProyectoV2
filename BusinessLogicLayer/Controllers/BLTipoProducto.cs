using BusinessLogicLayer.Interfaces;
using Crosscutting.Entity;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Controllers
{
    public class BLTipoProducto : IBLTipoProducto
    {
        private IDALTipoProducto _dal = new DALTipoProductoEF();

        public BLTipoProducto(IDALTipoProducto dal) 
        {
            this._dal = dal;
        }

        public BLTipoProducto() { }

        public void AgregarTipoProducto(TipoProductoDTO tipoProductoDTO)
        {
            _dal.AgregarTipoProducto(tipoProductoDTO);
        }

        public TipoProductoDTO ObtenerTipoProducto(int tipoProductoId)
        {
            return _dal.ObtenerTipoProducto(tipoProductoId);
        }

        public List<TipoProductoDTO> ObtenerTipoProductos()
        {
            return _dal.ObtenerTipoProductos();
        }

        public List<TipoProductoDTO> ObtenerTipoPorCategoria(int idCat) 
        {
            return _dal.ObtenerTipoPorCategoria(idCat);
        }

        public void ActualizarTipoProducto(TipoProductoDTO tipoProductoDTO)
        {
            _dal.ActualizarTipoProducto(tipoProductoDTO);
        }

        public void EliminarTipoProducto(int tipoProductoId)
        {
            _dal.EliminarTipoProducto(tipoProductoId);
        }
    }
}

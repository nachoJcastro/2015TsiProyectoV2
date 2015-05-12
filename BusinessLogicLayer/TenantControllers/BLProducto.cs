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
    public class BLProducto : IBLProducto
    {

        private IDALProducto _dal = new DALProductoEF();


        public BLProducto(IDALProducto dal)
        {
            this._dal = dal;
        }

        public BLProducto() { }


        public void AgregarProducto(Producto producto)
        {
            _dal.AgregarProducto(producto);
        }


        public Producto ObtenerProducto(int productoId)
        {
            return _dal.ObtenerProducto(productoId);
        }


        public List<Producto> ObtenerProductos()
        {
            return _dal.ObtenerProductos();
        }


        public void ActualizarProducto(Producto producto)
        {
            _dal.ActualizarProducto(producto);
        }


        public void EliminarProducto(int productoId)
        {
            _dal.EliminarProducto(productoId);
        }
    }
}

using Crosscutting.EntityTenant;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public interface IDALProducto
    {
        void AgregarProducto(Producto producto); //Create
        Producto ObtenerProducto(int productoId); //Read
        List<Producto> ObtenerProductos();
        void ActualizarProducto(Producto producto); //Update
        void EliminarProducto(int productoId); //Delete
    }
}

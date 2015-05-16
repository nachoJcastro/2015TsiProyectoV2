using Crosscutting.EntityTenant;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crosscutting.Entity;


namespace DAL
{
    public interface IDALProducto
    {
        //void AgregarProducto(Producto producto); //Create
        TipoProductoDTO ObtenerProducto(int productoId); //Read
        List<TipoProductoDTO> ObtenerProductos();
        //void ActualizarProducto(Producto producto); //Update
        //void EliminarProducto(int productoId); //Delete
        List<TipoProductoDTO>   ObtenerTipoProdCategoria(int idCategoria);
        List<AtributosDTO> ObtenerAtributosTipoProd(int idCategoria);
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crosscutting.EntityTenant;
using Crosscutting.Entity;

namespace BusinessLogicLayer.TenantInterfaces
{
    public interface IBLProducto
    {
        //Operaciones CRUD
       // void AgregarProducto(Producto producto); //Insert
        TipoProductoDTO ObtenerProducto(int productoId); //FindOne
        List<TipoProductoDTO> ObtenerProductos();//FindAllAs
       // void ActualizarProducto(Producto producto); //Update
       //       void EliminarProducto(int productoId); //Delete

        List<CategoriasDTO> ObtenerCategoriasPorTienda(int idTienda);
        List<TipoProductoDTO> ObtenerTipoProdCategoria(int idCategoria);
        List<AtributosDTO> ObtenerAtributosTipoProd(int idCategoria);
    }
}

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
        List<TipoProductoDTO> ObtenerTipoProdCategoria(int idTienda, int idCategoria);
        List<AtributosDTO> ObtenerAtributosTipoProd(int idTienda, int idCategoria);

        TipoProductoDTO ObtenerProductoTenant(int idTienda,int categoriaId,int productoId); //FindOneString tenant

    }
}

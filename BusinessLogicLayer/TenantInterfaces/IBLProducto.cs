using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crosscutting.EntityTenant;

namespace BusinessLogicLayer.TenantInterfaces
{
    public interface IBLProducto
    {
        //Operaciones CRUD
        void AgregarProducto(Producto producto); //Insert
        Producto ObtenerProducto(int productoId); //FindOne
        List<Producto> ObtenerProductos();//FindAllAs
        void ActualizarProducto(Producto producto); //Update
        void EliminarProducto(int productoId); //Delete
    }
}

using Crosscutting.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IBLTipoProducto
    {
        //Operaciones CRUD
        void AgregarTipoProducto(TipoProductoDTO tipoProductoDTO); //Create
        TipoProductoDTO ObtenerTipoProducto(int tipoProductoId); //Read
        List<TipoProductoDTO> ObtenerTipoProductos();
        List<TipoProductoDTO> ObtenerTipoPorCategoria(int idCat);
        void ActualizarTipoProducto(TipoProductoDTO tipoProductoDTO); //Update
        void EliminarTipoProducto(int tipoProductoId); //Delete

    }
}

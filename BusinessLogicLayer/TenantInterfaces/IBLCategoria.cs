//using Crosscutting.Entity;
using Crosscutting.EntityTenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.TenantInterfaces
{
    public interface IBLCategoria
    {
        void AgregarCategoria(Categoria categoriaDTO); //Insert
        Categoria ObtenerCategoria(int categoriaId); //FindONe
        List<Categoria> ObtenerCategorias();//FindAll
        List<Categoria> ObtenerCategoriasPorTienda(int idTienda);
        void ActualizarCategoria(Categoria categoriaDTO); //Update
        void EliminarCategoria(int categoriaId); //Delete
    }
}

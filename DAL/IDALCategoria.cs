using Crosscutting.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDALCategoria
    {
        void AgregarCategoria(CategoriasDTO categoriaDTO); //Create
        CategoriasDTO ObtenerCategoria(int categoriaId); //Read
        List<CategoriasDTO> ObtenerCategorias();
        List<CategoriasDTO> ObtenerCategoriasPorTienda(int Tienda);
        void ActualizarCategoria(CategoriasDTO categoriaDTO); //Update
        void EliminarCategoria(int categoriaId); //Delete
        CategoriasDTO ObtenerCategoriaByNombre(int tenant, String nombre);
    }
}

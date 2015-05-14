using Crosscutting.EntityTenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDALCategoriaTenant
    {
        Categoria ObtenerCategoria(int categoriaId); //Read
        List<Categoria> ObtenerCategorias(String valor_tenant);
    }
}

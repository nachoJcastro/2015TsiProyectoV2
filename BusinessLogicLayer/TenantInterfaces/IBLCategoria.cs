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
        Categoria ObtenerCategoria(int categoriaId); //FindONe
        List<Categoria> ObtenerCategorias(String valor_tenant);//FindAll
    }
}

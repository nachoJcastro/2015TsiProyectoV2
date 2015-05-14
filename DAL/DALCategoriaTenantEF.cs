using Crosscutting.EntityTenant;
using DAL.Contextos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALCategoriaTenantEF : IDALCategoriaTenant
    {
        static TenantDB db = new TenantDB("string tenant");

        public DALCategoriaTenantEF() { }


        public Categoria ObtenerCategoria(int categoriaId)
        {
            try
            {
                var categoria = db.Categoria.FirstOrDefault(r => r.id == categoriaId);
                return categoria;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Categoria> ObtenerCategorias(String valor_tenant)
        {
            var listaCat = new List<Categoria>();
            try
            {
                db = new TenantDB(valor_tenant);
                listaCat = db.Categoria.ToList();
                return listaCat;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
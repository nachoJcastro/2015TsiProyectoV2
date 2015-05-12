using Crosscutting.Entity;
using DAL.Contextos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALCategoriaEF : IDALCategoria
    {
        static BackendDB db = new BackendDB();

        public DALCategoriaEF() { }

        public void AgregarCategoria(CategoriasDTO categoriaDTO)
        {
            try
            {
                db.Categorias.Add(categoriaDTO);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public CategoriasDTO ObtenerCategoria(int categoriaId)
        {
            try
            {
                var categoria = db.Categorias.FirstOrDefault(r => r.CategoriaId == categoriaId);

                return categoria;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CategoriasDTO> ObtenerCategorias()
        {
            var listaCat = new List<CategoriasDTO>();

            try
            {
                listaCat = db.Categorias.ToList();
                
                return listaCat;

            }
            catch (Exception e)
            {
                
                throw e;
            }
        }
        public List<CategoriasDTO> ObtenerCategoriasPorTienda(int idTienda) 
        {
            var listaCat = new List<CategoriasDTO>();
            try
            {
                listaCat = db.Categorias.Where(t => t.TiendaId == idTienda).ToList();
                return listaCat;
            }
            catch (Exception e)
            {
                
                throw e;
            }
        
        }

        public void ActualizarCategoria(CategoriasDTO categoriaDTO)
        {
            try
            {
                var categoria = db.Categorias.FirstOrDefault(r => r.CategoriaId == categoriaDTO.CategoriaId);

                if (categoria != null)
                {
                    categoria = categoriaDTO;
                    db.SaveChanges();
                }

            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        public void EliminarCategoria(int categoriaId)
        {

            try
            {
                var categoria = db.Categorias.FirstOrDefault(r => r.CategoriaId == categoriaId);

                if (categoria != null)
                {
                    while (categoria.Atributos.Any())
                    {
                        var atributo = categoria.Atributos.First();
                        db.Atributos.Remove(atributo);
                    }

                    while (categoria.TipoProductos.Any())
                    {
                        var tipo = categoria.TipoProductos.First();
                        db.TiposProductos.Remove(tipo);
                    }

                    db.Categorias.Remove(categoria);
                    db.SaveChanges();
                }  

            }
            catch (Exception)
            {    
                throw;
            }
        }
    }
}

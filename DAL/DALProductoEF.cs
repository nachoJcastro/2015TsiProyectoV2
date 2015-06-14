using Crosscutting.Entity;
using Crosscutting.EntityTenant;
using DAL.Contextos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALProductoEF : IDALProducto
    {
        BackendDB db = new BackendDB();

        public DALProductoEF() { }

       /* public void AgregarProducto(Producto producto)
        {
            try
            {
                db.Producto.Add(producto);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }*/

        public TipoProductoDTO ObtenerProductoTenant(int idTienda, int categoriaId, int productoId)
        {
            try
            {

                var producto = db.TiposProductos.FirstOrDefault(p => p.TipoProductoId == productoId && p.CategoriaId == categoriaId);
                return producto;

            }
            catch (Exception e)
            {
                throw e;
            }
        }   
        public TipoProductoDTO ObtenerProducto(int productoId)
        {
            try
            {
                var producto = db.TiposProductos.FirstOrDefault(p => p.TipoProductoId == productoId);

                return producto;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<TipoProductoDTO> ObtenerProductos()
        {
            var listaProd = new List<TipoProductoDTO>();

            try
            {
                listaProd = db.TiposProductos.ToList();

                return listaProd;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /*public void ActualizarProducto(TipoProductoDTO producto)
        {
            try
            {
                var productoViejo = db.Producto.FirstOrDefault(p => p.id == producto.id);

                if (productoViejo != null)
                {
                    productoViejo = producto;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }*/

        //***************************************
         public List<TipoProductoDTO>   ObtenerTipoProdCategoria(int idTienda, int idCategoria){
             var listaProd = new List<TipoProductoDTO>();
             var listaProd_temp = new List<TipoProductoDTO>();
             try
             {
                System.Diagnostics.Debug.WriteLine("Entro en DAL  ObtenerTipoProdCategoria: " + idTienda + "- cat " + idCategoria);
                listaProd_temp = db.TiposProductos.Where(t => t.CategoriaId == idCategoria).ToList();

                foreach (var item in listaProd_temp)
                {
                    listaProd.Add(item);
                }

                if (listaProd == null) System.Diagnostics.Debug.WriteLine("Retorno lista nula");
                else  System.Diagnostics.Debug.WriteLine("Lista con datos");
                return listaProd;
                 
             }
             catch (Exception e)
             {
                 throw e;
             }

            
         }

        //************************************************
         //***************************************
         public List<AtributosDTO> ObtenerAtributosTipoProd(int idTienda, int idCategoria)
         {
            try{

                List<AtributosDTO> listaAtrib = db.Atributos.Where(t => t.CategoriaId == idCategoria).ToList();
                return listaAtrib;
             }
             catch (Exception e)
             {
                 throw e;
             }
         }

         //************************************************




       /*public void EliminarProducto(int productoId)
        {
            try
            {
                var producto = db.Producto.FirstOrDefault(p => p.id == productoId);

                if (producto != null)
                {

                    while (producto.Atributo.Any())
                    {
                        var atributo = producto.Atributo.First();
                        db.Atributo.Remove(atributo);
                    }

                    while (producto.Favorito.Any())
                    {
                        var fav = producto.Favorito.First();
                        db.Favorito.Remove(fav);
                    }

                    while (producto.Subasta.Any())
                    {
                        var sub = producto.Subasta.First();
                        db.Subasta.Remove(sub);
                    }

                    db.Producto.Remove(producto);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }*/
    }
}

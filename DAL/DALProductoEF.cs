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
        static TenantDB db = new TenantDB("falta pasarle el string database");

        public DALProductoEF() { }

        public void AgregarProducto(Producto producto)
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
        }

        public Producto ObtenerProducto(int productoId)
        {
            try
            {
                var producto = db.Producto.FirstOrDefault(p => p.id == productoId);

                return producto;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Producto> ObtenerProductos()
        {
            var listaProd = new List<Producto>();

            try
            {
                listaProd = db.Producto.ToList();

                return listaProd;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ActualizarProducto(Producto producto)
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
        }

        public void EliminarProducto(int productoId)
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
        }
    }
}

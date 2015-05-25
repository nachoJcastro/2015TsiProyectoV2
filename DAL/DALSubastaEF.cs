using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crosscutting.EntityTenant;
using DAL.Contextos;

namespace DAL
{
    public class DALSubastaEF : IDALSubasta
    {
        static TenantDB db ;//= new TenantDB(" ")


        public DALSubastaEF() { }


        //************************
        public void AltaSubasta(string tenant, Subasta subasta) {
            try
            {
                db = new TenantDB(tenant);
                db.Subasta.Add(subasta);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        
        
        }
        //************************

        public void AgregarSubasta(String tenant, Subasta subasta)
        {
            try
            {
                db = new TenantDB(tenant);
                db.Subasta.Add(subasta);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public Subasta ObtenerSubasta(String tenant, int subastaId)
        {
            try
            {
                db = new TenantDB(tenant);
                var subasta = db.Subasta.FirstOrDefault(s => s.id == subastaId);
                return subasta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<Subasta> ObtenerSubastas(String tenant)
        {
            var listaSub = new List<Subasta>();
            try
            {
                db = new TenantDB(tenant);
                listaSub = db.Subasta.ToList();
                return listaSub;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void ActualizarSubasta(String tenant, Subasta subastaNueva)
        {
            try
            {
                db = new TenantDB(tenant);
                var subasta = db.Subasta.FirstOrDefault(p => p.id == subastaNueva.id);
                if (subasta != null)
                {
                    subasta.Atributo_Subasta = subastaNueva.Atributo_Subasta;
                    subasta.Calificacion = subastaNueva.Calificacion;
                    subasta.Comentario = subastaNueva.Comentario;
                    subasta.coordenadas = subastaNueva.coordenadas;
                    subasta.descripcion = subastaNueva.descripcion;
                    subasta.estado = subastaNueva.estado;
                    subasta.Favorito = subastaNueva.Favorito;
                    subasta.fecha_Cierre = subastaNueva.fecha_Cierre;
                    subasta.fecha_Inicio = subastaNueva.fecha_Inicio;
                    subasta.finalizado = subastaNueva.finalizado;
                    subasta.garantia = subastaNueva.garantia;
                    subasta.id = subastaNueva.id;
                    subasta.id_Categoria = subastaNueva.id_Categoria;
                    subasta.id_Comprador = subastaNueva.id_Comprador;
                    subasta.id_Producto = subastaNueva.id_Producto;
                    subasta.id_Vendedor = subastaNueva.id_Vendedor;
                    subasta.Imagen = subastaNueva.Imagen;
                    //subasta.Oferta = subastaNueva.Oferta;
                    subasta.precio_Base = subastaNueva.precio_Base;
                    subasta.precio_Compra = subastaNueva.precio_Compra;
                    subasta.tags = subastaNueva.tags;
                    subasta.titulo = subastaNueva.titulo;
                    subasta.valor_Actual = subastaNueva.valor_Actual;
                    //subasta.Vendedor = subastaNueva.Vendedor;

                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        

        public void EliminarSubasta(int subastaId)
        {
            try
            {
                var subasta = db.Subasta.FirstOrDefault(s => s.id == subastaId);
                if (subasta != null)
                {

                    while (subasta.Atributo_Subasta.Any())
                    {
                        var atributo = subasta.Atributo_Subasta.First();
                        db.Atributo_Subasta.Remove(atributo);
                    }
                    
                    while (subasta.Calificacion.Any())
                    {
                        var cal = subasta.Calificacion.First();
                        db.Calificacion.Remove(cal);
                    }

                    while (subasta.Imagen.Any())
                    {
                        var img = subasta.Imagen.First();
                        db.Imagen.Remove(img);
                    }

                    while (subasta.Oferta.Any())
                    {
                        var oferta = subasta.Oferta.First();
                        db.Oferta.Remove(oferta);
                    }

                    db.Subasta.Remove(subasta);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<Oferta> ObtenerOfertas(int subastaId)
        {
            var listaOfertas = new List<Oferta>();
            var subasta = db.Subasta.FirstOrDefault(s => s.id == subastaId);
            try
            {
                listaOfertas = subasta.Oferta.ToList();
                return listaOfertas;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public Boolean ActualizarMonto(string tenant, int id_subasta, double monto)
        {
             try
            {
                db = new TenantDB(tenant);
                var subasta = db.Subasta.FirstOrDefault(s => s.id == id_subasta);
                subasta.valor_Actual = monto;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

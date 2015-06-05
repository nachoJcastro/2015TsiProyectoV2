using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crosscutting.EntityTenant;
using DAL.Contextos;
using Crosscutting.EntityTareas;
using DAL.IDAL_Tenant;
using DAL.DAL_Tenant;

namespace DAL
{
    public class DALSubastaEF : IDALSubasta
    {
        static TenantDB db ;//= new TenantDB(" ")
        IDALUsuario _idal;
        

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

        //************************************************************************************
        public List<Correo> correoCompraDirecta(String tenant,Subasta sub) {
            List<Correo> lista=new List<Correo>();
            try
            {
                System.Diagnostics.Debug.WriteLine("Entro correoCompraDirecta DAL ");
                //Creo los correos a enviar
                Correo comprador = correoComprador(tenant, sub);

                Correo vendedor = correoVendedor(tenant, sub);
                
                //Agrego
                lista.Add(comprador);

                lista.Add(vendedor);

                System.Diagnostics.Debug.WriteLine("Salgo correoCompraDirecta  DAL ");

            }
            catch (Exception)
            {
                
                throw;
            }
            
            
            
            return lista;
        }

        public Correo correoVendedor(String tenant,Subasta sub)
        {
            Correo correo = new Correo();
            try
            {
                System.Diagnostics.Debug.WriteLine("Entro correoVendedor DAL");
                _idal = new DALUsuario();
               
                Usuario vendedor = _idal.GetUsuario(tenant, sub.id_Vendedor);
                Usuario comprador = _idal.GetUsuario(tenant, (int)sub.id_Comprador);
                //Subasta subasta = ObtenerSubasta(tenant, idSubasta);

                correo.destinatario = vendedor.email;
                correo.asunto = "Felicidades " + vendedor.nick + ". El usuario " + comprador.nick +"ha comprado tu articulo " + sub.titulo;
                correo.mensaje = "Articulo : " + sub.titulo + "Precio venta " + sub.precio_Compra.ToString() +" Fecha : " + DateTime.Now.ToString();

                System.Diagnostics.Debug.WriteLine("Salgo correoVendedor DAL ");
               

            }
            catch (Exception)
            {
                
                throw;
            }
           




            return correo;
        }

        public Correo correoComprador(String tenant , Subasta sub)
        {
            Correo correo = new Correo();
            

            try
            {
                System.Diagnostics.Debug.WriteLine("Entro correoComprador DAL");

                _idal= new DALUsuario();
                Usuario vendedor = _idal.GetUsuario(tenant, sub.id_Vendedor);


                Usuario comprador = _idal.GetUsuario(tenant,(int)sub.id_Comprador);
                
                correo.destinatario = comprador.email;
                correo.asunto = "Felicidades " + comprador.nick + ". Has comprado el articulo " + sub.titulo;
                correo.mensaje = "Articulo : " + sub.titulo + "Precio compra " + sub.precio_Compra.ToString() + " Fecha : " + DateTime.Now.ToString();

                System.Diagnostics.Debug.WriteLine("Salgo correoComprador DAL");
                

            }
            catch (Exception)
            {

                throw;
            }

            return correo;
        }

        public void AgregarImagen(string tenant, Imagen img) 
        {
            
            try
            {
                db = new TenantDB(tenant);
                db.Imagen.Add(img);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        
        }

        public List<Imagen> ObtenerImagenes(string tenant, int id) {

            List<Imagen> imagenes = new List<Imagen>();
            try
            {
                db = new TenantDB(tenant);
                imagenes = db.Imagen.Where(i => i.id_Subasta == id).ToList();
                return imagenes;
            }
            catch (Exception e)
            {
                throw e;
            }
        
        }
       
    }
}

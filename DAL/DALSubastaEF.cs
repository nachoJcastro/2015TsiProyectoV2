using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crosscutting.EntityTenant;
using DAL.Contextos;
using Crosscutting.EntityTareas;
using Crosscutting.Enum;
using DAL.IDAL_Tenant;
using DAL.DAL_Tenant;
using System.Data.Entity;
using Crosscutting.Entity;

namespace DAL
{
    public class DALSubastaEF : IDALSubasta
    {
        static TenantDB db ;
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
            System.Diagnostics.Debug.WriteLine("ID SUBASTA " + subasta.titulo);

            //int id_subasta = 0;
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

            //return id_subasta;
        }



        public int AgregarSubasta_ID(String tenant, Subasta subasta)
        {
            int id_subasta=-1;
            try
            {
                db = new TenantDB(tenant);
                db.Subasta.Add(subasta);
                db.SaveChanges();
                // read out your newly set IDENTITY value 
                 
                 
                 id_subasta = subasta.id;
                 System.Diagnostics.Debug.WriteLine("ID SUBASTA " + id_subasta.ToString());
            }
            catch (Exception e)
            {
                throw e;
            }

            return id_subasta;
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


        public List<Subasta> ObtenerSubastasActivas(string tenant) {

            List<Subasta> listaSub = null;
            try
            {
                db = new TenantDB(tenant);
                listaSub = db.Subasta.Where(x => x.estado == EstadoTransaccion.Activa).ToList();
                return listaSub;
            }
            catch (Exception e)
            {
                throw e;
            }
        
        
        }

        public List<Subasta> ObtenerSubastasActivasxCategoria(string tenant, int idCat)
        {

            var listaSub = new List<Subasta>();
            try
            {
                db = new TenantDB(tenant);
                var aux = db.Subasta.Where(x => x.estado == EstadoTransaccion.Activa).ToList();
                listaSub = aux.Where(x => x.id_Categoria == idCat).ToList();
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
                
                System.Diagnostics.Debug.WriteLine("Ingreso actualizo subasta");
                System.Diagnostics.Debug.WriteLine("Actualizo a estado "+ subastaNueva.estado.ToString());

                db = new TenantDB(tenant);
                Subasta subasta = db.Subasta.Find(subastaNueva.id);
                
                System.Diagnostics.Debug.WriteLine("Paso busco subasta");
                if (subasta != null)
                {

                    System.Diagnostics.Debug.WriteLine("Entro mod subasta");
                    /*subasta.Atributo_Subasta = subastaNueva.Atributo_Subasta;
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
                    //subasta.id = subastaNueva.id;
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
                    //subasta.Vendedor = subastaNueva.Vendedor;*/

                    //save modified entity using new Context

                    db.Entry(subasta).CurrentValues.SetValues(subastaNueva);
                    

                    db.SaveChanges();
                    System.Diagnostics.Debug.WriteLine("Sub "+ subasta.estado.ToString());
                    
                    // other changed properties
                   
                    System.Diagnostics.Debug.WriteLine("Salgo actualizar subasta");
            }
               
            else System.Diagnostics.Debug.WriteLine("No hay subasta para actualizar");
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
                subasta.precio_Compra = monto;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<Subasta> ObtenerSubastasByTipoProducto(String tenant, int idTipoProducto)
        {
            var listaSub = new List<Subasta>();
            try
            {
                db = new TenantDB(tenant);
                listaSub = db.Subasta.Where(s => s.id_Producto == idTipoProducto && s.estado == EstadoTransaccion.Activa).ToList();
                return listaSub;
            }
            catch (Exception e)
            {
                throw e;
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



        public TiendaVirtualDTO datos_tienda(string tenant) {

            TiendaVirtualDTO tienda = null;

            try
            {
                IDALTiendaVirtual _itienda = new DALTiendaVirtualEF();

                List<TiendaVirtualDTO> tiendas = _itienda.ObtenerTiendas();

                foreach (var item in tiendas)
                {
                    if (item.Dominio.Equals(tenant))
                    {
                        tienda = (TiendaVirtualDTO)item;
                    }
                }

            }
            catch (Exception)
            {
                
                throw;
            }
            return tienda;
        
        }




        //******************************************************
       
        public List<Correo> correoCompraSubasta(String tenant, Subasta sub)
        {
            List<Correo> lista = new List<Correo>();
            try
            {
                System.Diagnostics.Debug.WriteLine("Entro Subasta DAL ");
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
        //******************************************************

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
                correo.asunto = "Felicidades " + vendedor.nick + ". El usuario " + comprador.nick +" ha comprado tu articulo " + sub.titulo;
                correo.mensaje = "Articulo : " + sub.titulo + "Precio venta " + sub.precio_Base.ToString() + " Fecha : " + DateTime.Now.ToString() + System.Environment.NewLine + " Sitio " + tenant + "chebay.com";

                System.Diagnostics.Debug.WriteLine("Salgo correoVendedor DAL ");
               

            }
            catch (Exception)
            {
                
                throw;
            }
           




            return correo;
        }

        public Correo correoSinOfertas(string tenant, Subasta subasta) {

            Correo correo = new Correo();
            try
            {
               TiendaVirtualDTO tienda = datos_tienda(tenant);



                System.Diagnostics.Debug.WriteLine("Entro correoVendedor DAL");
                _idal = new DALUsuario();

                Usuario vendedor = _idal.GetUsuario(tenant, subasta.id_Vendedor);
               
                //Subasta subasta = ObtenerSubasta(tenant, idSubasta);

                correo.destinatario = vendedor.email;
                correo.asunto = "Lo sentimos " + vendedor.nick + ". El articulo finalizo sin ofertas " ;
                correo.mensaje = "Articulo : " + subasta.titulo + " .Precio base " + subasta.precio_Base + " .Fecha : " + DateTime.Now.ToString() + System.Environment.NewLine + " Sitio " + tenant+"chebay.com" ;

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
                correo.mensaje = "Articulo : " + sub.titulo + "Precio compra " + sub.valor_Actual.ToString() + " Fecha : " + DateTime.Now.ToString() + System.Environment.NewLine + " Sitio " + tenant + "chebay.com";

                System.Diagnostics.Debug.WriteLine("Salgo correoComprador DAL");
                

            }
            catch (Exception)
            {

                throw;
            }

            return correo;
        }



        internal Correo correoVendedorOferta(string tenant, Subasta sub , Oferta oferta)
        {

            Correo correo = new Correo();
            try
            {
                System.Diagnostics.Debug.WriteLine("Entro correoVendedor DAL");
                _idal = new DALUsuario();

                Usuario vendedor = _idal.GetUsuario(tenant, sub.id_Vendedor);
                Usuario comprador = _idal.GetUsuario(tenant, (int)oferta.id_Usuario);
                //Subasta subasta = ObtenerSubasta(tenant, idSubasta);

                correo.destinatario = vendedor.email;
                correo.asunto = "Novedades " + vendedor.nick + ". El usuario " + comprador.nick + " ha ofertado tu articulo " + sub.titulo;
                correo.mensaje = "Articulo : " + sub.titulo + "Precio oferta "+oferta.Monto+ " Fecha : " + DateTime.Now.ToString() + System.Environment.NewLine + " Sitio " + tenant + "chebay.com";

                System.Diagnostics.Debug.WriteLine("Salgo correoVendedor DAL ");


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

        public List<Imagen> ObtenerImagenes(string tenant, int id)
        {

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

        public List<Subasta> ObtenerSubastasPorCriterio(string tenant, int idCat, string criterio, int? tipo, String min, string max)
        {
            
            var listaSub = new List<Subasta>();
            try
            {
                db = new TenantDB(tenant);
                var aux = db.Subasta.Where(x => x.estado == EstadoTransaccion.Activa).ToList();
                listaSub = aux.Where(x => x.id_Categoria == idCat).ToList();

                if (!String.IsNullOrEmpty(criterio))
                {
                    listaSub = listaSub.Where(x => (x.titulo.Contains(criterio) || x.descripcion.Contains(criterio))).ToList();
                    
                }

                if (!String.IsNullOrEmpty(min) && !String.IsNullOrEmpty(max)) {
                    double minimo = double.Parse(min.ToString());
                    double maximo = double.Parse(max.ToString());

                    listaSub = listaSub.Where(x => (x.precio_Compra >= minimo && x.precio_Compra <= maximo)).ToList();
                    
                }
                else
                {

                    if (!String.IsNullOrEmpty(min))
                    {
                        double minimo = double.Parse(min.ToString());
                        listaSub = listaSub.Where(x => x.precio_Compra >= minimo).ToList();
                    }

                    if (!String.IsNullOrEmpty(max))
                    {
                        double maximo = double.Parse(max.ToString());
                        listaSub = listaSub.Where(x => x.precio_Compra <= maximo).ToList();
                    }

                }

                if (tipo!=null && tipo!= 0)
                {
                    TipoFinalizacion t;

                    switch (tipo)
                    {
                        case 1:
                            t = (TipoFinalizacion)Enum.Parse(typeof(TipoFinalizacion), "Subasta");
                            listaSub = listaSub.Where(x => x.finalizado == t).ToList();
                            break;
                        case 2:
                            t = (TipoFinalizacion)Enum.Parse(typeof(TipoFinalizacion), "Compra_directa");
                            listaSub = listaSub.Where(x => x.finalizado == t).ToList();
                            break;
                    }

                    
                }

                /*switch (campoOrdenamiento)
                {
                    case "nombre_desc":
                        juegos = juegos.OrderByDescending(o => o.Nombre);
                        break; ;
                    case "consola":
                        juegos = juegos.OrderBy(o => o.Consola.Nombre);
                        break;
                    case "consola_desc":
                        juegos = juegos.OrderByDescending(o => o.Consola.Nombre);
                        break;
                    case "genero":
                        juegos = juegos.OrderBy(o => o.Genero.Nombre);
                        break;
                    case "genero_desc":
                        juegos = juegos.OrderByDescending(o => o.Genero.Nombre);
                        break;
                    default:  // Nombre ascending 
                        juegos = juegos.OrderBy(s => s.Nombre);
                        break;
                }*/


                return listaSub;
                
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }


        public List<Subasta> ObtenerVentasbyUsuario(string tenant, int idUsuario)
        {
            var listaSub = new List<Subasta>();
            try
            {
                db = new TenantDB(tenant);
                listaSub = db.Subasta.Where(s => s.id_Vendedor == idUsuario && s.estado == EstadoTransaccion.Cerrada).ToList();
                return listaSub;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<Subasta> ObtenerComprasbyUsuario(string tenant, int idUsuario)
        {
            var listaSub = new List<Subasta>();
            try
            {
                db = new TenantDB(tenant);
                listaSub = db.Subasta.Where(s => s.id_Comprador == idUsuario && s.estado == EstadoTransaccion.Cerrada).ToList();
                return listaSub;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}

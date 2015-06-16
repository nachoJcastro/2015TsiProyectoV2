using AutoMapper;
using Crosscutting.Entity;
using DAL.Contextos;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crosscutting.EntityTenant;
using System.Data.Entity.Core.Objects;

namespace DAL
{
    public class DALTiendaVirtualEF : IDALTiendaVirtual
    {
        static TenantDB dbt;
        static BackendDB db = new BackendDB();

        public DALTiendaVirtualEF()
        {

        }

        public void AgregarTienda(TiendaVirtualDTO tiendaDTO)
        {
            try
            {   //var tienda = Mapper.Map<TiendaVirtual>(tiendaDTO);
                //tiendaDTO.ListaImagenes = Mapper.Map<ICollection<Imagenes>>(tiendaDTO.ListaImagenes);

                db.TiendaVirtual.Add(tiendaDTO);
                db.SaveChanges(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public TiendaVirtualDTO ObtenerTienda(int tiendaId) 
        { 
            try
            {
                var tienda = db.TiendaVirtual.FirstOrDefault(r => r.TiendaVitualId == tiendaId);

                //var tiendaDTO = new TiendaVirtualDTO();

                //tiendaDTO.ListaImagenes = tienda.ListaImagenes;

                return tienda;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TiendaVirtualDTO> ObtenerTiendas()
        {
            var listaTienda = new List<TiendaVirtualDTO>();
            
            try
            {

                //foreach (var t in db.TiendaVirtual)
                //{
                //    var tiendaDTO = Mapper.Map<TiendaVirtualDTO>(t);

                //    listaTienda.Add(tiendaDTO);
                //}
                //var 
                listaTienda = db.TiendaVirtual.ToList();

                //var tiendasDTO = Mapper.Map<List<TiendaVirtualDTO>>(tiendas);

                return listaTienda;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TiendaVirtualDTO> ObtenerTiendaDelUsuario(string idUsuario) 
        {
            var listaTienda = new List<TiendaVirtualDTO>();
            try
            {
                listaTienda = db.TiendaVirtual.Where(j => j.UsuarioId == idUsuario).ToList();
                return listaTienda;
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        public void ActualizarTiendas(TiendaVirtualDTO tiendaDTO) 
        {
            try
            {
                var tienda = db.TiendaVirtual.FirstOrDefault(r => r.TiendaVitualId == tiendaDTO.TiendaVitualId);

                if (tienda != null)
                {
                    tienda.Nombre = tiendaDTO.Nombre;
                    tienda.Dominio = tiendaDTO.Dominio;
                    //tienda.StringConection = tiendaDTO.StringConection;
                    tienda.Logo = tiendaDTO.Logo;
                    //tienda.Estado = tienda.Estado;
                    //tienda.Css = tiendaDTO.Css;
                    tienda.Descripcion = tiendaDTO.Descripcion;
                    //tienda.ListaImagenes = tiendaDTO.ListaImagenes;
                    //Mapper.Map(tiendaDTO, tienda);
                    //Mapper.Map(tiendaDTO.ListaImagenes, tienda.Imagenes);

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarCSS(TiendaVirtualDTO tiendaDTO) 
        {
            try
            {
                var tienda = db.TiendaVirtual.FirstOrDefault(r => r.TiendaVitualId == tiendaDTO.TiendaVitualId);

                if (tienda != null)
                {

                    tienda.Css = tiendaDTO.Css;
                    tienda.ListaImagenes = tiendaDTO.ListaImagenes;
                    //Mapper.Map(tiendaDTO, tienda);
                    //Mapper.Map(tiendaDTO.ListaImagenes, tienda.Imagenes);

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EditarCss(int id,string css)
        { 
            try
            {
                var tienda = db.TiendaVirtual.FirstOrDefault(r => r.TiendaVitualId == id);

                if (tienda != null)
                {
                    tienda.Css = css;

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        public void EliminarTienda(int tiendaId) 
        {
            try
            {
                var tienda = db.TiendaVirtual.FirstOrDefault(r => r.TiendaVitualId == tiendaId);

                if (tienda != null)
                {
                    while (tienda.ListaImagenes.Any())
                    {
                        var imagen = tienda.ListaImagenes.First();

                        db.Imagenes.Remove(imagen);
                    }

                    while (tienda.ListaImagenes.Any())
                    {
                        var imagen = tienda.ListaImagenes.First();

                        db.Imagenes.Remove(imagen);
                    }

                    db.TiendaVirtual.Remove(tienda);
                    db.SaveChanges();
                }      
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarTiendaVirtual(int tiendaId)
        {
            try
            {
                var tienda = db.TiendaVirtual.FirstOrDefault(r => r.TiendaVitualId == tiendaId);

                if (tienda != null)
                {
                    if (tienda.Estado)
                    {
                        tienda.Estado = false;
                    }
                    else 
                    { 
                        tienda.Estado = true; 
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<String> ObtenerTenants() {
           System.Diagnostics.Debug.WriteLine("obtengo tenants");

            var tiendas = new List<TiendaVirtualDTO>();
            List<String> tenants = new List<String>();
            try
            {
                 tiendas =db.TiendaVirtual.ToList();

                foreach (var item in tiendas)
                {
                    System.Diagnostics.Debug.WriteLine(item.Dominio);
                    tenants.Add(item.Dominio);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return tenants;
            
        
        }

        public void EliminarImagenTienda(int tiendaId, string nombre) 
        {

            try
            {
                var tienda = db.TiendaVirtual.FirstOrDefault(r => r.TiendaVitualId == tiendaId);

                if (tienda.ListaImagenes != null)
                {
                    foreach (var imagen in tienda.ListaImagenes.ToList())
                    {
                        if (imagen.Nombre.Equals(nombre)) 
                        {
                            imagen.ImagenEliminada = true;
                           
                        }
                        
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        
        }

        public void AgregarImagenTienda(ImagenesDTO img) 
        {
            try
            {   //var tienda = Mapper.Map<TiendaVirtual>(tiendaDTO);
                //tiendaDTO.ListaImagenes = Mapper.Map<ICollection<Imagenes>>(tiendaDTO.ListaImagenes);

                db.Imagenes.Add(img);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }

        public List<Usuario> ReportUsers(string dominio, DateTime fechaini, DateTime fechafin) {
            List<Usuario> usuarios = new List<Usuario>();

            if ((dominio == null) || (fechaini == null) || (fechafin == null))
            {
                System.Diagnostics.Debug.WriteLine("Dominio nulo");
                

            }
            else {
                System.Diagnostics.Debug.WriteLine("Chart dom - "+ dominio);
                System.Diagnostics.Debug.WriteLine("Chart fech inic - "+ fechaini);
                System.Diagnostics.Debug.WriteLine("Chart fecha fin"+ fechafin);

                try
                {
                    dbt = new TenantDB(dominio);
                    usuarios = dbt.Usuario.Where(u => (u.fecha_Alta >= fechaini && u.fecha_Alta <= fechafin)).ToList();

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            
            }
            System.Diagnostics.Debug.WriteLine("Usuarios" + usuarios.ToString());
            return usuarios;
        }

        public List<SubastaAux> ReportSubasta(string dominio, DateTime fechaini, DateTime fechafin)
        {
            List<Subasta> sub = new List<Subasta>();
            var listaSub = new List<SubastaAux>();

            try
            {
                dbt = new TenantDB(dominio);
                sub = dbt.Subasta.Where(u => (u.fecha_Inicio >= fechaini && u.fecha_Inicio <= fechafin)).ToList();
                sub = sub.Where(x => x.id_Comprador != null).ToList();

                foreach (var subasta in sub)
                {
                    SubastaAux aux = new SubastaAux();
                    Usuario Comprador = dbt.Usuario.FirstOrDefault(x => x.id == subasta.id_Comprador);
                    Usuario Vendedor = dbt.Usuario.FirstOrDefault(x => x.id == subasta.id_Vendedor);

                    aux.id = subasta.id;
                    aux.id_Comprador = subasta.id_Comprador;
                    aux.nombreComprador = Comprador.nick;
                    aux.id_Vendedor = subasta.id_Vendedor;
                    aux.nombreVendedor = Vendedor.nick;
                    aux.id_Categoria = subasta.id_Categoria;
                    aux.id_Producto = subasta.id_Producto;
                    aux.titulo = subasta.titulo;
                    aux.descripcion = subasta.descripcion;
                    aux.precio_Base = subasta.precio_Base;
                    aux.precio_Compra = subasta.precio_Compra;
                    aux.valor_Actual = subasta.valor_Actual;
                    aux.portada = subasta.portada;
                    aux.fecha_Inicio = (DateTime)subasta.fecha_Inicio;
                    aux.fecha_Cierre = (DateTime)subasta.fecha_Cierre;

                    listaSub.Add(aux);

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return listaSub;
        }

        public List<ReporteLineal> ReportUsersLineal(string dominio, DateTime fechaini, DateTime fechafin)
        {
            List<ReporteLineal> reporte = new List<ReporteLineal>();
            try
            {
                dbt = new TenantDB(dominio);
                //sub = dbt.Subasta.Where(u => (u.fecha_Inicio >= fechaini && u.fecha_Inicio <= fechafin)).ToList();

                //var query = from c in dbt.Subasta group c by c.fecha_Cierre into g select new { Fecha = g.Key.fecha_Cierre, cantidad = g.Count() };
                List<UsuarioAux> aux = new List<UsuarioAux>();
                List<Usuario> todos = dbt.Usuario.Where(x => x.fecha_Alta != null).ToList();

                foreach (var item in todos)
                {
                    UsuarioAux aux2 = new UsuarioAux
                    {
                        id = item.id,
                        nick = item.nick,
                        password = item.password,
                        nombre = item.nombre,
                        apellido = item.apellido,
                        fecha_Nacimiento = (DateTime)item.fecha_Nacimiento,
                        email = item.email,
                        direccion = item.direccion,
                        imagen = item.imagen,
                        fecha_Alta = (DateTime)item.fecha_Alta,
                        reputacion_Venta = item.reputacion_Venta,
                        reputacion_Compra= item.reputacion_Compra 
                    };

                    aux.Add(aux2);
                }


                reporte = aux.Where(u => (u.fecha_Alta >= fechaini && u.fecha_Alta <= fechafin)).GroupBy(x => new { x.fecha_Alta.Date }).Select(a => new ReporteLineal { Fecha = (DateTime)a.Key.Date, cantidad = a.Count() }).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return reporte;
        }

        public List<ReporteLineal> ReportSubastaLineal(string dominio, DateTime fechaini, DateTime fechafin) {
            List<ReporteLineal> reporte = new List<ReporteLineal>();

            try
            {
                dbt = new TenantDB(dominio);
                //sub = dbt.Subasta.Where(u => (u.fecha_Inicio >= fechaini && u.fecha_Inicio <= fechafin)).ToList();

                //var query = from c in dbt.Subasta group c by c.fecha_Cierre into g select new { Fecha = g.Key.fecha_Cierre, cantidad = g.Count() };
                List<SubastaAux> aux = new List<SubastaAux>();
                List<Subasta> todas = dbt.Subasta.Where(x => x.fecha_Cierre != null).ToList();
                
                foreach (var item in todas)
                {
                    SubastaAux aux2 = new SubastaAux
                    {
                        id = item.id,
                        id_Comprador = item.id_Comprador,
                        id_Vendedor = item.id_Vendedor,
                        id_Categoria = item.id_Categoria,
                        id_Producto = item.id_Producto,
                        titulo = item.titulo,
                        descripcion = item.descripcion,
                        precio_Base = item.precio_Base,
                        precio_Compra = item.precio_Compra,
                        valor_Actual = item.valor_Actual,
                        portada = item.portada,
                        fecha_Inicio = (DateTime)item.fecha_Inicio,
                        fecha_Cierre = (DateTime)item.fecha_Cierre
                    };

                    aux.Add(aux2);
                }


                reporte = aux.Where(u => (u.fecha_Cierre >= fechaini && u.fecha_Cierre <= fechafin)).GroupBy(x => new { x.fecha_Cierre.Date } ).Select(a => new ReporteLineal { Fecha = (DateTime)a.Key.Date, cantidad = a.Count() }).ToList();
                    
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return reporte;
        }

        


    }
}

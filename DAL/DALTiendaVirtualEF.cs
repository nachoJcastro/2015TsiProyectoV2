using AutoMapper;
using Crosscutting.Entity;
using DAL.Contextos;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALTiendaVirtualEF : IDALTiendaVirtual
    {
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
                    tienda.StringConection = tiendaDTO.StringConection;
                    tienda.Logo = tiendaDTO.Logo;
                    tienda.Estado = tienda.Estado;
                    tienda.Css = tiendaDTO.Css;
                    tienda.Dominio = tiendaDTO.Descripcion;
                    tienda.Descripcion = tiendaDTO.Descripcion;
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

    }
}

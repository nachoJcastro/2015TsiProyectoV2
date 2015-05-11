using Crosscutting.Entity;
using DAL.Contextos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALTipoProductoEF : IDALTipoProducto
    {
        static BackendDB db = new BackendDB();

        public DALTipoProductoEF()
        {

        }

        public void AgregarTipoProducto(TipoProductoDTO tipoProductoDTO)
        {
            try
            {
                db.TiposProductos.Add(tipoProductoDTO);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        public TipoProductoDTO ObtenerTipoProducto(int tipoProductoId)
        {
            try
            {
                var tipo = db.TiposProductos.FirstOrDefault(r => r.TipoProductoId == tipoProductoId);

                return tipo;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public List<TipoProductoDTO> ObtenerTipoProductos()
        {
            var listaTipos = new List<TipoProductoDTO>();

            try
            {
                listaTipos = db.TiposProductos.ToList();
                return listaTipos;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public List<TipoProductoDTO> ObtenerTipoPorCategoria(int idCat) 
        {
            var listaTipos = new List<TipoProductoDTO>();

            try
            {
                listaTipos = db.TiposProductos.Where(t=>t.CategoriaId == idCat).ToList();
                return listaTipos;
            }
            catch (Exception e)
            {

                throw e;
            }
        
        }

        public void ActualizarTipoProducto(TipoProductoDTO tipoProductoDTO)
        {
            try
            {
                var tipo = db.TiposProductos.FirstOrDefault(r => r.TipoProductoId == tipoProductoDTO.TipoProductoId);

                if (tipo != null)
                {
                    tipo = tipoProductoDTO;
                    db.SaveChanges();
                }

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void EliminarTipoProducto(int tipoProductoId)
        {
            try
            {
                var tipo = db.TiposProductos.FirstOrDefault(r => r.TipoProductoId == tipoProductoId);
                if (tipo!=null)
	            {
                    db.TiposProductos.Remove(tipo);
                    db.SaveChanges();
	            }
                
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}

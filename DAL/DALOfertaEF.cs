using Crosscutting.EntityTenant;
using DAL.Contextos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALOfertaEF : IDALOferta
    {
        static TenantDB db = new TenantDB("falta pasarle el string database");

        public DALOfertaEF() { }


        public void AgregarOferta(Oferta oferta)
        {
            try
            {
                db.Oferta.Add(oferta);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public Oferta ObtenerOferta(int ofertaId)
        {
            try
            {
                var oferta = db.Oferta.FirstOrDefault(o => o.id == ofertaId);
                return oferta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<Oferta> ObtenerOfertas()
        {
            var listaOfertas = new List<Oferta>();
            try
            {
                listaOfertas = db.Oferta.ToList();
                return listaOfertas;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        

        public void ActualizarOferta(Oferta oferta)
        {
            try
            {
                var ofertaVieja = db.Oferta.FirstOrDefault(o => o.id == oferta.id);
                if (ofertaVieja != null)
                {
                    ofertaVieja = oferta;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void EliminarOferta(int ofertaId)
        {
            try
            {
                var oferta = db.Oferta.FirstOrDefault(o => o.id == ofertaId);
                if (oferta != null)
                {

                    db.Oferta.Remove(oferta);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<Oferta> ObtenerOfertasByProducto(int id_subasta)
        {
            var listaOfer = new List<Oferta>();
            try
            {
                listaOfer = db.Oferta.Where(o => o.id_Subasta == id_subasta).ToList();
                return listaOfer;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

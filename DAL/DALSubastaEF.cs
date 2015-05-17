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
        static TenantDB db = new TenantDB("database");


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


        public Subasta ObtenerSubasta(int subastaId)
        {
            try
            {
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


        public void ActualizarSubasta(Subasta subasta)
        {
            try
            {
                var subastaVieja = db.Subasta.FirstOrDefault(p => p.id == subasta.id);
                if (subastaVieja != null)
                {
                    subastaVieja = subasta;
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
    }
}

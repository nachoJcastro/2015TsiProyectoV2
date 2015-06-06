using Crosscutting.EntityTenant;
using DAL.Contextos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALAtributo_SubastaEF : IDALAtributo_Subasta
    {
        
        public DALAtributo_SubastaEF() { }


        public void AgregarAtributo_Subasta(String tenant, Atributo_Subasta atributo)
        {
            try
            {
                TenantDB db = new TenantDB(tenant);
                db.Atributo_Subasta.Add(atributo);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public Atributo_Subasta ObtenerAtributo_Subasta(String tenant, int IdSubasta, int IdAtributo)
        {
            try
            {
                TenantDB db = new TenantDB(tenant);
                var atributo_subasta = db.Atributo_Subasta.FirstOrDefault(a => a.id_Atributo == IdAtributo && a.id_Subasta == IdSubasta);

                return atributo_subasta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Atributo_Subasta> ObtenerAtributos_Subasta(String tenant)
        {
             TenantDB db = new TenantDB(tenant);
             var listaAtr = new List<Atributo_Subasta>();

             try
             {
                 listaAtr = db.Atributo_Subasta.ToList();

                 return listaAtr;

             }
             catch (Exception e)
             {
                 throw e;
             }
        }


        public void ActualizarAtributo_Subasta(String tenant, Atributo_Subasta atributo)
        {
            try
            {
                TenantDB db = new TenantDB(tenant);
                var atributo_subasta = db.Atributo_Subasta.FirstOrDefault(a => a.id_Atributo == atributo.id_Atributo && a.id_Subasta == atributo.id_Subasta);
                if (atributo_subasta != null)
                {
                    atributo_subasta.valor = atributo.valor;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void EliminarAtributo_Subasta(String tenant, int idAtributo, int idSubasta)
        {
            try
            {
                TenantDB db = new TenantDB(tenant);
                var atributo_subasta = db.Atributo_Subasta.FirstOrDefault(a => a.id_Subasta == idSubasta && a.id_Atributo == idAtributo);
                if (atributo_subasta != null)
                {
                    db.Atributo_Subasta.Remove(atributo_subasta);
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

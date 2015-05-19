using Crosscutting.Entity;
using DAL.Contextos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALAtributoEF : IDALAtributos
    {
        static BackendDB db = new BackendDB();

        public DALAtributoEF() { }

        public void AgregarAtributo(AtributosDTO atributoDTO)
        {
            try
            {
                db.Atributos.Add(atributoDTO);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        public AtributosDTO ObtenerAtributo(int atributoId)
        {
            try
            {
                var atributo = db.Atributos.FirstOrDefault(r => r.AtributoId == atributoId);
                return atributo;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public List<AtributosDTO> ObtenerAtributos()
        {
            var listAtri = new List<AtributosDTO>();
            try
            {
                listAtri = db.Atributos.ToList();

                return listAtri;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public List<AtributosDTO> ObtenerAtributosPorCategoria(int idCat) 
        {
            var listAtri = new List<AtributosDTO>();
            try
            {
                listAtri = db.Atributos.Where(t => t.CategoriaId == idCat).ToList();

                return listAtri;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void ActualizarAtributo(AtributosDTO atributoDTO)
        {
            try
            {
                var atributo = db.Atributos.FirstOrDefault(r => r.AtributoId == atributoDTO.AtributoId);
                if (atributo!=null)
                {
                    atributo.CategoriaId = atributoDTO.CategoriaId;
                    atributo.Nombre = atributoDTO.Nombre;
                    db.SaveChanges();
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void EliminarAtributo(int atributoId)
        {
            try
            {
                var atributo = db.Atributos.FirstOrDefault(r => r.AtributoId == atributoId);
                if (atributo != null)
                {
                    db.Atributos.Remove(atributo);
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

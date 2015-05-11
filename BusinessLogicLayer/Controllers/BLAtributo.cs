using BusinessLogicLayer.Interfaces;
using Crosscutting.Entity;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Controllers
{
    public class BLAtributo: IBLAtributo
    {
        IDALAtributos _dal = new DALAtributoEF();

        public BLAtributo(IDALAtributos dal)
        {
            this._dal = dal;
        }

        public BLAtributo() { }

        public void AgregarAtributo(AtributosDTO atributoDTO)
        {
            _dal.AgregarAtributo(atributoDTO);
        }

        public AtributosDTO ObtenerAtributo(int atributoId)
        {
            return _dal.ObtenerAtributo(atributoId);
        }

        public List<AtributosDTO> ObtenerAtributos()
        {
            return _dal.ObtenerAtributos();
        }

        public List<AtributosDTO> ObtenerAtributosPorCategoria(int idCat) 
        {
            return _dal.ObtenerAtributosPorCategoria(idCat);
        }

        public void ActualizarAtributo(AtributosDTO atributoDTO)
        {
            _dal.ActualizarAtributo(atributoDTO);
        }

        public void EliminarAtributo(int atributoId)
        {
            _dal.EliminarAtributo(atributoId);
        }
    }
}

using BusinessLogicLayer.Interfaces;
using Crosscutting.Entity;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.TenantControllers
{
    public class BLCategoria : IBLCategoria
    {
        private IDALCategoria _dal = new DALCategoriaEF();

        public BLCategoria(IDALCategoria dal)
        {
            this._dal = dal;
        }

        public BLCategoria() { }


        public void AgregarCategoria(CategoriasDTO categoriaDTO)
        {
            _dal.AgregarCategoria(categoriaDTO);
        }


        public CategoriasDTO ObtenerCategoria(int categoriaId)
        {
            return _dal.ObtenerCategoria(categoriaId);
        }


        public List<CategoriasDTO> ObtenerCategorias()
        {
            return _dal.ObtenerCategorias();
        }

        public List<CategoriasDTO> ObtenerCategoriasPorTienda(int idTienda)
        {
            return _dal.ObtenerCategoriasPorTienda(idTienda);
        }

        public void ActualizarCategoria(CategoriasDTO categoriaDTO)
        {
            _dal.ActualizarCategoria(categoriaDTO);
        }


        public void EliminarCategoria(int categoriaId)
        {
            _dal.EliminarCategoria(categoriaId);
        }
    }
}

﻿using BusinessLogicLayer.TenantInterfaces;
using Crosscutting.Entity;
using Crosscutting.EntityTenant;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.TenantControllers
{
    public class BLProducto : IBLProducto
    {
        private IDALProducto _dal = new DALProductoEF();
        private IDALCategoria _cat = new DALCategoriaEF();


        public BLProducto(IDALProducto dal)
        {
            this._dal = dal;
        }

        public BLProducto() { }


       /* public void AgregarProducto(Producto producto)
        {
            _dal.AgregarProducto(producto);
        }*/

        public TipoProductoDTO ObtenerProductoTenant(int idTienda,int categoriaId,int productoId)
        {
            return _dal.ObtenerProductoTenant( idTienda, categoriaId, productoId);
        }



        public TipoProductoDTO ObtenerProducto(int productoId)
        {
            return _dal.ObtenerProducto(productoId);
        }


        public List<TipoProductoDTO> ObtenerProductos()
        {
            return _dal.ObtenerProductos();
        }


        public List<TipoProductoDTO> ObtenerTipoProdCategoria(int idTienda,int idCategoria)
        {
            return _dal.ObtenerTipoProdCategoria(idTienda,idCategoria);
        }


        public List<CategoriasDTO> ObtenerCategoriasPorTienda(int idTienda)
        {
            return _cat.ObtenerCategoriasPorTienda(idTienda);
        }

        public List<AtributosDTO> ObtenerAtributosTipoProd(int idTienda,int idCategoria)
        {
            return _dal.ObtenerAtributosTipoProd(idTienda,idCategoria);
        }

        /*public void ActualizarProducto(Producto producto)
        {
            _dal.ActualizarProducto(producto);
        }


        public void EliminarProducto(int productoId)
        {
            _dal.EliminarProducto(productoId);
        }*/

       // List<AtributosDTO> ObtenerAtributosTipoProd(int idCategoria);

      
    }
}

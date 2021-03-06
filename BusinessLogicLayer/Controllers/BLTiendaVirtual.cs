﻿
using BusinessLogicLayer.TenantControllers;
using BusinessLogicLayer.TenantInterfaces;
using Crosscutting.Entity;
using Crosscutting.EntityTenant;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Controllers
{
    public class BLTiendaVirtual : IBLTiendaVirtual
    {
        private IDALTiendaVirtual _dal = new DALTiendaVirtualEF();
        private IBLTenant _tenant = new BLTenant();

        public BLTiendaVirtual(IDALTiendaVirtual dal)
        {
            this._dal = dal;
        }
        
        public BLTiendaVirtual() { }

        public void AgregarTienda(TiendaVirtualDTO tiendaDTO)
        {
           

            try
            {
                _dal.AgregarTienda(tiendaDTO);
                _tenant.AgregarTenant(tiendaDTO.Dominio.ToString());
                _tenant.AgregarHost(tiendaDTO.Dominio.ToString());
                //Console.WriteLine("Paso crear Tenant");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al crear Tenant", e.ToString());
            }


        }

        public TiendaVirtualDTO ObtenerTienda(int tiendaId)
        {
            return _dal.ObtenerTienda(tiendaId);
        }

        public List<TiendaVirtualDTO> ObtenerTiendas()
        {
            return _dal.ObtenerTiendas();
        }

        public List<TiendaVirtualDTO> ObtenerTiendaDelUsuario(string idUsuario)
        {
            return _dal.ObtenerTiendaDelUsuario(idUsuario);
        }

        public void ActualizarTiendas(TiendaVirtualDTO tiendaDTO) 
        {
            _dal.ActualizarTiendas(tiendaDTO);
        }
        public void ActualizarCSS(TiendaVirtualDTO tiendaDTO) 
        {
            _dal.ActualizarCSS(tiendaDTO);
        }

        public void EditarCss(int id,string css) 
        {
            _dal.EditarCss(id,css);
        }

        public void EliminarTienda(int tiendaId) 
        {
            _dal.EliminarTienda(tiendaId);
        }

        public void EliminarTiendaVirtual(int tiendaId)
        {
            _dal.EliminarTiendaVirtual(tiendaId);
        }

        public List<String> ObtenerTenants() {

            return _dal.ObtenerTenants();
        }

        public bool EliminarImagenTienda(int tiendaId, string nombre) {

            return _dal.EliminarImagenTienda(tiendaId, nombre);
        }

        public void AgregarImagenTienda(ImagenesDTO img){
        
            _dal.AgregarImagenTienda(img);
        }
        public List<ImagenesDTO> ObtenerImagenes(int tiendaId) {
            return _dal.ObtenerImagenes(tiendaId);
        }
        public List<Usuario> ReportUsers(string dominio, DateTime fechaini, DateTime fechafin) {
            return _dal.ReportUsers(dominio, fechaini, fechafin);
        }
        public List<SubastaAux> ReportSubasta(string dominio, DateTime fechaini, DateTime fechafin) {
            return _dal.ReportSubasta(dominio, fechaini, fechafin);
        }

        public List<ReporteLineal> ReportSubastaLineal(string dominio, DateTime fechaini, DateTime fechafin) {
            return _dal.ReportSubastaLineal(dominio, fechaini, fechafin);
        }
        public List<ReporteLineal> ReportUsersLineal(string dominio, DateTime fechaini, DateTime fechafin) {
            return _dal.ReportUsersLineal(dominio, fechaini, fechafin);
        }

        public List<UsuarioAux> DetUsers(string dominio, DateTime fechaini)
        {
            return _dal.DetUsers(dominio, fechaini);
        }
        public List<SubastaAux> DetSub(string dominio, DateTime fechaini) {
            return _dal.DetSub(dominio, fechaini);
        }
        public bool ExisteDominio(string dominio) {
            return _dal.ExisteDominio(dominio);        
        }
    }
}

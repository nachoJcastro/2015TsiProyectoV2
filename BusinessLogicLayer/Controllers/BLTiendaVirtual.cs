
using BusinessLogicLayer.TenantControllers;
using BusinessLogicLayer.TenantInterfaces;
using Crosscutting.Entity;
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
            _dal.AgregarTienda(tiendaDTO);

            try
            {
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
    }
}

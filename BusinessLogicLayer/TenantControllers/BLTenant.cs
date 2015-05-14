using Crosscutting.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

using BusinessLogicLayer.TenantInterfaces;
using DNSManager;

namespace BusinessLogicLayer.TenantControllers
{
    public class BLTenant : IBLTenant
    {
        private IDALTenant _dal = new DALTenant();
        private IHosts _hosts = new Hosts();
        
        public void AgregarTenant(String dominio)
        {
            //Console.WriteLine("Dentro de BLTenant");
            _dal.AgregarTenant(dominio);
        }

        public void AgregarHost(String dominio)
        {
            _hosts.AgregarHost(dominio);
        }

        public Boolean ExisteSitio(String dominio) {

            return _dal.ExisteSitio(dominio);
        
        }

        public int ObtenerIdTenant(String tenant)
        {
            return _dal.ObtenerIdTenant(tenant);
        }
        
    }
}

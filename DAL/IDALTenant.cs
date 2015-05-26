using Crosscutting.Entity;
using Crosscutting.EntityTenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDALTenant
    {
        //Operaciones 
        void AgregarTenant (String dominio); //Create
        Boolean ExisteSitio (String dominio); //Create
        int ObtenerIdTenant(String tenant);
        TiendaTenant ObtenerDatosTenant(int idTienda);
        List<ImagenesTenant> ObtenerImgTenant(int idTienda);
    }
}

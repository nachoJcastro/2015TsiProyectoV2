using Crosscutting.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDALTiendaVirtual
    {
        void AgregarTienda(TiendaVirtualDTO tiendaDTO); //Create
        TiendaVirtualDTO ObtenerTienda(int tiendaId); //Read
        List<TiendaVirtualDTO> ObtenerTiendas();
        List<TiendaVirtualDTO> ObtenerTiendaDelUsuario(string idUsuario);
        void ActualizarTiendas(TiendaVirtualDTO tiendaDTO); //Update
        void EliminarTienda(int tiendaId); //Delete
        void EliminarTiendaVirtual(int tiendaId);


        void EditarCss(int id,string css);
    }
}

using Crosscutting.Entity;
using Crosscutting.EntityTenant;
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
        void ActualizarCSS(TiendaVirtualDTO tiendaDTO); //Update
        void EliminarTienda(int tiendaId); //Delete
        void EliminarTiendaVirtual(int tiendaId);
        bool EliminarImagenTienda(int tiendaId, string nombre);
        List<ImagenesDTO> ObtenerImagenes(int tiendaId);

        void AgregarImagenTienda(ImagenesDTO img);

        void EditarCss(int id,string css);

        List<string> ObtenerTenants();

        List<Usuario> ReportUsers(string dominio, DateTime fechaini, DateTime fechafin);
        List<SubastaAux> ReportSubasta(string dominio, DateTime fechaini, DateTime fechafin);

        List<ReporteLineal> ReportSubastaLineal(string dominio, DateTime fechaini, DateTime fechafin);
        List<ReporteLineal> ReportUsersLineal(string dominio, DateTime fechaini, DateTime fechafin);

        List<UsuarioAux> DetUsers(string dominio, DateTime fechaini);
        List<SubastaAux> DetSub(string dominio, DateTime fechaini);

        bool ExisteDominio(string dominio);
    }
}

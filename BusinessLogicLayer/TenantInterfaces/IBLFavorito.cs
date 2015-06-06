using Crosscutting.EntityTenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.TenantInterfaces
{
    public interface IBLFavorito
    {
        void AgregarFavorito(String tenant, Favorito favorito);
        Favorito ObtenerFavorito(String tenant, int idSbasta, int idUsuario);
        bool esFavorito(String tenant, int idSbasta, int idUsuario);
        List<Favorito> FavoritosByUsuario(String tenant, int idUsuario);
        void EliminarFavorito(String tenant, int idSubasta, int idUsuario);
    }
}

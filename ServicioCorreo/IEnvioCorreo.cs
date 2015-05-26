using Crosscutting.EntityTareas;
using System.Collections.Generic;

namespace ServicioCorreo
{
    public interface IEnvioCorreo
    {
        void enviarCorreos(List<Correo> correo); //Create
    }

    
}

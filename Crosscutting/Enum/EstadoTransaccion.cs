using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crosscutting.Enum
{
    public enum EstadoTransaccion : int
    {
        Pendiente =0 ,
        Activa,
        Cerrada,
        Bloqueda,
        Pausar,
        Calificada
    }
}

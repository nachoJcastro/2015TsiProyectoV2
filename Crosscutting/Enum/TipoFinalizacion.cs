using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crosscutting.Enum
{
    public enum TipoFinalizacion: int
    {
        [Description("Subasta")]
        Subasta = 0,
        [Description("Directa")]
        Compra_directa
    }

    
}

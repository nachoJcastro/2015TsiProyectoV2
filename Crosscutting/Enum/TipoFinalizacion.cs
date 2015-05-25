using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crosscutting.Enum
{
    public enum TipoFinalizacion
    {
        [Description("Subasta")]
        Subasta,
        [Description("Directa")]
        Compra_directa,
    }

    
}

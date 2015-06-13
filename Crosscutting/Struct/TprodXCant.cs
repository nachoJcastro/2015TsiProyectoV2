using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crosscutting.Struct
{
    public struct TprodXCant
    {
        public TprodXCant (int posicion, int idTProd, int cantidad)
        {
            this.posicion= posicion;
            this.idTProd = idTProd;
            this.cantidad = cantidad;
        }

        public int posicion;
        public int idTProd;
        public int cantidad;

        public void setCantidad(int cant)
        {
            this.cantidad = cant;
        }
        
    }
}

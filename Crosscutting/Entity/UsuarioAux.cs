using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crosscutting.Entity
{
    public class UsuarioAux
    {
        public int id { get; set; }
        public string nick { get; set; }
        public string password { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public DateTime fecha_Nacimiento { get; set; }
        public string email { get; set; }
        public string direccion { get; set; }
        public string imagen { get; set; }
        public DateTime fecha_Alta { get; set; }
        public string reputacion_Venta { get; set; }
        public string reputacion_Compra { get; set; }
    }
}

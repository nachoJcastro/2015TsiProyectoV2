using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class UsuarioReporte
    {
        public string tipo { get; set; }
        public string nick { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public DateTime? fecha_Alta { get; set; }
    }
}
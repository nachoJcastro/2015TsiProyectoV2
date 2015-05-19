using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Site.Models
{
    public class OfertaModel
    {

        public int id { get; set; }

        public double Monto { get; set; }

        public String nombre { get; set; }

        public int id_Usuario { get; set; }

        public DateTime fecha { get; set; }

        public int id_Subasta { get; set; }

    }
}

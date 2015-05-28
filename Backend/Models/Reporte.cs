using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class Reporte
    {
        public int idTienda { get; set; }
        public string dominio { get; set; }
        public DateTime fechaini { get; set; }
        public DateTime fechafin { get; set; }
    }
}
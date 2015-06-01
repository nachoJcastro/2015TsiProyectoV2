using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class SubastaReporte
    {
        public string tipo { get; set; }
        public string titulo { get; set; }
        public double? precio_Base { get; set; }
        public DateTime? fecha_Inicio { get; set; }
    }
}
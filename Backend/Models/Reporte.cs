using Crosscutting.Entity;
using Crosscutting.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class Reporte
    {
       // public Reporte(){
        //    this.ListaTienda = new HashSet<TiendaVirtualDTO>();
        //}
        [Required(ErrorMessage = "Ingrese el dominio del reporte")]
        public string dominio { get; set; }
        [Required(ErrorMessage = "Ingrese el tipo del reporte")]
        public TipoReporte tipo { get; set; }
        [Required(ErrorMessage = "Ingrese la fecha del reporte")]
        public DateTime fechaini { get; set; }
        [Required(ErrorMessage = "Ingrese la fecha de fin del reporte")]
        public DateTime fechafin { get; set; }


        //public virtual ICollection<TiendaVirtualDTO> ListaTienda { get; set; }
    }
}
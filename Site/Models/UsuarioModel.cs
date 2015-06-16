using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class UsuarioModel
    {
        public UsuarioModel(){
        }

        public int id { get; set; }

        public string nick { get; set; }

        public string password { get; set; }

        public string nombre { get; set; }

        public string apellido { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime fecha_Nacimiento { get; set; }

        public string email { get; set; }

        public string direccion { get; set; }

        public string coordenadas { get; set; }

        public string preferencias { get; set; }

        public string imagen { get; set; }

        public double billetera { get; set; }

        public string telefono { get; set; }

        public DateTime fecha_Alta { get; set; }

        public string reputacion_Venta { get; set; }

        public string reputacion_Compra { get; set; }

    }
}
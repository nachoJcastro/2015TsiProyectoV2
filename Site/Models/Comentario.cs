using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Site.Models
{
    public class ComentarioModel
    {
        public int id { get; set; }

        public string texto { get; set; }

        public DateTime fecha { get; set; }

        public string nombreUsuario { get; set; }

        public int id_Usuario { get; set; }

        public int id_Subasta { get; set; }

        public string imagen { get; set; }
    }
}
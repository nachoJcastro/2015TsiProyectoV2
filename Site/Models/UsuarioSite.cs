using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class UsuarioSite
    {
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Dominio { get; set; }
        public int idTienda { get; set; }
        public int idUsuario { get; set; }
        public string IdConexion_Chat { get; set; }
        public string imagen { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class AtributoAux
    {
        public int AtributoId { get; set; }
        public int CategoriaId { get; set; }
        public string NombreCategoria { get; set; }
        public string NombreAtributo { get; set; }
    }
}
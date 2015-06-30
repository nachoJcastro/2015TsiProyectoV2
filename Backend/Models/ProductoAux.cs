using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class ProductoAux
    {
        public int TipoProductoId { get; set; }
        public int CategoriaId { get; set; }
        public string NombreCategoria { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
    }
}
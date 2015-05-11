using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crosscutting.Entity
{
    public class TipoProductoDTO
    {
        [Key]
        public int TipoProductoId { get; set; }

        public int CategoriaId { get; set; }
        public virtual CategoriasDTO Categoria { get; set; }

        [Required(ErrorMessage = "Ingrese el titulo del tipo de producto")]
        [Display(Name = "Titulo")]
        public string Titulo { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }


        //public string Tags { get; set; }

        //public string Precio_base { get; set; }

        //public string Precio_compra { get; set; }

        //public string Coordenadas { get; set; }

    }
}

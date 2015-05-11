using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crosscutting.Entity
{
    public class AtributosDTO
    {
        [Key]
        public int AtributoId { get; set; }

        public int CategoriaId { get; set; }
        public virtual CategoriasDTO Categoria { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre del atributo")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
    }
}

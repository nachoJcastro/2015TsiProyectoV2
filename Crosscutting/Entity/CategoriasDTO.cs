using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crosscutting.Entity
{

    public class CategoriasDTO
    {
        public CategoriasDTO() 
        {
            this.TipoProductos = new HashSet<TipoProductoDTO>();
            this.Atributos = new HashSet<AtributosDTO>();
        }

        [Key]
        public int CategoriaId { get; set; }

        public int TiendaId { get; set; }
        public virtual TiendaVirtualDTO TiendaVirtual { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        public virtual ICollection<TipoProductoDTO> TipoProductos { get; set; }
        public virtual ICollection<AtributosDTO> Atributos { get; set; }
    }
}

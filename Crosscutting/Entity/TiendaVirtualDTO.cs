using Crosscutting.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crosscutting.Entity
{
    public class TiendaVirtualDTO
    {
        
        public TiendaVirtualDTO()
        {
            this.Categorias = new HashSet<CategoriasDTO>();
            this.ListaImagenes = new HashSet<ImagenesDTO>();
            this.Estado = true;
        }

        [Key]
        public int TiendaVitualId { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre de la tienda virtual")]
        [Display(Name = "Nombre")]
        [MaxLength(50)]
        public string Nombre { get; set; }

        public string UsuarioId { get; set; }

        [Required(ErrorMessage = "Ingrese el dominio de la tienda virtural")]
        [Display(Name = "Dominio")]
        [MaxLength(20)]
        public string Dominio { get; set; }

        [Required(ErrorMessage = "Ingrese la descripción de la tienda virtural")]
        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        [StringLength(1024)]
        public string Descripcion { get; set; }

        [Display(Name = "Logo")]
        public string Logo { get; set; }

        [Display(Name = "Fecha creacion")]
        public DateTime Fecha_creacion { get; set; }

        public bool Estado { get; set; }

        public string Css { get; set; }

        public string StringConection { get; set; }

        public virtual ICollection<CategoriasDTO> Categorias { get; set; }
        public virtual ICollection<ImagenesDTO> ListaImagenes { get; set; }
        
    }
}

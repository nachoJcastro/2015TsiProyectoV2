using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crosscutting.EntityTenant
{
    [Table("Categoria")]
    public partial class Categoria
    {
        public Categoria()
        {
            Atributo = new HashSet<Atributo>();
            Producto = new HashSet<Producto>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        public virtual ICollection<Atributo> Atributo { get; set; }

        public virtual ICollection<Producto> Producto { get; set; }
    }
}

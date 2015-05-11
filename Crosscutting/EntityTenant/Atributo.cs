using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Crosscutting.EntityTenant
{
   
    [Table("Atributo")]
    public partial class Atributo
    {
        public Atributo()
        {
            Atributo_Subasta = new HashSet<Atributo_Subasta>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        public double valor { get; set; }

        public int id_Categoria { get; set; }

        public int id_Producto { get; set; }

        public virtual Categoria Categoria { get; set; }

        public virtual Producto Producto { get; set; }

        public virtual ICollection<Atributo_Subasta> Atributo_Subasta { get; set; }
    }
}

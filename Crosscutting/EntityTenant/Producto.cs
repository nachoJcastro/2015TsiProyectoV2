using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Crosscutting.EntityTenant
{

    [Table("Producto")]
    public partial class Producto
    {
        public Producto()
        {
            Atributo = new HashSet<Atributo>();
            Favorito = new HashSet<Favorito>();
            Subasta = new HashSet<Subasta>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string titulo { get; set; }

        public string descripcion { get; set; }

        [StringLength(50)]
        public string tags { get; set; }

        public double? precio_Base { get; set; }

        public double? precio_Compra { get; set; }

        [StringLength(50)]
        public string garantia { get; set; }

        public string coordenadas { get; set; }

        public int id_Categoria { get; set; }

        public virtual ICollection<Atributo> Atributo { get; set; }

        public virtual Categoria Categoria { get; set; }

        public virtual ICollection<Favorito> Favorito { get; set; }

        public virtual ICollection<Subasta> Subasta { get; set; }
    }
}

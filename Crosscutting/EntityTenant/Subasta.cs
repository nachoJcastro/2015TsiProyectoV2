using Crosscutting.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Crosscutting.EntityTenant
{
    [Table("Subasta")]
    public partial class Subasta
    {
        public Subasta()
        {
            Atributo_Subasta = new HashSet<Atributo_Subasta>();
            Calificacion = new HashSet<Calificacion>();
            Imagen = new HashSet<Imagen>();
            Oferta = new HashSet<Oferta>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int id_Comprador { get; set; }

        public int id_Vendedor { get; set; }

        public int id_Producto { get; set; }

        public EstadoTransaccion estado { get; set; }

        public double valor_Actual { get; set; }

        public TipoFinalizacion finalizado { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fecha_Inicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fecha_Cierre { get; set; }

        public virtual ICollection<Atributo_Subasta> Atributo_Subasta { get; set; }

        public virtual ICollection<Calificacion> Calificacion { get; set; }

        public virtual ICollection<Imagen> Imagen { get; set; }

        public virtual ICollection<Oferta> Oferta { get; set; }

        public virtual Producto Producto { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Usuario Usuario1 { get; set; }
    }
}

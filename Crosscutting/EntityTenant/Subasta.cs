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
            Favorito = new HashSet<Favorito>();
            Comentario = new HashSet<Comentario>();
        }

        // Id
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        // Atributos de los usuarios que intervienen

        
        // puede no tener comprador una subasta
        public int? id_Comprador { get; set; }

        public int id_Vendedor { get; set; }

        // Atributos del producto de la subasta

        public int id_Categoria { get; set; }

        public int id_Producto { get; set; }

        public virtual ICollection<Atributo_Subasta> Atributo_Subasta { get; set; }

        // Atributos de descripcion
        
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
       
        // Atributo de estado
        
        public EstadoTransaccion estado { get; set; }
       
        public TipoFinalizacion finalizado { get; set; }

        // Atributo de valor 

        public double valor_Actual { get; set; }

        // Atributos de fecha

        [Column(TypeName = "date")]
        public DateTime? fecha_Inicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fecha_Cierre { get; set; }

        public virtual ICollection<Calificacion> Calificacion { get; set; }

        public virtual ICollection<Imagen> Imagen { get; set; }

        public virtual ICollection<Oferta> Oferta { get; set; }

        public virtual ICollection<Favorito> Favorito { get; set; }

        public virtual ICollection<Comentario> Comentario { get; set; }

        public virtual Usuario Vendedor { get; set; }
    }
}

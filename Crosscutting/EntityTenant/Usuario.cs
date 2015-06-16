using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crosscutting.EntityTenant
{

    [Table("Usuario")]
    public partial class Usuario
    {
        public Usuario()
        {
            Calificacion = new HashSet<Calificacion>();
            Comentario = new HashSet<Comentario>();
            Favorito = new HashSet<Favorito>();
            Subasta = new HashSet<Subasta>();
            Oferta = new HashSet<Oferta>();
            billetera = 1000;
        }

        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string nick { get; set; }

        [Required]
        [StringLength(50)]
        public string password { get; set; }

        [StringLength(50)]
        public string nombre { get; set; }

        [StringLength(50)]
        public string apellido { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime fecha_Nacimiento { get; set; }

        [Required]
        [StringLength(50)]
        public string email { get; set; }

        public string direccion { get; set; }

        public string coordenadas { get; set; }

        public string preferencias { get; set; }

        public string imagen { get; set; }

        public double billetera { get; set; }

        public string telefono { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? fecha_Alta { get; set; }

        [StringLength(50)]
        public string reputacion_Venta { get; set; }

        [StringLength(50)]
        public string reputacion_Compra { get; set; }

        public virtual ICollection<Calificacion> Calificacion { get; set; }

        public virtual ICollection<Comentario> Comentario { get; set; }

        public virtual ICollection<Favorito> Favorito { get; set; }

        public virtual ICollection<Subasta> Subasta { get; set; }

        public virtual ICollection<Oferta> Oferta { get; set; }
    }
}

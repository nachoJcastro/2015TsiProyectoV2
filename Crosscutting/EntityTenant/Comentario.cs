using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Crosscutting.EntityTenant
{
    [Table("Comentario")]
    public partial class Comentario
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public string texto { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha { get; set; }

        public int id_Usuario { get; set; }

        public int id_Subasta { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Subasta Subasta { get; set; }
    }
}

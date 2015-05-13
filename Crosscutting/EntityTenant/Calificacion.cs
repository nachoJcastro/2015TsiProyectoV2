using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crosscutting.EntityTenant
{

    [Table("Calificacion")]
    public partial class Calificacion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [StringLength(50)]
        public string observacion { get; set; }

        public int id_Usuario { get; set; }

        public int id_Subasta { get; set; }

        public virtual Subasta Subasta { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}

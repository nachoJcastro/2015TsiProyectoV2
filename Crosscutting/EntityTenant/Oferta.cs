using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crosscutting.EntityTenant
{
    [Table("Oferta")]
    public partial class Oferta
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public double Monto { get; set; }

        public int id_Usuario { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha { get; set; }

        public int id_Subasta { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Subasta Subasta { get; set; }
    }
}

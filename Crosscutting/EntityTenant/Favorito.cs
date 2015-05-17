using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crosscutting.EntityTenant
{

    [Table("Favorito")]
    public partial class Favorito
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Column("favorito")]
        public bool favorito1 { get; set; }
        
        public int id_Usuario { get; set; }

        public int id_Subasta { get; set; }

        public virtual Subasta Subasta { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}

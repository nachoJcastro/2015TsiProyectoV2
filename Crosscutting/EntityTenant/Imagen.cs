using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Crosscutting.EntityTenant
{

    [Table("Imagen")]
    public partial class Imagen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int id_Subasta { get; set; }

        [Column("imagen")]
        [StringLength(50)]
        public string imagen1 { get; set; }

        public virtual Subasta Subasta { get; set; }
    }
}

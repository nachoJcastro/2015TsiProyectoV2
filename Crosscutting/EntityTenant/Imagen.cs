using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Crosscutting.EntityTenant
{

    [Table("Imagen")]
    public partial class Imagen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int id_Subasta { get; set; }

        [Column("nombre")]
        public string nombre { get; set; }

        [Column("uri")]
        public string uri { get; set; }

        public virtual Subasta Subasta { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crosscutting.EntityTenant
{

    [Table("Favorito")]
    public partial class Favorito
    {
        public int id_Usuario { get; set; }

        public int id_Producto { get; set; }

        [Column("favorito")]
        public bool favorito1 { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public virtual Producto Producto { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}

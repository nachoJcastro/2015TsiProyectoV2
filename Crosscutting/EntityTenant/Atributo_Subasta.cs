using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Crosscutting.EntityTenant
{
    

    public partial class Atributo_Subasta
    {
        public  Atributo_Subasta() { }

        public  Atributo_Subasta(int idA, string valA)
        {
            this.id_Atributo = idA;
            this.valor = valA;
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_Subasta { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_Atributo { get; set; }

        [Required]
        public string valor { get; set; }

        public virtual Subasta Subasta { get; set; }
    }
}

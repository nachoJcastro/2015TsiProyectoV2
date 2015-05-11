using Crosscutting.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configuration
{
    public class TiendaVirtualMap : EntityTypeConfiguration<TiendaVirtualDTO>
    {
        public TiendaVirtualMap() 
        {
            //Tabla
            this.ToTable("TiendasVirtuales");

            //Key
            this.HasKey(b => b.TiendaVitualId);

            //Autogenerar Key
            this.Property(b => b.TiendaVitualId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //Propiedades
            


        }

    }
}

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
    public class ImagenesMap : EntityTypeConfiguration<ImagenesDTO>
    {

        public ImagenesMap() 
        {
            //Tabla
            this.ToTable("Imagenes");

            //Key
            this.HasKey(b => b.ImageneId);

            //Autogenerar Key
            this.Property(b => b.ImageneId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //Propiedades

            //Relacion de muchos - Una tienda virtual tiene muchas imagenes
            this.HasRequired(b => b.TiendaVirtual).WithMany(b => b.ListaImagenes).HasForeignKey(b => b.TiendaId);

        }

    }
}

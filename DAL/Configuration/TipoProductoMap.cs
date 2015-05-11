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
    public class TipoProductoMap:EntityTypeConfiguration<TipoProductoDTO>
    {
        public TipoProductoMap() 
        {
            //Tabla
            this.ToTable("TipoProductos");

            //Key
            this.HasKey(b => b.TipoProductoId);

            //Autogenerar Key
            this.Property(b => b.TipoProductoId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //Propiedades
            
            //titulo
            this.Property(b => b.Titulo).IsRequired();
            
            //descripcion
            this.Property(b => b.Descripcion).HasMaxLength(1024);

            //Relacion de muchos - Una categoria tiene muchos tipo de productos
            this.HasRequired(b => b.Categoria).WithMany(b => b.TipoProductos).HasForeignKey(b => b.CategoriaId);


        }
    }
}

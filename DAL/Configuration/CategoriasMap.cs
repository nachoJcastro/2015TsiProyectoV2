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
    public class CategoriasMap : EntityTypeConfiguration<CategoriasDTO>
    {
        public CategoriasMap() 
        {
            //Tabla
            this.ToTable("Categorias");

            //Key
            this.HasKey(b => b.CategoriaId);

            //Autogenerar Key
            this.Property(b => b.CategoriaId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //Propiedades

            //Relacion de muchos - Una tienda virtual tiene muchas categorias
            this.HasRequired(b => b.TiendaVirtual).WithMany(b => b.Categorias).HasForeignKey(b => b.TiendaId);
        }
    }
}

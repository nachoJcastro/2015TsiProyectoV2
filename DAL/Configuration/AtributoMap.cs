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
    public class AtributoMap : EntityTypeConfiguration<AtributosDTO>
    {
        public AtributoMap() 
        {
            //Tabla
            this.ToTable("Atributos");

            //Key
            this.HasKey(b => b.AtributoId);

            //Autogenerar Key
            this.Property(b => b.AtributoId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //Propiedades
            
            //Nombre
            this.Property(b => b.Nombre).IsRequired();

            //Relacion de muchos - Una categoria tiene muchos atributos
            this.HasRequired(b => b.Categoria).WithMany(b => b.Atributos).HasForeignKey(b => b.CategoriaId);

        }

    }
}

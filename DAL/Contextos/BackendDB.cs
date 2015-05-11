using Crosscutting.Entity;
using DAL.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contextos
{
    public class BackendDB : DbContext
    {
        public BackendDB()
            : base("name=BackendDB")
        {
            //Esta linea lo que haces es que cuando cambia algo en el contexto, autamitacamente cambia la base.
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BackendDB>());
        }

        public DbSet<TiendaVirtualDTO> TiendaVirtual { get; set; }
        public DbSet<ImagenesDTO> Imagenes { get; set; }
        public DbSet<CategoriasDTO> Categorias { get; set; }
        public DbSet<TipoProductoDTO> TiposProductos { get; set; }
        public DbSet<AtributosDTO> Atributos { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TiendaVirtualMap());
            modelBuilder.Configurations.Add(new ImagenesMap());
            modelBuilder.Configurations.Add(new CategoriasMap());
            modelBuilder.Configurations.Add(new TipoProductoMap());
            modelBuilder.Configurations.Add(new AtributoMap());

            base.OnModelCreating(modelBuilder);
        }

    }
}

using System;

using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Crosscutting.EntityTenant;

using System.Security.Claims;

using System.Threading.Tasks;
using System.Data.Entity;
 

namespace DAL.Contextos
{
    public class TenantDB : DbContext
    {
        public TenantDB(string database)
            : base("Data Source=.;database=" + database + ";Integrated Security=True")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TenantDB>());
        }
        
        public virtual DbSet<Atributo_Subasta> Atributo_Subasta { get; set; }
        public virtual DbSet<Calificacion> Calificacion { get; set; }
        public virtual DbSet<Comentario> Comentario { get; set; }
        public virtual DbSet<Favorito> Favorito { get; set; }
        public virtual DbSet<Imagen> Imagen { get; set; }
        public virtual DbSet<Oferta> Oferta { get; set; }
        public virtual DbSet<Subasta> Subasta { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            // dependencias de la Subasta
            modelBuilder.Entity<Subasta>()
                .HasMany(e => e.Atributo_Subasta)
                .WithRequired(e => e.Subasta)
                .HasForeignKey(e => e.id_Subasta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subasta>()
                .HasMany(e => e.Calificacion)
                .WithRequired(e => e.Subasta)
                .HasForeignKey(e => e.id_Subasta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subasta>()
                .HasMany(e => e.Imagen)
                .WithRequired(e => e.Subasta)
                .HasForeignKey(e => e.id_Subasta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subasta>()
                .HasMany(e => e.Oferta)
                .WithRequired(e => e.Subasta)
                .HasForeignKey(e => e.id_Subasta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subasta>()
               .HasMany(e => e.Comentario)
               .WithRequired(e => e.Subasta)
               .HasForeignKey(e => e.id_Subasta)
               .WillCascadeOnDelete(false);


            // dependencias del Usuario
            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Calificacion)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.id_Usuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Comentario)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.id_Usuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Favorito)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.id_Usuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Subasta)
                .WithRequired(e => e.Vendedor)
                .HasForeignKey(e => e.id_Vendedor)
                .WillCascadeOnDelete(false);

            
            base.OnModelCreating(modelBuilder);
        }
    }
}

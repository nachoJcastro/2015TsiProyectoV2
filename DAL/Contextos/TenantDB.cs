using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Crosscutting.EntityTenant;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
 

namespace DAL.Contextos
{
    public class TenantDB : DbContext
    {
        public TenantDB(string database)
            : base("Data Source=.;database=" + database + ";Integrated Security=True")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TenantDB>());
        }
        
        public virtual DbSet<Atributo> Atributo { get; set; }
        public virtual DbSet<Atributo_Subasta> Atributo_Subasta { get; set; }
        public virtual DbSet<Calificacion> Calificacion { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Comentario> Comentario { get; set; }
        public virtual DbSet<Favorito> Favorito { get; set; }
        public virtual DbSet<Imagen> Imagen { get; set; }
        public virtual DbSet<Oferta> Oferta { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Subasta> Subasta { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Atributo>()
                .HasMany(e => e.Atributo_Subasta)
                .WithRequired(e => e.Atributo)
                .HasForeignKey(e => e.id_Atributo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Categoria>()
                .HasMany(e => e.Atributo)
                .WithRequired(e => e.Categoria)
                .HasForeignKey(e => e.id_Categoria)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Categoria>()
                .HasMany(e => e.Producto)
                .WithRequired(e => e.Categoria)
                .HasForeignKey(e => e.id_Categoria)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Producto>()
                .HasMany(e => e.Atributo)
                .WithRequired(e => e.Producto)
                .HasForeignKey(e => e.id_Producto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Producto>()
                .HasMany(e => e.Favorito)
                .WithRequired(e => e.Producto)
                .HasForeignKey(e => e.id_Producto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Producto>()
                .HasMany(e => e.Subasta)
                .WithRequired(e => e.Producto)
                .HasForeignKey(e => e.id_Producto)
                .WillCascadeOnDelete(false);

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
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.id_Comprador)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Subasta1)
                .WithRequired(e => e.Usuario1)
                .HasForeignKey(e => e.id_Vendedor)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}

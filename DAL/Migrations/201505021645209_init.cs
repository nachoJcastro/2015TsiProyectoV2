namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Atributos",
                c => new
                    {
                        AtributoId = c.Int(nullable: false, identity: true),
                        CategoriaId = c.Int(nullable: false),
                        Nombre = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AtributoId)
                .ForeignKey("dbo.Categorias", t => t.CategoriaId, cascadeDelete: true)
                .Index(t => t.CategoriaId);
            
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        CategoriaId = c.Int(nullable: false, identity: true),
                        TiendaId = c.Int(nullable: false),
                        Nombre = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.CategoriaId)
                .ForeignKey("dbo.TiendasVirtuales", t => t.TiendaId, cascadeDelete: true)
                .Index(t => t.TiendaId);
            
            CreateTable(
                "dbo.TiendasVirtuales",
                c => new
                    {
                        TiendaVitualId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        UsuarioId = c.String(),
                        Dominio = c.String(nullable: false, maxLength: 20),
                        Descripcion = c.String(nullable: false, maxLength: 1024),
                        Logo = c.String(),
                        Fecha_creacion = c.DateTime(nullable: false),
                        Estado = c.Boolean(nullable: false),
                        Css = c.Int(nullable: false),
                        StringConection = c.String(),
                    })
                .PrimaryKey(t => t.TiendaVitualId);
            
            CreateTable(
                "dbo.Imagenes",
                c => new
                    {
                        ImageneId = c.Int(nullable: false, identity: true),
                        TiendaId = c.Int(nullable: false),
                        UrlImagenMediana = c.String(),
                        EsPortada = c.Boolean(nullable: false),
                        ImagenEliminada = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ImageneId)
                .ForeignKey("dbo.TiendasVirtuales", t => t.TiendaId, cascadeDelete: true)
                .Index(t => t.TiendaId);
            
            CreateTable(
                "dbo.TipoProductos",
                c => new
                    {
                        TipoProductoId = c.Int(nullable: false, identity: true),
                        CategoriaId = c.Int(nullable: false),
                        Titulo = c.String(nullable: false),
                        Descripcion = c.String(maxLength: 1024),
                    })
                .PrimaryKey(t => t.TipoProductoId)
                .ForeignKey("dbo.Categorias", t => t.CategoriaId, cascadeDelete: true)
                .Index(t => t.CategoriaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Atributos", "CategoriaId", "dbo.Categorias");
            DropForeignKey("dbo.TipoProductos", "CategoriaId", "dbo.Categorias");
            DropForeignKey("dbo.Categorias", "TiendaId", "dbo.TiendasVirtuales");
            DropForeignKey("dbo.Imagenes", "TiendaId", "dbo.TiendasVirtuales");
            DropIndex("dbo.TipoProductos", new[] { "CategoriaId" });
            DropIndex("dbo.Imagenes", new[] { "TiendaId" });
            DropIndex("dbo.Categorias", new[] { "TiendaId" });
            DropIndex("dbo.Atributos", new[] { "CategoriaId" });
            DropTable("dbo.TipoProductos");
            DropTable("dbo.Imagenes");
            DropTable("dbo.TiendasVirtuales");
            DropTable("dbo.Categorias");
            DropTable("dbo.Atributos");
        }
    }
}

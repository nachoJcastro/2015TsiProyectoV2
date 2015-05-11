namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Nombre");
            DropColumn("dbo.AspNetUsers", "Apellido");
            DropColumn("dbo.AspNetUsers", "FechaNacimiento");
            DropColumn("dbo.AspNetUsers", "Direccion");
            DropColumn("dbo.AspNetUsers", "Imagen");
            DropColumn("dbo.AspNetUsers", "Estado");
            DropColumn("dbo.AspNetUsers", "Es_Admin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Es_Admin", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "Estado", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "Imagen", c => c.String());
            AddColumn("dbo.AspNetUsers", "Direccion", c => c.String());
            AddColumn("dbo.AspNetUsers", "FechaNacimiento", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "Apellido", c => c.String());
            AddColumn("dbo.AspNetUsers", "Nombre", c => c.String());
        }
    }
}

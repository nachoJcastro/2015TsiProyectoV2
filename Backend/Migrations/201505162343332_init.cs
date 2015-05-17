namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Nombre", c => c.String());
            AddColumn("dbo.AspNetUsers", "Apellido", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Apellido");
            DropColumn("dbo.AspNetUsers", "Nombre");
        }
    }
}

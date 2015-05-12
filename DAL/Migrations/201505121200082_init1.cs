namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TiendasVirtuales", "Css", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TiendasVirtuales", "Css", c => c.Int(nullable: false));
        }
    }
}

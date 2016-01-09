namespace QuickCode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedplantfkfromdtpye : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DowntimeTypes", "PlantID", "dbo.Plants");
            DropIndex("dbo.DowntimeTypes", new[] { "PlantID" });
            DropColumn("dbo.DowntimeTypes", "PlantID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DowntimeTypes", "PlantID", c => c.Int(nullable: false));
            CreateIndex("dbo.DowntimeTypes", "PlantID");
            AddForeignKey("dbo.DowntimeTypes", "PlantID", "dbo.Plants", "PlantID", cascadeDelete: true);
        }
    }
}

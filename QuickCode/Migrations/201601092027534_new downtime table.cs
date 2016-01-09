namespace QuickCode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newdowntimetable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Downtimes", "PlantID", c => c.Int(nullable: false));
            CreateIndex("dbo.Downtimes", "DowntimeTypeID");
            CreateIndex("dbo.Downtimes", "PlantID");
            AddForeignKey("dbo.Downtimes", "DowntimeTypeID", "dbo.DowntimeTypes", "DowntimeTypeID", cascadeDelete: true);
            AddForeignKey("dbo.Downtimes", "PlantID", "dbo.Plants", "PlantID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Downtimes", "PlantID", "dbo.Plants");
            DropForeignKey("dbo.Downtimes", "DowntimeTypeID", "dbo.DowntimeTypes");
            DropIndex("dbo.Downtimes", new[] { "PlantID" });
            DropIndex("dbo.Downtimes", new[] { "DowntimeTypeID" });
            DropColumn("dbo.Downtimes", "PlantID");
        }
    }
}

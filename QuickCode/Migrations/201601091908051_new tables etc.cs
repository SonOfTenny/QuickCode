namespace QuickCode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newtablesetc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DowntimeTypes",
                c => new
                    {
                        DowntimeTypeID = c.Int(nullable: false, identity: true),
                        PlantID = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DowntimeTypeID)
                .ForeignKey("dbo.Plants", t => t.PlantID, cascadeDelete: true)
                .Index(t => t.PlantID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DowntimeTypes", "PlantID", "dbo.Plants");
            DropIndex("dbo.DowntimeTypes", new[] { "PlantID" });
            DropTable("dbo.DowntimeTypes");
        }
    }
}

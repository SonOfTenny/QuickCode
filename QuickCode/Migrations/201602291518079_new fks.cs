namespace QuickCode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newfks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "AccessType_AccessID", c => c.Int());
            CreateIndex("dbo.User", "AccessType_AccessID");
            AddForeignKey("dbo.User", "AccessType_AccessID", "dbo.AccessTypes", "AccessID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "AccessType_AccessID", "dbo.AccessTypes");
            DropIndex("dbo.User", new[] { "AccessType_AccessID" });
            DropColumn("dbo.User", "AccessType_AccessID");
        }
    }
}

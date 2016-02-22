namespace QuickCode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addednew : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserAccessTypes", "AccessID", c => c.Int(nullable: false));
            CreateIndex("dbo.UserAccessTypes", "AccessID");
            AddForeignKey("dbo.UserAccessTypes", "AccessID", "dbo.AccessTypes", "AccessID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAccessTypes", "AccessID", "dbo.AccessTypes");
            DropIndex("dbo.UserAccessTypes", new[] { "AccessID" });
            DropColumn("dbo.UserAccessTypes", "AccessID");
        }
    }
}

namespace QuickCode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changed : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserAccessTypes",
                c => new
                    {
                        UserAccessID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserAccessID)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAccessTypes", "UserID", "dbo.User");
            DropIndex("dbo.UserAccessTypes", new[] { "UserID" });
            DropTable("dbo.UserAccessTypes");
        }
    }
}

namespace QuickCode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fkey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccessTypes", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AccessTypes", "User_Id");
            AddForeignKey("dbo.AccessTypes", "User_Id", "dbo.User", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccessTypes", "User_Id", "dbo.User");
            DropIndex("dbo.AccessTypes", new[] { "User_Id" });
            DropColumn("dbo.AccessTypes", "User_Id");
        }
    }
}

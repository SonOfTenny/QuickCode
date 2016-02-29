namespace QuickCode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newshit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRole", "User_Id", "dbo.User");
            DropIndex("dbo.UserRole", new[] { "User_Id" });
            DropColumn("dbo.UserRole", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserRole", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserRole", "User_Id");
            AddForeignKey("dbo.UserRole", "User_Id", "dbo.User", "UserId");
        }
    }
}

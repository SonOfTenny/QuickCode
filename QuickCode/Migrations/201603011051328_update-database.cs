namespace QuickCode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserRole", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.UserRole", "Role_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Role", "Description", c => c.String());
            AddColumn("dbo.Role", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.UserRole", "Role_Id");
            AddForeignKey("dbo.UserRole", "Role_Id", "dbo.Role", "RoleId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "Role_Id", "dbo.Role");
            DropIndex("dbo.UserRole", new[] { "Role_Id" });
            DropColumn("dbo.Role", "Discriminator");
            DropColumn("dbo.Role", "Description");
            DropColumn("dbo.UserRole", "Role_Id");
            DropColumn("dbo.UserRole", "Discriminator");
        }
    }
}

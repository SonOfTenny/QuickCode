namespace QuickCode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccessTypes",
                c => new
                    {
                        AccessID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.AccessID);
            
            CreateTable(
                "dbo.UserAccessTypes",
                c => new
                    {
                        UserAccessID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        AccessID = c.Int(nullable: false),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        Id = c.String(),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserAccessID)
                .ForeignKey("dbo.AccessTypes", t => t.AccessID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.AccessID);
            
            AddColumn("dbo.User", "RoleName", c => c.String());
            AddColumn("dbo.User", "AccessID", c => c.Int());
            AddColumn("dbo.UserClaim", "UserAccessTypes_UserAccessID", c => c.Int());
            AddColumn("dbo.UserLogin", "UserAccessTypes_UserAccessID", c => c.Int());
            AddColumn("dbo.UserRole", "UserAccessTypes_UserAccessID", c => c.Int());
            CreateIndex("dbo.UserClaim", "UserAccessTypes_UserAccessID");
            CreateIndex("dbo.UserLogin", "UserAccessTypes_UserAccessID");
            CreateIndex("dbo.UserRole", "UserAccessTypes_UserAccessID");
            AddForeignKey("dbo.UserClaim", "UserAccessTypes_UserAccessID", "dbo.UserAccessTypes", "UserAccessID");
            AddForeignKey("dbo.UserLogin", "UserAccessTypes_UserAccessID", "dbo.UserAccessTypes", "UserAccessID");
            AddForeignKey("dbo.UserRole", "UserAccessTypes_UserAccessID", "dbo.UserAccessTypes", "UserAccessID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAccessTypes", "UserID", "dbo.User");
            DropForeignKey("dbo.UserRole", "UserAccessTypes_UserAccessID", "dbo.UserAccessTypes");
            DropForeignKey("dbo.UserLogin", "UserAccessTypes_UserAccessID", "dbo.UserAccessTypes");
            DropForeignKey("dbo.UserClaim", "UserAccessTypes_UserAccessID", "dbo.UserAccessTypes");
            DropForeignKey("dbo.UserAccessTypes", "AccessID", "dbo.AccessTypes");
            DropIndex("dbo.UserAccessTypes", new[] { "AccessID" });
            DropIndex("dbo.UserAccessTypes", new[] { "UserID" });
            DropIndex("dbo.UserRole", new[] { "UserAccessTypes_UserAccessID" });
            DropIndex("dbo.UserLogin", new[] { "UserAccessTypes_UserAccessID" });
            DropIndex("dbo.UserClaim", new[] { "UserAccessTypes_UserAccessID" });
            DropColumn("dbo.UserRole", "UserAccessTypes_UserAccessID");
            DropColumn("dbo.UserLogin", "UserAccessTypes_UserAccessID");
            DropColumn("dbo.UserClaim", "UserAccessTypes_UserAccessID");
            DropColumn("dbo.User", "AccessID");
            DropColumn("dbo.User", "RoleName");
            DropTable("dbo.UserAccessTypes");
            DropTable("dbo.AccessTypes");
        }
    }
}

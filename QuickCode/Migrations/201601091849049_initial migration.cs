namespace QuickCode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.RoleId)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaim",
                c => new
                    {
                        UserClaimId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.UserClaimId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Downtimes",
                c => new
                    {
                        DowntimeID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        ShiftID = c.Int(nullable: false),
                        DowntimeTypeID = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Reason = c.String(),
                        Action = c.String(),
                        Date = c.DateTime(nullable: false),
                        TotalDownMins = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.DowntimeID)
                .ForeignKey("dbo.Shifts", t => t.ShiftID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.ShiftID);
            
            CreateTable(
                "dbo.Shifts",
                c => new
                    {
                        ShiftID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ShiftID);
            
            CreateTable(
                "dbo.UserLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Productions",
                c => new
                    {
                        ProductionID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        ShiftID = c.Int(nullable: false),
                        PlantID = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        ActualMix = c.Int(nullable: false),
                        CrumbWaste = c.Int(nullable: false),
                        Cmp_Waste = c.Int(nullable: false),
                        Manning = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        TotalWaste = c.Int(nullable: false),
                        TotalProdMins = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ProductionID)
                .ForeignKey("dbo.Plants", t => t.PlantID, cascadeDelete: true)
                .ForeignKey("dbo.Shifts", t => t.ShiftID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.ShiftID)
                .Index(t => t.PlantID);
            
            CreateTable(
                "dbo.Plants",
                c => new
                    {
                        PlantID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MixRatePerHour = c.Double(),
                    })
                .PrimaryKey(t => t.PlantID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.Productions", "UserID", "dbo.User");
            DropForeignKey("dbo.Productions", "ShiftID", "dbo.Shifts");
            DropForeignKey("dbo.Productions", "PlantID", "dbo.Plants");
            DropForeignKey("dbo.UserLogin", "UserId", "dbo.User");
            DropForeignKey("dbo.Downtimes", "UserID", "dbo.User");
            DropForeignKey("dbo.Downtimes", "ShiftID", "dbo.Shifts");
            DropForeignKey("dbo.UserClaim", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropIndex("dbo.Productions", new[] { "PlantID" });
            DropIndex("dbo.Productions", new[] { "ShiftID" });
            DropIndex("dbo.Productions", new[] { "UserID" });
            DropIndex("dbo.UserLogin", new[] { "UserId" });
            DropIndex("dbo.Downtimes", new[] { "ShiftID" });
            DropIndex("dbo.Downtimes", new[] { "UserID" });
            DropIndex("dbo.UserClaim", new[] { "UserId" });
            DropIndex("dbo.User", "UserNameIndex");
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.Role", "RoleNameIndex");
            DropTable("dbo.Plants");
            DropTable("dbo.Productions");
            DropTable("dbo.UserLogin");
            DropTable("dbo.Shifts");
            DropTable("dbo.Downtimes");
            DropTable("dbo.UserClaim");
            DropTable("dbo.User");
            DropTable("dbo.UserRole");
            DropTable("dbo.Role");
        }
    }
}

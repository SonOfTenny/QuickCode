namespace QuickCode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doubles : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Productions", "ActualMix", c => c.Double(nullable: false));
            AlterColumn("dbo.Productions", "CrumbWaste", c => c.Double(nullable: false));
            AlterColumn("dbo.Productions", "Cmp_Waste", c => c.Double(nullable: false));
            AlterColumn("dbo.Productions", "Pack_Waste", c => c.Double(nullable: false));
            AlterColumn("dbo.Productions", "Gen_Pack_Waste", c => c.Double(nullable: false));
            AlterColumn("dbo.Productions", "StdManning", c => c.Double(nullable: false));
            AlterColumn("dbo.Productions", "AgencyManning", c => c.Double(nullable: false));
            AlterColumn("dbo.Productions", "OpManning", c => c.Double(nullable: false));
            AlterColumn("dbo.Productions", "Manning", c => c.Double(nullable: false));
            AlterColumn("dbo.Productions", "TotalWaste", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Productions", "TotalWaste", c => c.Int(nullable: false));
            AlterColumn("dbo.Productions", "Manning", c => c.String());
            AlterColumn("dbo.Productions", "OpManning", c => c.Int(nullable: false));
            AlterColumn("dbo.Productions", "AgencyManning", c => c.Int(nullable: false));
            AlterColumn("dbo.Productions", "StdManning", c => c.Int(nullable: false));
            AlterColumn("dbo.Productions", "Gen_Pack_Waste", c => c.Int(nullable: false));
            AlterColumn("dbo.Productions", "Pack_Waste", c => c.Int(nullable: false));
            AlterColumn("dbo.Productions", "Cmp_Waste", c => c.Int(nullable: false));
            AlterColumn("dbo.Productions", "CrumbWaste", c => c.Int(nullable: false));
            AlterColumn("dbo.Productions", "ActualMix", c => c.Int(nullable: false));
        }
    }
}

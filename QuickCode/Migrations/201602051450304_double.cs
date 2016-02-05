namespace QuickCode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _double : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Productions", "ActualMix", c => c.Double(nullable: false));
            AlterColumn("dbo.Productions", "CrumbWaste", c => c.Double(nullable: false));
            AlterColumn("dbo.Productions", "Cmp_Waste", c => c.Double(nullable: false));
            AlterColumn("dbo.Productions", "Pack_Waste", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Productions", "Pack_Waste", c => c.Int(nullable: false));
            AlterColumn("dbo.Productions", "Cmp_Waste", c => c.Int(nullable: false));
            AlterColumn("dbo.Productions", "CrumbWaste", c => c.Int(nullable: false));
            AlterColumn("dbo.Productions", "ActualMix", c => c.Int(nullable: false));
        }
    }
}

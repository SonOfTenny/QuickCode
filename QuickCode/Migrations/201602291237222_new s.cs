namespace QuickCode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class news : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FiscalWeeks",
                c => new
                    {
                        FiscalWeekID = c.Int(nullable: false, identity: true),
                        FiscalYear = c.Int(nullable: false),
                        WeekName = c.String(),
                        WeekQuarter = c.Int(nullable: false),
                        WeekMonth = c.Int(nullable: false),
                        WeekNumber = c.Int(nullable: false),
                        WeekStart = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FiscalWeekID);
            
            AddColumn("dbo.Downtimes", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Downtimes", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Productions", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Productions", "EndDate", c => c.DateTime(nullable: false));
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
            DropColumn("dbo.Downtimes", "Date");
            DropColumn("dbo.Productions", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Productions", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Downtimes", "Date", c => c.DateTime(nullable: false));
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
            DropColumn("dbo.Productions", "EndDate");
            DropColumn("dbo.Productions", "StartDate");
            DropColumn("dbo.Downtimes", "EndDate");
            DropColumn("dbo.Downtimes", "StartDate");
            DropTable("dbo.FiscalWeeks");
        }
    }
}

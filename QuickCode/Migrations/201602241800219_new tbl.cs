namespace QuickCode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newtbl : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FiscalWeeks");
        }
    }
}

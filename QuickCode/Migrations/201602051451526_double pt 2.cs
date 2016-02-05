namespace QuickCode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doublept2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Productions", "TotalWaste", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Productions", "TotalWaste", c => c.Int(nullable: false));
        }
    }
}

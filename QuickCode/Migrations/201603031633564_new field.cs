namespace QuickCode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Productions", "QualityIssues", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Productions", "QualityIssues");
        }
    }
}

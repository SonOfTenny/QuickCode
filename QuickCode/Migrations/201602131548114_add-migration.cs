namespace QuickCode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Productions", "TotalProdMins", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Productions", "TotalProdMins", c => c.Double(nullable: false));
        }
    }
}

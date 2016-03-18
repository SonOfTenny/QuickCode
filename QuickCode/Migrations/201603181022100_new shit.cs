namespace QuickCode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newshit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Productions", "cleaner_Waste", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Productions", "cleaner_Waste");
        }
    }
}

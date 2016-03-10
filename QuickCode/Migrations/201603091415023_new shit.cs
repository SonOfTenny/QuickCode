namespace QuickCode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newshit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Productions", "sDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Productions", "sDate");
        }
    }
}

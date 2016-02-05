namespace QuickCode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedmanningtypes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Productions", "StdManning", c => c.Int(nullable: false));
            AddColumn("dbo.Productions", "AgencyManning", c => c.Int(nullable: false));
            AddColumn("dbo.Productions", "OpManning", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Productions", "OpManning");
            DropColumn("dbo.Productions", "AgencyManning");
            DropColumn("dbo.Productions", "StdManning");
        }
    }
}

namespace QuickCode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newst : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.User", "AccessID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "AccessID", c => c.Int());
        }
    }
}

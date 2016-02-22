namespace QuickCode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeddesctoaccess : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccessTypes", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccessTypes", "Description");
        }
    }
}

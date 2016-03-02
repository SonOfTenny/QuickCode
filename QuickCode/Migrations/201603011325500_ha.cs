namespace QuickCode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ha : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.User", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
    }
}

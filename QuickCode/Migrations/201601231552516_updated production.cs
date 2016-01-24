namespace QuickCode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedproduction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Productions", "Pack_Waste", c => c.Int(nullable: false));
            AddColumn("dbo.Productions", "Gen_Pack_Waste", c => c.Int(nullable: false));
            AlterColumn("dbo.Productions", "Manning", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Productions", "Manning", c => c.Int(nullable: false));
            DropColumn("dbo.Productions", "Gen_Pack_Waste");
            DropColumn("dbo.Productions", "Pack_Waste");
        }
    }
}

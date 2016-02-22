namespace QuickCode.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccessTypes",
                c => new
                    {
                        AccessID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.AccessID);
            
            DropColumn("dbo.User", "accessType_AccessID");
            DropColumn("dbo.User", "accessType_Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "accessType_Name", c => c.String());
            AddColumn("dbo.User", "accessType_AccessID", c => c.Int(nullable: false));
            DropTable("dbo.AccessTypes");
        }
    }
}

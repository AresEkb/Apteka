namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrgName300 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Organization", "Name", c => c.String(nullable: false, maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Organization", "Name", c => c.String(nullable: false, maxLength: 200));
        }
    }
}

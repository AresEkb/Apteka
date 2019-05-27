namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAltKeys : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Country", "Code", c => c.String(nullable: false, maxLength: 2, fixedLength: true, unicode: false));
            AlterColumn("dbo.Country", "Name", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Organization", "Name", c => c.String(nullable: false, maxLength: 200));
            CreateIndex("dbo.City", "Code", unique: true);
            CreateIndex("dbo.Country", "Code", unique: true);
            CreateIndex("dbo.Country", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Country", new[] { "Name" });
            DropIndex("dbo.Country", new[] { "Code" });
            DropIndex("dbo.City", new[] { "Code" });
            AlterColumn("dbo.Organization", "Name", c => c.String(maxLength: 200));
            AlterColumn("dbo.Country", "Name", c => c.String(maxLength: 200));
            AlterColumn("dbo.Country", "Code", c => c.String(maxLength: 2, fixedLength: true, unicode: false));
        }
    }
}

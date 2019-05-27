namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountryCodeOptional : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Country", new[] { "Code" });
            AlterColumn("dbo.Country", "Code", c => c.String(maxLength: 2, fixedLength: true, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Country", "Code", c => c.String(nullable: false, maxLength: 2, fixedLength: true, unicode: false));
            CreateIndex("dbo.Country", "Code", unique: true);
        }
    }
}

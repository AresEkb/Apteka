namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Address300 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Address", "Description", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Address", "Description", c => c.String(maxLength: 200));
        }
    }
}

namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PricePrecision : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Medicine", "Inn", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Medicine", "Inn", c => c.String(maxLength: 200));
        }
    }
}

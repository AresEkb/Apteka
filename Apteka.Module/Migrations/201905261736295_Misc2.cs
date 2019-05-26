namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Misc2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Invoice", "ValueAddedTaxAmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoice", "ValueAddedTaxAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}

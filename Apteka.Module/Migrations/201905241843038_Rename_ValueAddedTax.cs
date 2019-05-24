namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rename_ValueAddedTax : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoice", "ValueAddedTaxAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Invoice", "ValueAddedTax");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoice", "ValueAddedTax", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Invoice", "ValueAddedTaxAmount");
        }
    }
}

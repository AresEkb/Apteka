namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rename_Invoice_Fields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoice", "ShipmentDateTime", c => c.DateTime());
            DropColumn("dbo.Invoice", "ShippingDateTime");
            DropColumn("dbo.Invoice", "ManufacturerPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoice", "ManufacturerPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Invoice", "ShippingDateTime", c => c.DateTime());
            DropColumn("dbo.Invoice", "ShipmentDateTime");
        }
    }
}

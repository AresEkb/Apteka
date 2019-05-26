namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixed_Rate_Types : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.City", "Code", c => c.String(maxLength: 10, unicode: false));
            AlterColumn("dbo.InvoiceItem", "SupplierMarkupRate", c => c.Decimal(nullable: false, precision: 5, scale: 4));
            AlterColumn("dbo.InvoiceItem", "ValueAddedTaxRate", c => c.Decimal(nullable: false, precision: 5, scale: 4));
            AlterColumn("dbo.InvoiceItem", "Ean13", c => c.String(maxLength: 13, fixedLength: true, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InvoiceItem", "Ean13", c => c.String(maxLength: 13));
            AlterColumn("dbo.InvoiceItem", "ValueAddedTaxRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.InvoiceItem", "SupplierMarkupRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.City", "Code", c => c.String(maxLength: 10, fixedLength: true, unicode: false));
        }
    }
}

namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Misc1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Invoice", name: "Provider_Id", newName: "Supplier_Id");
            RenameIndex(table: "dbo.Invoice", name: "IX_Provider_Id", newName: "IX_Supplier_Id");
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 2),
                        Name = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductSeries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 20),
                        CertificateCode = c.String(maxLength: 50),
                        CertificateAuthority = c.String(maxLength: 100),
                        CertificateIssueDate = c.DateTime(),
                        CertificateExpireDate = c.DateTime(),
                        ShelfLifeDate = c.DateTime(),
                        RegionalCertificateCode = c.String(maxLength: 50),
                        RegionalCertificateAuthority = c.String(maxLength: 100),
                        RegionalCertificateIssueDate = c.DateTime(),
                        InvoiceItem_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InvoiceItem", t => t.InvoiceItem_Id)
                .Index(t => t.InvoiceItem_Id);
            
            AddColumn("dbo.Invoice", "ManufacturerPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Invoice", "ValueAddedTax", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Invoice", "Note", c => c.String(maxLength: 1000));
            AddColumn("dbo.Organization", "TaxRegistrationReasonCode", c => c.String(maxLength: 20));
            AddColumn("dbo.Organization", "PhoneNumber", c => c.String(maxLength: 20));
            AddColumn("dbo.Organization", "Email", c => c.String(maxLength: 100));
            AddColumn("dbo.InvoiceItem", "ProductCode", c => c.String(maxLength: 100));
            AddColumn("dbo.InvoiceItem", "ProductName", c => c.String(maxLength: 100));
            AddColumn("dbo.InvoiceItem", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.InvoiceItem", "ManufacturerPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.InvoiceItem", "StateRegistryPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.InvoiceItem", "SupplierMarkupRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.InvoiceItem", "ValueAddedTaxRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.InvoiceItem", "Ean13", c => c.String(maxLength: 13));
            AddColumn("dbo.InvoiceItem", "CustomsDeclarationNumber", c => c.String(maxLength: 30));
            AddColumn("dbo.InvoiceItem", "Manufacturer_Id", c => c.Int());
            AddColumn("dbo.InvoiceItem", "ManufacturerCountry_Id", c => c.Int());
            AlterColumn("dbo.Invoice", "Code", c => c.String(maxLength: 15));
            CreateIndex("dbo.InvoiceItem", "Manufacturer_Id");
            CreateIndex("dbo.InvoiceItem", "ManufacturerCountry_Id");
            AddForeignKey("dbo.InvoiceItem", "Manufacturer_Id", "dbo.Organization", "Id");
            AddForeignKey("dbo.InvoiceItem", "ManufacturerCountry_Id", "dbo.Country", "Id");
            DropColumn("dbo.InvoiceItem", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InvoiceItem", "Code", c => c.String(maxLength: 100));
            DropForeignKey("dbo.ProductSeries", "InvoiceItem_Id", "dbo.InvoiceItem");
            DropForeignKey("dbo.InvoiceItem", "ManufacturerCountry_Id", "dbo.Country");
            DropForeignKey("dbo.InvoiceItem", "Manufacturer_Id", "dbo.Organization");
            DropIndex("dbo.ProductSeries", new[] { "InvoiceItem_Id" });
            DropIndex("dbo.InvoiceItem", new[] { "ManufacturerCountry_Id" });
            DropIndex("dbo.InvoiceItem", new[] { "Manufacturer_Id" });
            AlterColumn("dbo.Invoice", "Code", c => c.String(maxLength: 100));
            DropColumn("dbo.InvoiceItem", "ManufacturerCountry_Id");
            DropColumn("dbo.InvoiceItem", "Manufacturer_Id");
            DropColumn("dbo.InvoiceItem", "CustomsDeclarationNumber");
            DropColumn("dbo.InvoiceItem", "Ean13");
            DropColumn("dbo.InvoiceItem", "ValueAddedTaxRate");
            DropColumn("dbo.InvoiceItem", "SupplierMarkupRate");
            DropColumn("dbo.InvoiceItem", "StateRegistryPrice");
            DropColumn("dbo.InvoiceItem", "ManufacturerPrice");
            DropColumn("dbo.InvoiceItem", "Quantity");
            DropColumn("dbo.InvoiceItem", "ProductName");
            DropColumn("dbo.InvoiceItem", "ProductCode");
            DropColumn("dbo.Organization", "Email");
            DropColumn("dbo.Organization", "PhoneNumber");
            DropColumn("dbo.Organization", "TaxRegistrationReasonCode");
            DropColumn("dbo.Invoice", "Note");
            DropColumn("dbo.Invoice", "ValueAddedTax");
            DropColumn("dbo.Invoice", "ManufacturerPrice");
            DropTable("dbo.ProductSeries");
            DropTable("dbo.Country");
            RenameIndex(table: "dbo.Invoice", name: "IX_Supplier_Id", newName: "IX_Provider_Id");
            RenameColumn(table: "dbo.Invoice", name: "Supplier_Id", newName: "Provider_Id");
        }
    }
}

namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrgCountry : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InvoiceItem", "ManufacturerCountryId", "dbo.Country");
            DropForeignKey("dbo.MedicineDosageForm", "CertificateRecipientCountryId", "dbo.Country");
            DropIndex("dbo.InvoiceItem", "IX_ManufacturerCountry_Id");
            DropIndex("dbo.MedicineDosageForm", "IX_CertificateRecipientCountry_Id");
            AddColumn("dbo.Organization", "CountryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Organization", "CountryId", name: "IX_Country_Id");
            AddForeignKey("dbo.Organization", "CountryId", "dbo.Country", "Id", cascadeDelete: true);
            DropColumn("dbo.InvoiceItem", "ManufacturerCountryId");
            DropColumn("dbo.MedicineDosageForm", "CertificateRecipientCountryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MedicineDosageForm", "CertificateRecipientCountryId", c => c.Int());
            AddColumn("dbo.InvoiceItem", "ManufacturerCountryId", c => c.Int());
            DropForeignKey("dbo.Organization", "CountryId", "dbo.Country");
            DropIndex("dbo.Organization", "IX_Country_Id");
            DropColumn("dbo.Organization", "CountryId");
            CreateIndex("dbo.MedicineDosageForm", "CertificateRecipientCountryId", name: "IX_CertificateRecipientCountry_Id");
            CreateIndex("dbo.InvoiceItem", "ManufacturerCountryId", name: "IX_ManufacturerCountry_Id");
            AddForeignKey("dbo.MedicineDosageForm", "CertificateRecipientCountryId", "dbo.Country", "Id");
            AddForeignKey("dbo.InvoiceItem", "ManufacturerCountryId", "dbo.Country", "Id");
        }
    }
}

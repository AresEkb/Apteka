namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Medicine : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Medicine",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TradeName = c.String(maxLength: 200),
                        Inn = c.String(maxLength: 200),
                        AtcCodeId = c.Int(),
                        PharmacotherapeuticGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AtcGroup", t => t.AtcCodeId)
                .ForeignKey("dbo.PharmacotherapeuticGroup", t => t.PharmacotherapeuticGroupId)
                .Index(t => t.AtcCodeId, name: "IX_AtcCode_Id")
                .Index(t => t.PharmacotherapeuticGroupId, name: "IX_PharmacotherapeuticGroup_Id");
            
            CreateTable(
                "dbo.AtcGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 15),
                        Name = c.String(maxLength: 200),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AtcGroup", t => t.ParentId)
                .Index(t => t.ParentId, name: "IX_Parent_Id");
            
            CreateTable(
                "dbo.PharmacotherapeuticGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 15),
                        Name = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MedicineReleaseForm",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DosageForm = c.String(maxLength: 200),
                        Dosage = c.String(maxLength: 100),
                        Package = c.String(maxLength: 100),
                        UnitQuantity = c.Int(nullable: false),
                        RegistrationCertificateNumber = c.String(maxLength: 20),
                        RegistrationCertificateIssueDate = c.DateTime(nullable: false),
                        RegistrationCertificateExpiryDate = c.DateTime(),
                        RegistrationCertificateCancellationDate = c.DateTime(),
                        NormativeDocument = c.String(maxLength: 200),
                        Ean13 = c.String(maxLength: 13, fixedLength: true, unicode: false),
                        CertificateRecipientId = c.Int(),
                        CertificateRecipientCountryId = c.Int(),
                        ManufacturerId = c.Int(),
                        MedicineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organization", t => t.CertificateRecipientId)
                .ForeignKey("dbo.Country", t => t.CertificateRecipientCountryId)
                .ForeignKey("dbo.Organization", t => t.ManufacturerId)
                .ForeignKey("dbo.Medicine", t => t.MedicineId, cascadeDelete: true)
                .Index(t => t.CertificateRecipientId, name: "IX_CertificateRecipient_Id")
                .Index(t => t.CertificateRecipientCountryId, name: "IX_CertificateRecipientCountry_Id")
                .Index(t => t.ManufacturerId, name: "IX_Manufacturer_Id")
                .Index(t => t.MedicineId, name: "IX_Medicine_Id");
            
            CreateTable(
                "dbo.MedicinePriceLimit",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PriceLimit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrimaryPackagePrice = c.Boolean(nullable: false),
                        RegistrationDate = c.DateTime(nullable: false),
                        RegistrationDocNumber = c.String(maxLength: 100),
                        ReleaseFormId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MedicineReleaseForm", t => t.ReleaseFormId, cascadeDelete: true)
                .Index(t => t.ReleaseFormId, name: "IX_ReleaseForm_Id");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MedicinePriceLimit", "ReleaseFormId", "dbo.MedicineReleaseForm");
            DropForeignKey("dbo.MedicineReleaseForm", "MedicineId", "dbo.Medicine");
            DropForeignKey("dbo.MedicineReleaseForm", "ManufacturerId", "dbo.Organization");
            DropForeignKey("dbo.MedicineReleaseForm", "CertificateRecipientCountryId", "dbo.Country");
            DropForeignKey("dbo.MedicineReleaseForm", "CertificateRecipientId", "dbo.Organization");
            DropForeignKey("dbo.Medicine", "PharmacotherapeuticGroupId", "dbo.PharmacotherapeuticGroup");
            DropForeignKey("dbo.Medicine", "AtcCodeId", "dbo.AtcGroup");
            DropForeignKey("dbo.AtcGroup", "ParentId", "dbo.AtcGroup");
            DropIndex("dbo.MedicinePriceLimit", "IX_ReleaseForm_Id");
            DropIndex("dbo.MedicineReleaseForm", "IX_Medicine_Id");
            DropIndex("dbo.MedicineReleaseForm", "IX_Manufacturer_Id");
            DropIndex("dbo.MedicineReleaseForm", "IX_CertificateRecipientCountry_Id");
            DropIndex("dbo.MedicineReleaseForm", "IX_CertificateRecipient_Id");
            DropIndex("dbo.AtcGroup", "IX_Parent_Id");
            DropIndex("dbo.Medicine", "IX_PharmacotherapeuticGroup_Id");
            DropIndex("dbo.Medicine", "IX_AtcCode_Id");
            DropTable("dbo.MedicinePriceLimit");
            DropTable("dbo.MedicineReleaseForm");
            DropTable("dbo.PharmacotherapeuticGroup");
            DropTable("dbo.AtcGroup");
            DropTable("dbo.Medicine");
        }
    }
}

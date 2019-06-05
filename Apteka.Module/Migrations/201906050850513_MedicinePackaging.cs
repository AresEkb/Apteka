namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MedicinePackaging : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MedicinePriceLimit", "ReleaseFormId", "dbo.MedicineReleaseForm");
            DropForeignKey("dbo.MedicineReleaseForm", "CertificateRecipientId", "dbo.Organization");
            DropForeignKey("dbo.MedicineReleaseForm", "CertificateRecipientCountryId", "dbo.Country");
            DropForeignKey("dbo.MedicineReleaseForm", "ManufacturerId", "dbo.Organization");
            DropForeignKey("dbo.MedicineReleaseForm", "MedicineId", "dbo.Medicine");
            DropIndex("dbo.MedicineReleaseForm", "IX_CertificateRecipient_Id");
            DropIndex("dbo.MedicineReleaseForm", "IX_CertificateRecipientCountry_Id");
            DropIndex("dbo.MedicineReleaseForm", "IX_Manufacturer_Id");
            DropIndex("dbo.MedicineReleaseForm", "IX_Medicine_Id");
            CreateTable(
                "dbo.MedicineDosageForm",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DosageMeasure = c.Decimal(precision: 18, scale: 6),
                        DosageFormMeasure = c.Decimal(precision: 18, scale: 6),
                        PrimaryPackagingCount = c.Int(),
                        SecondaryPackagingCount = c.Int(),
                        PrimaryPackaging2Count = c.Int(),
                        TotalCount = c.Int(),
                        RegistrationCertificateNumber = c.String(maxLength: 20),
                        RegistrationCertificateIssueDate = c.DateTime(nullable: false),
                        RegistrationCertificateExpiryDate = c.DateTime(),
                        RegistrationCertificateCancellationDate = c.DateTime(),
                        NormativeDocument = c.String(maxLength: 200),
                        Ean13 = c.String(maxLength: 13, fixedLength: true, unicode: false),
                        CertificateRecipientId = c.Int(),
                        CertificateRecipientCountryId = c.Int(),
                        DosageFormId = c.Int(),
                        DosageFormMeasurementUnitId = c.Int(),
                        DosageMeasurementUnitId = c.Int(),
                        ManufacturerId = c.Int(),
                        MedicineId = c.Int(nullable: false),
                        PrimaryPackagingId = c.Int(),
                        SecondaryPackagingId = c.Int(),
                        SecondaryPackaging2Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organization", t => t.CertificateRecipientId)
                .ForeignKey("dbo.Country", t => t.CertificateRecipientCountryId)
                .ForeignKey("dbo.DosageForm", t => t.DosageFormId)
                .ForeignKey("dbo.MeasurementUnit", t => t.DosageFormMeasurementUnitId)
                .ForeignKey("dbo.MeasurementUnit", t => t.DosageMeasurementUnitId)
                .ForeignKey("dbo.Organization", t => t.ManufacturerId)
                .ForeignKey("dbo.Medicine", t => t.MedicineId, cascadeDelete: true)
                .ForeignKey("dbo.PrimaryPackaging", t => t.PrimaryPackagingId)
                .ForeignKey("dbo.SecondaryPackaging", t => t.SecondaryPackagingId)
                .ForeignKey("dbo.SecondaryPackaging", t => t.SecondaryPackaging2Id)
                .Index(t => t.CertificateRecipientId, name: "IX_CertificateRecipient_Id")
                .Index(t => t.CertificateRecipientCountryId, name: "IX_CertificateRecipientCountry_Id")
                .Index(t => t.DosageFormId, name: "IX_DosageForm_Id")
                .Index(t => t.DosageFormMeasurementUnitId, name: "IX_DosageFormMeasurementUnit_Id")
                .Index(t => t.DosageMeasurementUnitId, name: "IX_DosageMeasurementUnit_Id")
                .Index(t => t.ManufacturerId, name: "IX_Manufacturer_Id")
                .Index(t => t.MedicineId, name: "IX_Medicine_Id")
                .Index(t => t.PrimaryPackagingId, name: "IX_PrimaryPackaging_Id")
                .Index(t => t.SecondaryPackagingId, name: "IX_SecondaryPackaging_Id")
                .Index(t => t.SecondaryPackaging2Id, name: "IX_SecondaryPackaging2_Id");
            
            CreateTable(
                "dbo.DosageForm",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MeasurementUnit",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PrimaryPackaging",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SecondaryPackaging",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.MedicineReleaseForm");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.MedicineDosageForm", "SecondaryPackaging2Id", "dbo.SecondaryPackaging");
            DropForeignKey("dbo.MedicineDosageForm", "SecondaryPackagingId", "dbo.SecondaryPackaging");
            DropForeignKey("dbo.MedicineDosageForm", "PrimaryPackagingId", "dbo.PrimaryPackaging");
            DropForeignKey("dbo.MedicineDosageForm", "MedicineId", "dbo.Medicine");
            DropForeignKey("dbo.MedicineDosageForm", "ManufacturerId", "dbo.Organization");
            DropForeignKey("dbo.MedicineDosageForm", "DosageMeasurementUnitId", "dbo.MeasurementUnit");
            DropForeignKey("dbo.MedicineDosageForm", "DosageFormMeasurementUnitId", "dbo.MeasurementUnit");
            DropForeignKey("dbo.MedicineDosageForm", "DosageFormId", "dbo.DosageForm");
            DropForeignKey("dbo.MedicineDosageForm", "CertificateRecipientCountryId", "dbo.Country");
            DropForeignKey("dbo.MedicineDosageForm", "CertificateRecipientId", "dbo.Organization");
            DropIndex("dbo.MedicineDosageForm", "IX_SecondaryPackaging2_Id");
            DropIndex("dbo.MedicineDosageForm", "IX_SecondaryPackaging_Id");
            DropIndex("dbo.MedicineDosageForm", "IX_PrimaryPackaging_Id");
            DropIndex("dbo.MedicineDosageForm", "IX_Medicine_Id");
            DropIndex("dbo.MedicineDosageForm", "IX_Manufacturer_Id");
            DropIndex("dbo.MedicineDosageForm", "IX_DosageMeasurementUnit_Id");
            DropIndex("dbo.MedicineDosageForm", "IX_DosageFormMeasurementUnit_Id");
            DropIndex("dbo.MedicineDosageForm", "IX_DosageForm_Id");
            DropIndex("dbo.MedicineDosageForm", "IX_CertificateRecipientCountry_Id");
            DropIndex("dbo.MedicineDosageForm", "IX_CertificateRecipient_Id");
            DropTable("dbo.SecondaryPackaging");
            DropTable("dbo.PrimaryPackaging");
            DropTable("dbo.MeasurementUnit");
            DropTable("dbo.DosageForm");
            DropTable("dbo.MedicineDosageForm");
            CreateIndex("dbo.MedicineReleaseForm", "MedicineId", name: "IX_Medicine_Id");
            CreateIndex("dbo.MedicineReleaseForm", "ManufacturerId", name: "IX_Manufacturer_Id");
            CreateIndex("dbo.MedicineReleaseForm", "CertificateRecipientCountryId", name: "IX_CertificateRecipientCountry_Id");
            CreateIndex("dbo.MedicineReleaseForm", "CertificateRecipientId", name: "IX_CertificateRecipient_Id");
            AddForeignKey("dbo.MedicineReleaseForm", "MedicineId", "dbo.Medicine", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MedicineReleaseForm", "ManufacturerId", "dbo.Organization", "Id");
            AddForeignKey("dbo.MedicineReleaseForm", "CertificateRecipientCountryId", "dbo.Country", "Id");
            AddForeignKey("dbo.MedicineReleaseForm", "CertificateRecipientId", "dbo.Organization", "Id");
            AddForeignKey("dbo.MedicinePriceLimit", "ReleaseFormId", "dbo.MedicineReleaseForm", "Id");
        }
    }
}

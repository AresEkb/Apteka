namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RegCertificateRename : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Organization", "CountryId", "dbo.Country");
            DropIndex("dbo.Organization", "IX_Country_Id");
            RenameColumn(table: "dbo.MedicineDosageForm", name: "CertificateRecipientId", newName: "RegCertificateOwnerId");
            RenameIndex(table: "dbo.MedicineDosageForm", name: "IX_CertificateRecipient_Id", newName: "IX_RegCertificateOwner_Id");
            AddColumn("dbo.MedicineDosageForm", "RegCertificateNumber", c => c.String(maxLength: 20));
            AddColumn("dbo.MedicineDosageForm", "RegCertificateIssueDate", c => c.DateTime(storeType: "date"));
            AddColumn("dbo.MedicineDosageForm", "RegCertificateExpiryDate", c => c.DateTime(storeType: "date"));
            AddColumn("dbo.MedicineDosageForm", "RegCertificateCancellationDate", c => c.DateTime(storeType: "date"));
            DropColumn("dbo.Organization", "CountryId");
            DropColumn("dbo.MedicineDosageForm", "RegistrationCertificateNumber");
            DropColumn("dbo.MedicineDosageForm", "RegistrationCertificateIssueDate");
            DropColumn("dbo.MedicineDosageForm", "RegistrationCertificateExpiryDate");
            DropColumn("dbo.MedicineDosageForm", "RegistrationCertificateCancellationDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MedicineDosageForm", "RegistrationCertificateCancellationDate", c => c.DateTime(storeType: "date"));
            AddColumn("dbo.MedicineDosageForm", "RegistrationCertificateExpiryDate", c => c.DateTime(storeType: "date"));
            AddColumn("dbo.MedicineDosageForm", "RegistrationCertificateIssueDate", c => c.DateTime(nullable: false, storeType: "date"));
            AddColumn("dbo.MedicineDosageForm", "RegistrationCertificateNumber", c => c.String(maxLength: 20));
            AddColumn("dbo.Organization", "CountryId", c => c.Int(nullable: false));
            DropColumn("dbo.MedicineDosageForm", "RegCertificateCancellationDate");
            DropColumn("dbo.MedicineDosageForm", "RegCertificateExpiryDate");
            DropColumn("dbo.MedicineDosageForm", "RegCertificateIssueDate");
            DropColumn("dbo.MedicineDosageForm", "RegCertificateNumber");
            RenameIndex(table: "dbo.MedicineDosageForm", name: "IX_RegCertificateOwner_Id", newName: "IX_CertificateRecipient_Id");
            RenameColumn(table: "dbo.MedicineDosageForm", name: "RegCertificateOwnerId", newName: "CertificateRecipientId");
            CreateIndex("dbo.Organization", "CountryId", name: "IX_Country_Id");
            AddForeignKey("dbo.Organization", "CountryId", "dbo.Country", "Id", cascadeDelete: true);
        }
    }
}

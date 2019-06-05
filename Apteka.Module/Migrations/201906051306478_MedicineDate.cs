namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MedicineDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MedicineDosageForm", "RegistrationCertificateIssueDate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.MedicineDosageForm", "RegistrationCertificateExpiryDate", c => c.DateTime(storeType: "date"));
            AlterColumn("dbo.MedicineDosageForm", "RegistrationCertificateCancellationDate", c => c.DateTime(storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MedicineDosageForm", "RegistrationCertificateCancellationDate", c => c.DateTime());
            AlterColumn("dbo.MedicineDosageForm", "RegistrationCertificateExpiryDate", c => c.DateTime());
            AlterColumn("dbo.MedicineDosageForm", "RegistrationCertificateIssueDate", c => c.DateTime(nullable: false));
        }
    }
}

namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MedicinePackaging2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.MedicinePriceLimit", name: "ReleaseFormId", newName: "DosageFormId");
            RenameIndex(table: "dbo.MedicinePriceLimit", name: "IX_ReleaseForm_Id", newName: "IX_DosageForm_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.MedicinePriceLimit", name: "IX_DosageForm_Id", newName: "IX_ReleaseForm_Id");
            RenameColumn(table: "dbo.MedicinePriceLimit", name: "DosageFormId", newName: "ReleaseFormId");
        }
    }
}

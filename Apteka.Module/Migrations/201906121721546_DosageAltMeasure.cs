namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DosageAltMeasure : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MedicineDosageForm", "AltDosageFormMeasure", c => c.Decimal(precision: 18, scale: 6));
            AddColumn("dbo.MedicineDosageForm", "AltDosageFormMeasurementUnitId", c => c.Int());
            CreateIndex("dbo.MedicineDosageForm", "AltDosageFormMeasurementUnitId", name: "IX_AltDosageFormMeasurementUnit_Id");
            AddForeignKey("dbo.MedicineDosageForm", "AltDosageFormMeasurementUnitId", "dbo.MeasurementUnit", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MedicineDosageForm", "AltDosageFormMeasurementUnitId", "dbo.MeasurementUnit");
            DropIndex("dbo.MedicineDosageForm", "IX_AltDosageFormMeasurementUnit_Id");
            DropColumn("dbo.MedicineDosageForm", "AltDosageFormMeasurementUnitId");
            DropColumn("dbo.MedicineDosageForm", "AltDosageFormMeasure");
        }
    }
}

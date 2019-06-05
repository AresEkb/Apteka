namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MedicineUniqueIndex : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Medicine", "TradeName", unique: true);
            CreateIndex("dbo.MedicineDosageForm", "Ean13", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.MedicineDosageForm", new[] { "Ean13" });
            DropIndex("dbo.Medicine", new[] { "TradeName" });
        }
    }
}

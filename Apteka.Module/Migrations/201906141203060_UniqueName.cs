namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueName : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.City", new[] { "Code" });
            DropIndex("dbo.Medicine", new[] { "TradeName" });
            AlterColumn("dbo.City", "Name", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Medicine", "TradeName", c => c.String(nullable: false, maxLength: 300));
            AlterColumn("dbo.AtcGroup", "Name", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.MeasurementUnit", "Name", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.DosageForm", "Name", c => c.String(nullable: false, maxLength: 300));
            AlterColumn("dbo.IntermediatePackaging", "Name", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.OrganizationRole", "Name", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.MedicinePriceLimitExclusionReason", "Name", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.PrimaryPackaging", "Name", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.SecondaryPackaging", "Name", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.PharmacotherapeuticGroup", "Name", c => c.String(nullable: false, maxLength: 200));
            CreateIndex("dbo.City", "Name", unique: true);
            CreateIndex("dbo.Medicine", "TradeName", unique: true);
            CreateIndex("dbo.AtcGroup", "Name", unique: true);
            CreateIndex("dbo.MeasurementUnit", "Name", unique: true);
            CreateIndex("dbo.DosageForm", "Name", unique: true);
            CreateIndex("dbo.IntermediatePackaging", "Name", unique: true);
            CreateIndex("dbo.OrganizationRole", "Name", unique: true);
            CreateIndex("dbo.MedicinePriceLimitExclusionReason", "Name", unique: true);
            CreateIndex("dbo.PrimaryPackaging", "Name", unique: true);
            CreateIndex("dbo.SecondaryPackaging", "Name", unique: true);
            CreateIndex("dbo.PharmacotherapeuticGroup", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.PharmacotherapeuticGroup", new[] { "Name" });
            DropIndex("dbo.SecondaryPackaging", new[] { "Name" });
            DropIndex("dbo.PrimaryPackaging", new[] { "Name" });
            DropIndex("dbo.MedicinePriceLimitExclusionReason", new[] { "Name" });
            DropIndex("dbo.OrganizationRole", new[] { "Name" });
            DropIndex("dbo.IntermediatePackaging", new[] { "Name" });
            DropIndex("dbo.DosageForm", new[] { "Name" });
            DropIndex("dbo.MeasurementUnit", new[] { "Name" });
            DropIndex("dbo.AtcGroup", new[] { "Name" });
            DropIndex("dbo.Medicine", new[] { "TradeName" });
            DropIndex("dbo.City", new[] { "Name" });
            AlterColumn("dbo.PharmacotherapeuticGroup", "Name", c => c.String(maxLength: 200));
            AlterColumn("dbo.SecondaryPackaging", "Name", c => c.String(maxLength: 200));
            AlterColumn("dbo.PrimaryPackaging", "Name", c => c.String(maxLength: 200));
            AlterColumn("dbo.MedicinePriceLimitExclusionReason", "Name", c => c.String(maxLength: 200));
            AlterColumn("dbo.OrganizationRole", "Name", c => c.String(maxLength: 200));
            AlterColumn("dbo.IntermediatePackaging", "Name", c => c.String(maxLength: 200));
            AlterColumn("dbo.DosageForm", "Name", c => c.String(maxLength: 300));
            AlterColumn("dbo.MeasurementUnit", "Name", c => c.String(maxLength: 200));
            AlterColumn("dbo.AtcGroup", "Name", c => c.String(maxLength: 200));
            AlterColumn("dbo.Medicine", "TradeName", c => c.String(maxLength: 300));
            AlterColumn("dbo.City", "Name", c => c.String(maxLength: 200));
            CreateIndex("dbo.Medicine", "TradeName", unique: true);
            CreateIndex("dbo.City", "Code", unique: true);
        }
    }
}

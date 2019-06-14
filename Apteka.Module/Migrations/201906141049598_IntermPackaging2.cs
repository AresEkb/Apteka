namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntermPackaging2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MedicineDosageForm", "IntermediatePackaging2Count", c => c.Int());
            AddColumn("dbo.MedicineDosageForm", "IntermediatePackaging2Note", c => c.String());
            AddColumn("dbo.MedicineDosageForm", "IntermediatePackaging2Id", c => c.Int());
            CreateIndex("dbo.MedicineDosageForm", "IntermediatePackaging2Id", name: "IX_IntermediatePackaging2_Id");
            AddForeignKey("dbo.MedicineDosageForm", "IntermediatePackaging2Id", "dbo.IntermediatePackaging", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MedicineDosageForm", "IntermediatePackaging2Id", "dbo.IntermediatePackaging");
            DropIndex("dbo.MedicineDosageForm", "IX_IntermediatePackaging2_Id");
            DropColumn("dbo.MedicineDosageForm", "IntermediatePackaging2Id");
            DropColumn("dbo.MedicineDosageForm", "IntermediatePackaging2Note");
            DropColumn("dbo.MedicineDosageForm", "IntermediatePackaging2Count");
        }
    }
}

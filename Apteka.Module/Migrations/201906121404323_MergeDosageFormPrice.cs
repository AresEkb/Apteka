namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MergeDosageFormPrice : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MedicinePriceLimit", "DosageFormId", "dbo.MedicineDosageForm");
            DropForeignKey("dbo.MedicinePriceLimit", "ExclusionReasonId", "dbo.MedicinePriceLimitExclusionReason");
            DropForeignKey("dbo.MedicineDosageForm", "SecondaryPackaging2Id", "dbo.SecondaryPackaging");
            DropIndex("dbo.MedicineDosageForm", "IX_SecondaryPackaging2_Id");
            DropIndex("dbo.MedicinePriceLimit", "IX_DosageForm_Id");
            DropIndex("dbo.MedicinePriceLimit", "IX_ExclusionReason_Id");
            CreateTable(
                "dbo.IntermediatePackaging",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.MedicineDosageForm", "IntermediatePackagingCount", c => c.Int());
            AddColumn("dbo.MedicineDosageForm", "PriceLimit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MedicineDosageForm", "IsPrimaryPackagingPrice", c => c.Boolean(nullable: false));
            AddColumn("dbo.MedicineDosageForm", "PriceRegistrationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.MedicineDosageForm", "PriceRegistrationDocNumber", c => c.String(maxLength: 100));
            AddColumn("dbo.MedicineDosageForm", "PriceExclusionDate", c => c.DateTime());
            AddColumn("dbo.MedicineDosageForm", "IntermediatePackagingId", c => c.Int());
            AddColumn("dbo.MedicineDosageForm", "PriceExclusionReasonId", c => c.Int());
            CreateIndex("dbo.MedicineDosageForm", "IntermediatePackagingId", name: "IX_IntermediatePackaging_Id");
            CreateIndex("dbo.MedicineDosageForm", "PriceExclusionReasonId", name: "IX_PriceExclusionReason_Id");
            AddForeignKey("dbo.MedicineDosageForm", "IntermediatePackagingId", "dbo.IntermediatePackaging", "Id");
            AddForeignKey("dbo.MedicineDosageForm", "PriceExclusionReasonId", "dbo.MedicinePriceLimitExclusionReason", "Id");
            DropColumn("dbo.MedicineDosageForm", "PrimaryPackaging2Count");
            DropColumn("dbo.MedicineDosageForm", "SecondaryPackaging2Id");
            DropTable("dbo.MedicinePriceLimit");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MedicinePriceLimit",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PriceLimit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrimaryPackagePrice = c.Boolean(nullable: false),
                        RegistrationDate = c.DateTime(nullable: false),
                        RegistrationDocNumber = c.String(maxLength: 100),
                        ExclusionDate = c.DateTime(),
                        DosageFormId = c.Int(nullable: false),
                        ExclusionReasonId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.MedicineDosageForm", "SecondaryPackaging2Id", c => c.Int());
            AddColumn("dbo.MedicineDosageForm", "PrimaryPackaging2Count", c => c.Int());
            DropForeignKey("dbo.MedicineDosageForm", "PriceExclusionReasonId", "dbo.MedicinePriceLimitExclusionReason");
            DropForeignKey("dbo.MedicineDosageForm", "IntermediatePackagingId", "dbo.IntermediatePackaging");
            DropIndex("dbo.MedicineDosageForm", "IX_PriceExclusionReason_Id");
            DropIndex("dbo.MedicineDosageForm", "IX_IntermediatePackaging_Id");
            DropColumn("dbo.MedicineDosageForm", "PriceExclusionReasonId");
            DropColumn("dbo.MedicineDosageForm", "IntermediatePackagingId");
            DropColumn("dbo.MedicineDosageForm", "PriceExclusionDate");
            DropColumn("dbo.MedicineDosageForm", "PriceRegistrationDocNumber");
            DropColumn("dbo.MedicineDosageForm", "PriceRegistrationDate");
            DropColumn("dbo.MedicineDosageForm", "IsPrimaryPackagingPrice");
            DropColumn("dbo.MedicineDosageForm", "PriceLimit");
            DropColumn("dbo.MedicineDosageForm", "IntermediatePackagingCount");
            DropTable("dbo.IntermediatePackaging");
            CreateIndex("dbo.MedicinePriceLimit", "ExclusionReasonId", name: "IX_ExclusionReason_Id");
            CreateIndex("dbo.MedicinePriceLimit", "DosageFormId", name: "IX_DosageForm_Id");
            CreateIndex("dbo.MedicineDosageForm", "SecondaryPackaging2Id", name: "IX_SecondaryPackaging2_Id");
            AddForeignKey("dbo.MedicineDosageForm", "SecondaryPackaging2Id", "dbo.SecondaryPackaging", "Id");
            AddForeignKey("dbo.MedicinePriceLimit", "ExclusionReasonId", "dbo.MedicinePriceLimitExclusionReason", "Id");
            AddForeignKey("dbo.MedicinePriceLimit", "DosageFormId", "dbo.MedicineDosageForm", "Id", cascadeDelete: true);
        }
    }
}

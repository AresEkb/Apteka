namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorPackaging : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MedicineDosageForm", "IntermediatePackagingId", "dbo.IntermediatePackaging");
            DropForeignKey("dbo.MedicineDosageForm", "IntermediatePackaging2Id", "dbo.IntermediatePackaging");
            DropForeignKey("dbo.MedicineDosageForm", "PrimaryPackagingId", "dbo.PrimaryPackaging");
            DropForeignKey("dbo.MedicineDosageForm", "SecondaryPackagingId", "dbo.SecondaryPackaging");
            DropIndex("dbo.MedicineDosageForm", "IX_IntermediatePackaging_Id");
            DropIndex("dbo.MedicineDosageForm", "IX_IntermediatePackaging2_Id");
            DropIndex("dbo.MedicineDosageForm", "IX_PrimaryPackaging_Id");
            DropIndex("dbo.MedicineDosageForm", "IX_SecondaryPackaging_Id");
            DropIndex("dbo.IntermediatePackaging", new[] { "Name" });
            DropIndex("dbo.PrimaryPackaging", new[] { "Name" });
            DropIndex("dbo.SecondaryPackaging", new[] { "Name" });
            CreateTable(
                "dbo.MedicineDosageFormPackaging",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Order = c.Byte(nullable: false),
                        Measure = c.Decimal(precision: 18, scale: 6),
                        Count = c.Int(),
                        Note = c.String(),
                        KindId = c.Int(),
                        MeasurementUnitId = c.Int(),
                        MedicineDosageFormId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PackagingKind", t => t.KindId)
                .ForeignKey("dbo.MeasurementUnit", t => t.MeasurementUnitId)
                .ForeignKey("dbo.MedicineDosageForm", t => t.MedicineDosageFormId, cascadeDelete: true)
                .Index(t => t.KindId, name: "IX_Kind_Id")
                .Index(t => t.MeasurementUnitId, name: "IX_MeasurementUnit_Id")
                .Index(t => t.MedicineDosageFormId, name: "IX_MedicineDosageForm_Id");
            
            CreateTable(
                "dbo.PackagingKind",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        IsPrimary = c.Boolean(nullable: false),
                        IsIntermediate = c.Boolean(nullable: false),
                        IsSecondary = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            DropColumn("dbo.MedicineDosageForm", "PrimaryPackagingCount");
            DropColumn("dbo.MedicineDosageForm", "PrimaryPackagingNote");
            DropColumn("dbo.MedicineDosageForm", "IntermediatePackagingCount");
            DropColumn("dbo.MedicineDosageForm", "IntermediatePackagingNote");
            DropColumn("dbo.MedicineDosageForm", "IntermediatePackaging2Count");
            DropColumn("dbo.MedicineDosageForm", "IntermediatePackaging2Note");
            DropColumn("dbo.MedicineDosageForm", "SecondaryPackagingCount");
            DropColumn("dbo.MedicineDosageForm", "SecondaryPackagingNote");
            DropColumn("dbo.MedicineDosageForm", "IntermediatePackagingId");
            DropColumn("dbo.MedicineDosageForm", "IntermediatePackaging2Id");
            DropColumn("dbo.MedicineDosageForm", "PrimaryPackagingId");
            DropColumn("dbo.MedicineDosageForm", "SecondaryPackagingId");
            DropTable("dbo.IntermediatePackaging");
            DropTable("dbo.PrimaryPackaging");
            DropTable("dbo.SecondaryPackaging");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SecondaryPackaging",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PrimaryPackaging",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IntermediatePackaging",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.MedicineDosageForm", "SecondaryPackagingId", c => c.Int());
            AddColumn("dbo.MedicineDosageForm", "PrimaryPackagingId", c => c.Int());
            AddColumn("dbo.MedicineDosageForm", "IntermediatePackaging2Id", c => c.Int());
            AddColumn("dbo.MedicineDosageForm", "IntermediatePackagingId", c => c.Int());
            AddColumn("dbo.MedicineDosageForm", "SecondaryPackagingNote", c => c.String());
            AddColumn("dbo.MedicineDosageForm", "SecondaryPackagingCount", c => c.Int());
            AddColumn("dbo.MedicineDosageForm", "IntermediatePackaging2Note", c => c.String());
            AddColumn("dbo.MedicineDosageForm", "IntermediatePackaging2Count", c => c.Int());
            AddColumn("dbo.MedicineDosageForm", "IntermediatePackagingNote", c => c.String());
            AddColumn("dbo.MedicineDosageForm", "IntermediatePackagingCount", c => c.Int());
            AddColumn("dbo.MedicineDosageForm", "PrimaryPackagingNote", c => c.String());
            AddColumn("dbo.MedicineDosageForm", "PrimaryPackagingCount", c => c.Int());
            DropForeignKey("dbo.MedicineDosageFormPackaging", "MedicineDosageFormId", "dbo.MedicineDosageForm");
            DropForeignKey("dbo.MedicineDosageFormPackaging", "MeasurementUnitId", "dbo.MeasurementUnit");
            DropForeignKey("dbo.MedicineDosageFormPackaging", "KindId", "dbo.PackagingKind");
            DropIndex("dbo.PackagingKind", new[] { "Name" });
            DropIndex("dbo.MedicineDosageFormPackaging", "IX_MedicineDosageForm_Id");
            DropIndex("dbo.MedicineDosageFormPackaging", "IX_MeasurementUnit_Id");
            DropIndex("dbo.MedicineDosageFormPackaging", "IX_Kind_Id");
            DropTable("dbo.PackagingKind");
            DropTable("dbo.MedicineDosageFormPackaging");
            CreateIndex("dbo.SecondaryPackaging", "Name", unique: true);
            CreateIndex("dbo.PrimaryPackaging", "Name", unique: true);
            CreateIndex("dbo.IntermediatePackaging", "Name", unique: true);
            CreateIndex("dbo.MedicineDosageForm", "SecondaryPackagingId", name: "IX_SecondaryPackaging_Id");
            CreateIndex("dbo.MedicineDosageForm", "PrimaryPackagingId", name: "IX_PrimaryPackaging_Id");
            CreateIndex("dbo.MedicineDosageForm", "IntermediatePackaging2Id", name: "IX_IntermediatePackaging2_Id");
            CreateIndex("dbo.MedicineDosageForm", "IntermediatePackagingId", name: "IX_IntermediatePackaging_Id");
            AddForeignKey("dbo.MedicineDosageForm", "SecondaryPackagingId", "dbo.SecondaryPackaging", "Id");
            AddForeignKey("dbo.MedicineDosageForm", "PrimaryPackagingId", "dbo.PrimaryPackaging", "Id");
            AddForeignKey("dbo.MedicineDosageForm", "IntermediatePackaging2Id", "dbo.IntermediatePackaging", "Id");
            AddForeignKey("dbo.MedicineDosageForm", "IntermediatePackagingId", "dbo.IntermediatePackaging", "Id");
        }
    }
}

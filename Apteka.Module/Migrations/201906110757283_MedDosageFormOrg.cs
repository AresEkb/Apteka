namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MedDosageFormOrg : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MedicineDosageForm", "ManufacturerId", "dbo.Organization");
            DropIndex("dbo.MedicineDosageForm", "IX_Manufacturer_Id");
            CreateTable(
                "dbo.MedicineDosageFormOrganization",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MedicineDosageFormId = c.Int(nullable: false),
                        OrganizationId = c.Int(),
                        RoleId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MedicineDosageForm", t => t.MedicineDosageFormId, cascadeDelete: true)
                .ForeignKey("dbo.Organization", t => t.OrganizationId)
                .ForeignKey("dbo.OrganizationRole", t => t.RoleId)
                .Index(t => t.MedicineDosageFormId, name: "IX_MedicineDosageForm_Id")
                .Index(t => t.OrganizationId, name: "IX_Organization_Id")
                .Index(t => t.RoleId, name: "IX_Role_Id");
            
            CreateTable(
                "dbo.OrganizationRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.MedicineDosageForm", "ManufacturerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MedicineDosageForm", "ManufacturerId", c => c.Int());
            DropForeignKey("dbo.MedicineDosageFormOrganization", "RoleId", "dbo.OrganizationRole");
            DropForeignKey("dbo.MedicineDosageFormOrganization", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.MedicineDosageFormOrganization", "MedicineDosageFormId", "dbo.MedicineDosageForm");
            DropIndex("dbo.MedicineDosageFormOrganization", "IX_Role_Id");
            DropIndex("dbo.MedicineDosageFormOrganization", "IX_Organization_Id");
            DropIndex("dbo.MedicineDosageFormOrganization", "IX_MedicineDosageForm_Id");
            DropTable("dbo.OrganizationRole");
            DropTable("dbo.MedicineDosageFormOrganization");
            CreateIndex("dbo.MedicineDosageForm", "ManufacturerId", name: "IX_Manufacturer_Id");
            AddForeignKey("dbo.MedicineDosageForm", "ManufacturerId", "dbo.Organization", "Id");
        }
    }
}

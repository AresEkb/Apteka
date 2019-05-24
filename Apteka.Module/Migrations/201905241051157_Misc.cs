namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Misc : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Analyses", newName: "Analysis");
            RenameTable(name: "dbo.DashboardDatas", newName: "DashboardData");
            RenameTable(name: "dbo.ModelDifferenceAspects", newName: "ModelDifferenceAspect");
            RenameTable(name: "dbo.ModelDifferences", newName: "ModelDifference");
            RenameTable(name: "dbo.ModuleInfoes", newName: "ModuleInfo");
            RenameTable(name: "dbo.PermissionPolicyRoleBases", newName: "PermissionPolicyRoleBase");
            RenameTable(name: "dbo.PermissionPolicyNavigationPermissionObjects", newName: "PermissionPolicyNavigationPermissionObject");
            RenameTable(name: "dbo.PermissionPolicyTypePermissionObjects", newName: "PermissionPolicyTypePermissionObject");
            RenameTable(name: "dbo.PermissionPolicyMemberPermissionsObjects", newName: "PermissionPolicyMemberPermissionsObject");
            RenameTable(name: "dbo.PermissionPolicyObjectPermissionsObjects", newName: "PermissionPolicyObjectPermissionsObject");
            RenameTable(name: "dbo.PermissionPolicyUsers", newName: "PermissionPolicyUser");
            RenameTable(name: "dbo.PermissionPolicyUserPermissionPolicyRoles", newName: "PermissionPolicyUserPermissionPolicyRole");
            CreateTable(
                "dbo.Invoice",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 100),
                        DocDateTime = c.DateTime(),
                        ShippingDateTime = c.DateTime(),
                        PaymentConditions = c.String(),
                        ProductGroup = c.String(),
                        Consignee_Id = c.Int(),
                        Provider_Id = c.Int(),
                        Receiver_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organization", t => t.Consignee_Id)
                .ForeignKey("dbo.Organization", t => t.Provider_Id)
                .ForeignKey("dbo.Organization", t => t.Receiver_Id)
                .Index(t => t.Consignee_Id)
                .Index(t => t.Provider_Id)
                .Index(t => t.Receiver_Id);
            
            CreateTable(
                "dbo.Organization",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(maxLength: 200),
                        TaxpayerCode = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InvoiceItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 100),
                        Invoice_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoice", t => t.Invoice_Id)
                .Index(t => t.Invoice_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoice", "Receiver_Id", "dbo.Organization");
            DropForeignKey("dbo.Invoice", "Provider_Id", "dbo.Organization");
            DropForeignKey("dbo.InvoiceItem", "Invoice_Id", "dbo.Invoice");
            DropForeignKey("dbo.Invoice", "Consignee_Id", "dbo.Organization");
            DropIndex("dbo.InvoiceItem", new[] { "Invoice_Id" });
            DropIndex("dbo.Invoice", new[] { "Receiver_Id" });
            DropIndex("dbo.Invoice", new[] { "Provider_Id" });
            DropIndex("dbo.Invoice", new[] { "Consignee_Id" });
            DropTable("dbo.InvoiceItem");
            DropTable("dbo.Organization");
            DropTable("dbo.Invoice");
            RenameTable(name: "dbo.PermissionPolicyUserPermissionPolicyRole", newName: "PermissionPolicyUserPermissionPolicyRoles");
            RenameTable(name: "dbo.PermissionPolicyUser", newName: "PermissionPolicyUsers");
            RenameTable(name: "dbo.PermissionPolicyObjectPermissionsObject", newName: "PermissionPolicyObjectPermissionsObjects");
            RenameTable(name: "dbo.PermissionPolicyMemberPermissionsObject", newName: "PermissionPolicyMemberPermissionsObjects");
            RenameTable(name: "dbo.PermissionPolicyTypePermissionObject", newName: "PermissionPolicyTypePermissionObjects");
            RenameTable(name: "dbo.PermissionPolicyNavigationPermissionObject", newName: "PermissionPolicyNavigationPermissionObjects");
            RenameTable(name: "dbo.PermissionPolicyRoleBase", newName: "PermissionPolicyRoleBases");
            RenameTable(name: "dbo.ModuleInfo", newName: "ModuleInfoes");
            RenameTable(name: "dbo.ModelDifference", newName: "ModelDifferences");
            RenameTable(name: "dbo.ModelDifferenceAspect", newName: "ModelDifferenceAspects");
            RenameTable(name: "dbo.DashboardData", newName: "DashboardDatas");
            RenameTable(name: "dbo.Analysis", newName: "Analyses");
        }
    }
}

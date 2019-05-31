namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Analysis",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Criteria = c.String(),
                        ObjectTypeName = c.String(),
                        DimensionPropertiesString = c.String(),
                        PivotGridSettingsContent = c.Binary(),
                        ChartSettingsContent = c.Binary(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 10, unicode: false),
                        Name = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Code, unique: true);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        Name = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.DashboardData",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Title = c.String(),
                        SynchronizeTitle = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.InvoiceItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductCode = c.String(maxLength: 100),
                        ProductName = c.String(maxLength: 100),
                        Quantity = c.Int(nullable: false),
                        ManufacturerPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StateRegistryPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SupplierMarkupRate = c.Decimal(nullable: false, precision: 5, scale: 4),
                        ValueAddedTaxRate = c.Decimal(nullable: false, precision: 5, scale: 4),
                        Ean13 = c.String(maxLength: 13, fixedLength: true, unicode: false),
                        CustomsDeclarationNumber = c.String(maxLength: 30),
                        InvoiceId = c.Int(nullable: false),
                        ManufacturerId = c.Int(),
                        ManufacturerCountryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoice", t => t.InvoiceId, cascadeDelete: true)
                .ForeignKey("dbo.Organization", t => t.ManufacturerId)
                .ForeignKey("dbo.Country", t => t.ManufacturerCountryId)
                .Index(t => t.InvoiceId, name: "IX_Invoice_Id")
                .Index(t => t.ManufacturerId, name: "IX_Manufacturer_Id")
                .Index(t => t.ManufacturerCountryId, name: "IX_ManufacturerCountry_Id");
            
            CreateTable(
                "dbo.Invoice",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Guid = c.Guid(nullable: false),
                        Code = c.String(maxLength: 15),
                        DocDateTime = c.DateTime(),
                        ShipmentDateTime = c.DateTime(),
                        PaymentConditions = c.String(),
                        ProductGroup = c.String(),
                        Note = c.String(maxLength: 1000),
                        ConsigneeId = c.Int(),
                        ReceiverId = c.Int(),
                        SupplierId = c.Int(),
                        SupplierBankAccountId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organization", t => t.ConsigneeId)
                .ForeignKey("dbo.Organization", t => t.ReceiverId)
                .ForeignKey("dbo.Organization", t => t.SupplierId)
                .ForeignKey("dbo.BankAccount", t => t.SupplierBankAccountId)
                .Index(t => t.Guid, unique: true)
                .Index(t => t.ConsigneeId, name: "IX_Consignee_Id")
                .Index(t => t.ReceiverId, name: "IX_Receiver_Id")
                .Index(t => t.SupplierId, name: "IX_Supplier_Id")
                .Index(t => t.SupplierBankAccountId, name: "IX_SupplierBankAccount_Id");
            
            CreateTable(
                "dbo.Organization",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        TaxpayerCode = c.String(maxLength: 20),
                        TaxRegistrationReasonCode = c.String(maxLength: 20),
                        PhoneNumber = c.String(maxLength: 20),
                        Email = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 200),
                        CityId = c.Int(),
                        CountryId = c.Int(),
                        OrganizationId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.City", t => t.CityId)
                .ForeignKey("dbo.Country", t => t.CountryId)
                .ForeignKey("dbo.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.CityId, name: "IX_City_Id")
                .Index(t => t.CountryId, name: "IX_Country_Id")
                .Index(t => t.OrganizationId, name: "IX_Organization_Id");
            
            CreateTable(
                "dbo.BankAccount",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CheckingAccount = c.String(maxLength: 50, unicode: false),
                        CorrespondentAccount = c.String(maxLength: 50, unicode: false),
                        BankCode = c.String(maxLength: 20),
                        BankName = c.String(maxLength: 200),
                        BankBranchName = c.String(maxLength: 200),
                        OrganizationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId, name: "IX_Organization_Id");
            
            CreateTable(
                "dbo.ProductSeries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 20),
                        CertificateCode = c.String(maxLength: 50),
                        CertificateAuthority = c.String(maxLength: 100),
                        CertificateIssueDate = c.DateTime(),
                        CertificateExpireDate = c.DateTime(),
                        ShelfLifeDate = c.DateTime(),
                        RegionalCertificateCode = c.String(maxLength: 50),
                        RegionalCertificateAuthority = c.String(maxLength: 100),
                        RegionalCertificateIssueDate = c.DateTime(),
                        InvoiceItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InvoiceItem", t => t.InvoiceItemId, cascadeDelete: true)
                .Index(t => t.InvoiceItemId, name: "IX_InvoiceItem_Id");
            
            CreateTable(
                "dbo.ModelDifferenceAspect",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Xml = c.String(),
                        OwnerID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ModelDifference", t => t.OwnerID)
                .Index(t => t.OwnerID, name: "IX_Owner_ID");
            
            CreateTable(
                "dbo.ModelDifference",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ContextId = c.String(),
                        Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ModuleInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Version = c.String(),
                        AssemblyFileName = c.String(),
                        IsMain = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ReportDataV2",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DataTypeName = c.String(),
                        IsInplaceReport = c.Boolean(nullable: false),
                        PredefinedReportTypeName = c.String(),
                        Content = c.Binary(),
                        DisplayName = c.String(),
                        ParametersObjectTypeName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PermissionPolicyRoleBase",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsAdministrative = c.Boolean(nullable: false),
                        CanEditModel = c.Boolean(nullable: false),
                        PermissionPolicy = c.Int(nullable: false),
                        IsAllowPermissionPriority = c.Boolean(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PermissionPolicyNavigationPermissionObject",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ItemPath = c.String(),
                        TargetTypeFullName = c.String(),
                        NavigateState = c.Int(),
                        RoleID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyRoleBase", t => t.RoleID)
                .Index(t => t.RoleID, name: "IX_Role_ID");
            
            CreateTable(
                "dbo.PermissionPolicyTypePermissionObject",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TargetTypeFullName = c.String(),
                        ReadState = c.Int(),
                        WriteState = c.Int(),
                        CreateState = c.Int(),
                        DeleteState = c.Int(),
                        NavigateState = c.Int(),
                        RoleID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyRoleBase", t => t.RoleID)
                .Index(t => t.RoleID, name: "IX_Role_ID");
            
            CreateTable(
                "dbo.PermissionPolicyMemberPermissionsObject",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Members = c.String(),
                        Criteria = c.String(),
                        ReadState = c.Int(),
                        WriteState = c.Int(),
                        TypePermissionObjectID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyTypePermissionObject", t => t.TypePermissionObjectID)
                .Index(t => t.TypePermissionObjectID, name: "IX_TypePermissionObject_ID");
            
            CreateTable(
                "dbo.PermissionPolicyObjectPermissionsObject",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Criteria = c.String(),
                        ReadState = c.Int(),
                        WriteState = c.Int(),
                        DeleteState = c.Int(),
                        NavigateState = c.Int(),
                        TypePermissionObjectID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyTypePermissionObject", t => t.TypePermissionObjectID)
                .Index(t => t.TypePermissionObjectID, name: "IX_TypePermissionObject_ID");
            
            CreateTable(
                "dbo.PermissionPolicyUser",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        ChangePasswordOnFirstLogon = c.Boolean(nullable: false),
                        StoredPassword = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PermissionPolicyUserPermissionPolicyRole",
                c => new
                    {
                        PermissionPolicyUserID = c.Int(nullable: false),
                        PermissionPolicyRoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PermissionPolicyUserID, t.PermissionPolicyRoleID })
                .ForeignKey("dbo.PermissionPolicyUser", t => t.PermissionPolicyUserID, cascadeDelete: true)
                .ForeignKey("dbo.PermissionPolicyRoleBase", t => t.PermissionPolicyRoleID, cascadeDelete: true)
                .Index(t => t.PermissionPolicyUserID, name: "IX_PermissionPolicyUser_ID")
                .Index(t => t.PermissionPolicyRoleID, name: "IX_PermissionPolicyRole_ID");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PermissionPolicyUserPermissionPolicyRole", "PermissionPolicyRoleID", "dbo.PermissionPolicyRoleBase");
            DropForeignKey("dbo.PermissionPolicyUserPermissionPolicyRole", "PermissionPolicyUserID", "dbo.PermissionPolicyUser");
            DropForeignKey("dbo.PermissionPolicyTypePermissionObject", "RoleID", "dbo.PermissionPolicyRoleBase");
            DropForeignKey("dbo.PermissionPolicyObjectPermissionsObject", "TypePermissionObjectID", "dbo.PermissionPolicyTypePermissionObject");
            DropForeignKey("dbo.PermissionPolicyMemberPermissionsObject", "TypePermissionObjectID", "dbo.PermissionPolicyTypePermissionObject");
            DropForeignKey("dbo.PermissionPolicyNavigationPermissionObject", "RoleID", "dbo.PermissionPolicyRoleBase");
            DropForeignKey("dbo.ModelDifferenceAspect", "OwnerID", "dbo.ModelDifference");
            DropForeignKey("dbo.ProductSeries", "InvoiceItemId", "dbo.InvoiceItem");
            DropForeignKey("dbo.InvoiceItem", "ManufacturerCountryId", "dbo.Country");
            DropForeignKey("dbo.InvoiceItem", "ManufacturerId", "dbo.Organization");
            DropForeignKey("dbo.InvoiceItem", "InvoiceId", "dbo.Invoice");
            DropForeignKey("dbo.Invoice", "SupplierBankAccountId", "dbo.BankAccount");
            DropForeignKey("dbo.Invoice", "SupplierId", "dbo.Organization");
            DropForeignKey("dbo.Invoice", "ReceiverId", "dbo.Organization");
            DropForeignKey("dbo.Invoice", "ConsigneeId", "dbo.Organization");
            DropForeignKey("dbo.BankAccount", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.Address", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.Address", "CountryId", "dbo.Country");
            DropForeignKey("dbo.Address", "CityId", "dbo.City");
            DropIndex("dbo.PermissionPolicyUserPermissionPolicyRole", "IX_PermissionPolicyRole_ID");
            DropIndex("dbo.PermissionPolicyUserPermissionPolicyRole", "IX_PermissionPolicyUser_ID");
            DropIndex("dbo.PermissionPolicyObjectPermissionsObject", "IX_TypePermissionObject_ID");
            DropIndex("dbo.PermissionPolicyMemberPermissionsObject", "IX_TypePermissionObject_ID");
            DropIndex("dbo.PermissionPolicyTypePermissionObject", "IX_Role_ID");
            DropIndex("dbo.PermissionPolicyNavigationPermissionObject", "IX_Role_ID");
            DropIndex("dbo.ModelDifferenceAspect", "IX_Owner_ID");
            DropIndex("dbo.ProductSeries", "IX_InvoiceItem_Id");
            DropIndex("dbo.BankAccount", "IX_Organization_Id");
            DropIndex("dbo.Address", "IX_Organization_Id");
            DropIndex("dbo.Address", "IX_Country_Id");
            DropIndex("dbo.Address", "IX_City_Id");
            DropIndex("dbo.Invoice", "IX_SupplierBankAccount_Id");
            DropIndex("dbo.Invoice", "IX_Supplier_Id");
            DropIndex("dbo.Invoice", "IX_Receiver_Id");
            DropIndex("dbo.Invoice", "IX_Consignee_Id");
            DropIndex("dbo.Invoice", new[] { "Guid" });
            DropIndex("dbo.InvoiceItem", "IX_ManufacturerCountry_Id");
            DropIndex("dbo.InvoiceItem", "IX_Manufacturer_Id");
            DropIndex("dbo.InvoiceItem", "IX_Invoice_Id");
            DropIndex("dbo.Country", new[] { "Name" });
            DropIndex("dbo.City", new[] { "Code" });
            DropTable("dbo.PermissionPolicyUserPermissionPolicyRole");
            DropTable("dbo.PermissionPolicyUser");
            DropTable("dbo.PermissionPolicyObjectPermissionsObject");
            DropTable("dbo.PermissionPolicyMemberPermissionsObject");
            DropTable("dbo.PermissionPolicyTypePermissionObject");
            DropTable("dbo.PermissionPolicyNavigationPermissionObject");
            DropTable("dbo.PermissionPolicyRoleBase");
            DropTable("dbo.ReportDataV2");
            DropTable("dbo.ModuleInfo");
            DropTable("dbo.ModelDifference");
            DropTable("dbo.ModelDifferenceAspect");
            DropTable("dbo.ProductSeries");
            DropTable("dbo.BankAccount");
            DropTable("dbo.Address");
            DropTable("dbo.Organization");
            DropTable("dbo.Invoice");
            DropTable("dbo.InvoiceItem");
            DropTable("dbo.DashboardData");
            DropTable("dbo.Country");
            DropTable("dbo.City");
            DropTable("dbo.Analysis");
        }
    }
}

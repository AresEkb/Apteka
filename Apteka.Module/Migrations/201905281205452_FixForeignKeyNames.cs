namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixForeignKeyNames : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Note", name: "Contact_ContactId", newName: "ContactId");
            RenameColumn(table: "dbo.InvoiceItem", name: "Invoice_Id", newName: "InvoiceId");
            RenameColumn(table: "dbo.InvoiceItem", name: "Manufacturer_Id", newName: "ManufacturerId");
            RenameColumn(table: "dbo.InvoiceItem", name: "ManufacturerCountry_Id", newName: "ManufacturerCountryId");
            RenameColumn(table: "dbo.ProductSeries", name: "InvoiceItem_Id", newName: "InvoiceItemId");
            RenameColumn(table: "dbo.Invoice", name: "Consignee_Id", newName: "ConsigneeId");
            RenameColumn(table: "dbo.Invoice", name: "Receiver_Id", newName: "ReceiverId");
            RenameColumn(table: "dbo.Invoice", name: "Supplier_Id", newName: "SupplierId");
            RenameColumn(table: "dbo.Invoice", name: "SupplierBankAccount_Id", newName: "SupplierBankAccountId");
            RenameColumn(table: "dbo.Organization", name: "Address_Id", newName: "AddressId");
            RenameColumn(table: "dbo.BankAccount", name: "Organization_Id", newName: "OrganizationId");
            RenameColumn(table: "dbo.Address", name: "City_Id", newName: "CityId");
            RenameColumn(table: "dbo.Address", name: "Country_Id", newName: "CountryId");
            RenameColumn(table: "dbo.ModelDifferenceAspect", name: "Owner_ID", newName: "OwnerID");
            RenameColumn(table: "dbo.PermissionPolicyNavigationPermissionObject", name: "Role_ID", newName: "RoleID");
            RenameColumn(table: "dbo.PermissionPolicyTypePermissionObject", name: "Role_ID", newName: "RoleID");
            RenameColumn(table: "dbo.PermissionPolicyUserPermissionPolicyRole", name: "PermissionPolicyUser_ID", newName: "PermissionPolicyUserID");
            RenameColumn(table: "dbo.PermissionPolicyUserPermissionPolicyRole", name: "PermissionPolicyRole_ID", newName: "PermissionPolicyRoleID");
            RenameColumn(table: "dbo.PermissionPolicyMemberPermissionsObject", name: "TypePermissionObject_ID", newName: "TypePermissionObjectID");
            RenameColumn(table: "dbo.PermissionPolicyObjectPermissionsObject", name: "TypePermissionObject_ID", newName: "TypePermissionObjectID");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.PermissionPolicyObjectPermissionsObject", name: "TypePermissionObjectID", newName: "TypePermissionObject_ID");
            RenameColumn(table: "dbo.PermissionPolicyMemberPermissionsObject", name: "TypePermissionObjectID", newName: "TypePermissionObject_ID");
            RenameColumn(table: "dbo.PermissionPolicyUserPermissionPolicyRole", name: "PermissionPolicyRoleID", newName: "PermissionPolicyRole_ID");
            RenameColumn(table: "dbo.PermissionPolicyUserPermissionPolicyRole", name: "PermissionPolicyUserID", newName: "PermissionPolicyUser_ID");
            RenameColumn(table: "dbo.PermissionPolicyTypePermissionObject", name: "RoleID", newName: "Role_ID");
            RenameColumn(table: "dbo.PermissionPolicyNavigationPermissionObject", name: "RoleID", newName: "Role_ID");
            RenameColumn(table: "dbo.ModelDifferenceAspect", name: "OwnerID", newName: "Owner_ID");
            RenameColumn(table: "dbo.Address", name: "CountryId", newName: "Country_Id");
            RenameColumn(table: "dbo.Address", name: "CityId", newName: "City_Id");
            RenameColumn(table: "dbo.BankAccount", name: "OrganizationId", newName: "Organization_Id");
            RenameColumn(table: "dbo.Organization", name: "AddressId", newName: "Address_Id");
            RenameColumn(table: "dbo.Invoice", name: "SupplierBankAccountId", newName: "SupplierBankAccount_Id");
            RenameColumn(table: "dbo.Invoice", name: "SupplierId", newName: "Supplier_Id");
            RenameColumn(table: "dbo.Invoice", name: "ReceiverId", newName: "Receiver_Id");
            RenameColumn(table: "dbo.Invoice", name: "ConsigneeId", newName: "Consignee_Id");
            RenameColumn(table: "dbo.ProductSeries", name: "InvoiceItemId", newName: "InvoiceItem_Id");
            RenameColumn(table: "dbo.InvoiceItem", name: "ManufacturerCountryId", newName: "ManufacturerCountry_Id");
            RenameColumn(table: "dbo.InvoiceItem", name: "ManufacturerId", newName: "Manufacturer_Id");
            RenameColumn(table: "dbo.InvoiceItem", name: "InvoiceId", newName: "Invoice_Id");
            RenameColumn(table: "dbo.Note", name: "ContactId", newName: "Contact_ContactId");
        }
    }
}

namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ParentInvoiceRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InvoiceItem", "Invoice_Id", "dbo.Invoice");
            DropIndex("dbo.InvoiceItem", new[] { "Invoice_Id" });
            AlterColumn("dbo.InvoiceItem", "Invoice_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.InvoiceItem", "Invoice_Id");
            AddForeignKey("dbo.InvoiceItem", "Invoice_Id", "dbo.Invoice", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceItem", "Invoice_Id", "dbo.Invoice");
            DropIndex("dbo.InvoiceItem", new[] { "Invoice_Id" });
            AlterColumn("dbo.InvoiceItem", "Invoice_Id", c => c.Int());
            CreateIndex("dbo.InvoiceItem", "Invoice_Id");
            AddForeignKey("dbo.InvoiceItem", "Invoice_Id", "dbo.Invoice", "Id");
        }
    }
}

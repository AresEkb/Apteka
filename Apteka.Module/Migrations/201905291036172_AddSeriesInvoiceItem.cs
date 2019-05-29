namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSeriesInvoiceItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductSeries", "InvoiceItemId", "dbo.InvoiceItem");
            DropIndex("dbo.ProductSeries", "IX_InvoiceItem_Id");
            AlterColumn("dbo.ProductSeries", "InvoiceItemId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductSeries", "InvoiceItemId", name: "IX_InvoiceItem_Id");
            AddForeignKey("dbo.ProductSeries", "InvoiceItemId", "dbo.InvoiceItem", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductSeries", "InvoiceItemId", "dbo.InvoiceItem");
            DropIndex("dbo.ProductSeries", "IX_InvoiceItem_Id");
            AlterColumn("dbo.ProductSeries", "InvoiceItemId", c => c.Int());
            CreateIndex("dbo.ProductSeries", "InvoiceItemId", name: "IX_InvoiceItem_Id");
            AddForeignKey("dbo.ProductSeries", "InvoiceItemId", "dbo.InvoiceItem", "Id");
        }
    }
}

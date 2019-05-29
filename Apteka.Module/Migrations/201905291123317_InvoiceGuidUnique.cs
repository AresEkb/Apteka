namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvoiceGuidUnique : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Invoice", "Guid", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Invoice", new[] { "Guid" });
        }
    }
}

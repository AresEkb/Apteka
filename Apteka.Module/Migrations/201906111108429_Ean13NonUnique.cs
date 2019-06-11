namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ean13NonUnique : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.MedicineDosageForm", new[] { "Ean13" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.MedicineDosageForm", "Ean13", unique: true);
        }
    }
}

namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceLimitOptional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MedicineDosageForm", "PriceLimit", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.MedicineDosageForm", "PriceRegistrationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MedicineDosageForm", "PriceRegistrationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.MedicineDosageForm", "PriceLimit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}

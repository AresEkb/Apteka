namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MedicinePriceDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MedicineDosageForm", "PriceRegistrationDate", c => c.DateTime(storeType: "date"));
            AlterColumn("dbo.MedicineDosageForm", "PriceExclusionDate", c => c.DateTime(storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MedicineDosageForm", "PriceExclusionDate", c => c.DateTime());
            AlterColumn("dbo.MedicineDosageForm", "PriceRegistrationDate", c => c.DateTime());
        }
    }
}

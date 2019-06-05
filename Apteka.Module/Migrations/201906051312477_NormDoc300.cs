namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NormDoc300 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MedicineDosageForm", "NormativeDocument", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MedicineDosageForm", "NormativeDocument", c => c.String(maxLength: 200));
        }
    }
}

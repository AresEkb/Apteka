namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DosageForm300 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DosageForm", "Name", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DosageForm", "Name", c => c.String(maxLength: 200));
        }
    }
}

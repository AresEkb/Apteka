namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DosageFormHash : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MedicineDosageForm", "StateRegistryHash", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MedicineDosageForm", "StateRegistryHash");
        }
    }
}

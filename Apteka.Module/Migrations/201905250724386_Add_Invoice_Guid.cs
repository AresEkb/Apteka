namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Invoice_Guid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoice", "Guid", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoice", "Guid");
        }
    }
}

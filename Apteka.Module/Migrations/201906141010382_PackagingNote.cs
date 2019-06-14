namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PackagingNote : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MedicineDosageForm", "PrimaryPackagingNote", c => c.String());
            AddColumn("dbo.MedicineDosageForm", "IntermediatePackagingNote", c => c.String());
            AddColumn("dbo.MedicineDosageForm", "SecondaryPackagingNote", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MedicineDosageForm", "SecondaryPackagingNote");
            DropColumn("dbo.MedicineDosageForm", "IntermediatePackagingNote");
            DropColumn("dbo.MedicineDosageForm", "PrimaryPackagingNote");
        }
    }
}

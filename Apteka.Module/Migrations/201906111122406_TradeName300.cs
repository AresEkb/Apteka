namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TradeName300 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Medicine", new[] { "TradeName" });
            AlterColumn("dbo.Medicine", "TradeName", c => c.String(maxLength: 300));
            CreateIndex("dbo.Medicine", "TradeName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Medicine", new[] { "TradeName" });
            AlterColumn("dbo.Medicine", "TradeName", c => c.String(maxLength: 200));
            CreateIndex("dbo.Medicine", "TradeName", unique: true);
        }
    }
}

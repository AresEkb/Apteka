namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Medicine2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MedicinePriceLimitExclusionReason",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.MedicinePriceLimit", "ExclusionDate", c => c.DateTime());
            AddColumn("dbo.MedicinePriceLimit", "ExclusionReasonId", c => c.Int());
            CreateIndex("dbo.MedicinePriceLimit", "ExclusionReasonId", name: "IX_ExclusionReason_Id");
            AddForeignKey("dbo.MedicinePriceLimit", "ExclusionReasonId", "dbo.MedicinePriceLimitExclusionReason", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MedicinePriceLimit", "ExclusionReasonId", "dbo.MedicinePriceLimitExclusionReason");
            DropIndex("dbo.MedicinePriceLimit", "IX_ExclusionReason_Id");
            DropColumn("dbo.MedicinePriceLimit", "ExclusionReasonId");
            DropColumn("dbo.MedicinePriceLimit", "ExclusionDate");
            DropTable("dbo.MedicinePriceLimitExclusionReason");
        }
    }
}

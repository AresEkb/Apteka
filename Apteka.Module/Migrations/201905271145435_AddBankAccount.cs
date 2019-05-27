namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBankAccount : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankAccount",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CheckingAccount = c.String(maxLength: 50, unicode: false),
                        CorrespondentAccount = c.String(maxLength: 50, unicode: false),
                        BankCode = c.String(maxLength: 20),
                        BankName = c.String(maxLength: 200),
                        BankBranchName = c.String(maxLength: 200),
                        Organization_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organization", t => t.Organization_Id, cascadeDelete: true)
                .Index(t => t.Organization_Id);
            
            AddColumn("dbo.Invoice", "SupplierBankAccount_Id", c => c.Int());
            CreateIndex("dbo.Invoice", "SupplierBankAccount_Id");
            AddForeignKey("dbo.Invoice", "SupplierBankAccount_Id", "dbo.BankAccount", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoice", "SupplierBankAccount_Id", "dbo.BankAccount");
            DropForeignKey("dbo.BankAccount", "Organization_Id", "dbo.Organization");
            DropIndex("dbo.BankAccount", new[] { "Organization_Id" });
            DropIndex("dbo.Invoice", new[] { "SupplierBankAccount_Id" });
            DropColumn("dbo.Invoice", "SupplierBankAccount_Id");
            DropTable("dbo.BankAccount");
        }
    }
}

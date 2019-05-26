namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Address : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 200),
                        City_Id = c.Int(),
                        Country_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.City", t => t.City_Id)
                .ForeignKey("dbo.Country", t => t.Country_Id)
                .Index(t => t.City_Id)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 10, fixedLength: true, unicode: false),
                        Name = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Organization", "Name", c => c.String(maxLength: 200));
            AddColumn("dbo.Organization", "Address_Id", c => c.Int());
            AlterColumn("dbo.Country", "Code", c => c.String(maxLength: 2, fixedLength: true, unicode: false));
            CreateIndex("dbo.Organization", "Address_Id");
            AddForeignKey("dbo.Organization", "Address_Id", "dbo.Address", "Id");
            DropColumn("dbo.Organization", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Organization", "Address", c => c.String(maxLength: 200));
            DropForeignKey("dbo.Organization", "Address_Id", "dbo.Address");
            DropForeignKey("dbo.Address", "Country_Id", "dbo.Country");
            DropForeignKey("dbo.Address", "City_Id", "dbo.City");
            DropIndex("dbo.Address", new[] { "Country_Id" });
            DropIndex("dbo.Address", new[] { "City_Id" });
            DropIndex("dbo.Organization", new[] { "Address_Id" });
            AlterColumn("dbo.Country", "Code", c => c.String(maxLength: 2));
            DropColumn("dbo.Organization", "Address_Id");
            DropColumn("dbo.Organization", "Name");
            DropTable("dbo.City");
            DropTable("dbo.Address");
        }
    }
}

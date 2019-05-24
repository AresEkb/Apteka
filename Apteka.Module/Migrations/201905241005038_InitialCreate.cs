namespace Apteka.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Analyses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Criteria = c.String(),
                        ObjectTypeName = c.String(),
                        DimensionPropertiesString = c.String(),
                        PivotGridSettingsContent = c.Binary(),
                        ChartSettingsContent = c.Binary(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DashboardDatas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Title = c.String(),
                        SynchronizeTitle = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ModelDifferenceAspects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Xml = c.String(),
                        Owner_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ModelDifferences", t => t.Owner_ID)
                .Index(t => t.Owner_ID);
            
            CreateTable(
                "dbo.ModelDifferences",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ContextId = c.String(),
                        Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ModuleInfoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Version = c.String(),
                        AssemblyFileName = c.String(),
                        IsMain = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PermissionPolicyRoleBases",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsAdministrative = c.Boolean(nullable: false),
                        CanEditModel = c.Boolean(nullable: false),
                        PermissionPolicy = c.Int(nullable: false),
                        IsAllowPermissionPriority = c.Boolean(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PermissionPolicyNavigationPermissionObjects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ItemPath = c.String(),
                        TargetTypeFullName = c.String(),
                        NavigateState = c.Int(),
                        Role_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyRoleBases", t => t.Role_ID)
                .Index(t => t.Role_ID);
            
            CreateTable(
                "dbo.PermissionPolicyTypePermissionObjects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TargetTypeFullName = c.String(),
                        ReadState = c.Int(),
                        WriteState = c.Int(),
                        CreateState = c.Int(),
                        DeleteState = c.Int(),
                        NavigateState = c.Int(),
                        Role_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyRoleBases", t => t.Role_ID)
                .Index(t => t.Role_ID);
            
            CreateTable(
                "dbo.PermissionPolicyMemberPermissionsObjects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Members = c.String(),
                        Criteria = c.String(),
                        ReadState = c.Int(),
                        WriteState = c.Int(),
                        TypePermissionObject_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyTypePermissionObjects", t => t.TypePermissionObject_ID)
                .Index(t => t.TypePermissionObject_ID);
            
            CreateTable(
                "dbo.PermissionPolicyObjectPermissionsObjects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Criteria = c.String(),
                        ReadState = c.Int(),
                        WriteState = c.Int(),
                        DeleteState = c.Int(),
                        NavigateState = c.Int(),
                        TypePermissionObject_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyTypePermissionObjects", t => t.TypePermissionObject_ID)
                .Index(t => t.TypePermissionObject_ID);
            
            CreateTable(
                "dbo.PermissionPolicyUsers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        ChangePasswordOnFirstLogon = c.Boolean(nullable: false),
                        StoredPassword = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PermissionPolicyUserPermissionPolicyRoles",
                c => new
                    {
                        PermissionPolicyUser_ID = c.Int(nullable: false),
                        PermissionPolicyRole_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PermissionPolicyUser_ID, t.PermissionPolicyRole_ID })
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.PermissionPolicyUser_ID, cascadeDelete: true)
                .ForeignKey("dbo.PermissionPolicyRoleBases", t => t.PermissionPolicyRole_ID, cascadeDelete: true)
                .Index(t => t.PermissionPolicyUser_ID)
                .Index(t => t.PermissionPolicyRole_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PermissionPolicyUserPermissionPolicyRoles", "PermissionPolicyRole_ID", "dbo.PermissionPolicyRoleBases");
            DropForeignKey("dbo.PermissionPolicyUserPermissionPolicyRoles", "PermissionPolicyUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.PermissionPolicyTypePermissionObjects", "Role_ID", "dbo.PermissionPolicyRoleBases");
            DropForeignKey("dbo.PermissionPolicyObjectPermissionsObjects", "TypePermissionObject_ID", "dbo.PermissionPolicyTypePermissionObjects");
            DropForeignKey("dbo.PermissionPolicyMemberPermissionsObjects", "TypePermissionObject_ID", "dbo.PermissionPolicyTypePermissionObjects");
            DropForeignKey("dbo.PermissionPolicyNavigationPermissionObjects", "Role_ID", "dbo.PermissionPolicyRoleBases");
            DropForeignKey("dbo.ModelDifferenceAspects", "Owner_ID", "dbo.ModelDifferences");
            DropIndex("dbo.PermissionPolicyUserPermissionPolicyRoles", new[] { "PermissionPolicyRole_ID" });
            DropIndex("dbo.PermissionPolicyUserPermissionPolicyRoles", new[] { "PermissionPolicyUser_ID" });
            DropIndex("dbo.PermissionPolicyObjectPermissionsObjects", new[] { "TypePermissionObject_ID" });
            DropIndex("dbo.PermissionPolicyMemberPermissionsObjects", new[] { "TypePermissionObject_ID" });
            DropIndex("dbo.PermissionPolicyTypePermissionObjects", new[] { "Role_ID" });
            DropIndex("dbo.PermissionPolicyNavigationPermissionObjects", new[] { "Role_ID" });
            DropIndex("dbo.ModelDifferenceAspects", new[] { "Owner_ID" });
            DropTable("dbo.PermissionPolicyUserPermissionPolicyRoles");
            DropTable("dbo.PermissionPolicyUsers");
            DropTable("dbo.PermissionPolicyObjectPermissionsObjects");
            DropTable("dbo.PermissionPolicyMemberPermissionsObjects");
            DropTable("dbo.PermissionPolicyTypePermissionObjects");
            DropTable("dbo.PermissionPolicyNavigationPermissionObjects");
            DropTable("dbo.PermissionPolicyRoleBases");
            DropTable("dbo.ModuleInfoes");
            DropTable("dbo.ModelDifferences");
            DropTable("dbo.ModelDifferenceAspects");
            DropTable("dbo.DashboardDatas");
            DropTable("dbo.Analyses");
        }
    }
}

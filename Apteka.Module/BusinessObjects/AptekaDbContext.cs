using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using DevExpress.ExpressApp.EF.Updating;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;

namespace Apteka.Module.BusinessObjects
{
    public class AptekaDbContext : DbContext {
		public AptekaDbContext(String connectionString)
			: base(connectionString) {
		}
		public AptekaDbContext(DbConnection connection)
			: base(connection, false) {
		}
		public AptekaDbContext()
			: base("name=ConnectionString") {
		}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ModuleInfo> ModulesInfo { get; set; }
	    public DbSet<PermissionPolicyRole> Roles { get; set; }
		public DbSet<PermissionPolicyTypePermissionObject> TypePermissionObjects { get; set; }
		public DbSet<PermissionPolicyUser> Users { get; set; }
		public DbSet<DashboardData> DashboardData { get; set; }
		public DbSet<Analysis> Analysis { get; set; }
		public DbSet<ModelDifference> ModelDifferences { get; set; }
		public DbSet<ModelDifferenceAspect> ModelDifferenceAspects { get; set; }

        public DbSet<Invoice> Invoices { get; set; }
    }
}

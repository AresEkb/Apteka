using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using Apteka.Model.Entities;
using Apteka.Model.Entities.Place;
using Apteka.Module.Conventions;

using DevExpress.ExpressApp.EF.Updating;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;

namespace Apteka.Module.BusinessObjects
{
    public class AptekaDbContext : DbContext
    {
		public AptekaDbContext(String connectionString) : base(connectionString)
        {
            Database.Log = Console.WriteLine;
        }

        public AptekaDbContext(DbConnection connection) : base(connection, false)
        {
            Database.Log = Console.WriteLine;
        }

        public AptekaDbContext() : base("name=ConnectionString")
        {
            Database.Log = Console.WriteLine;
		}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Add<ForeignKeyNamingConvention>();

            modelBuilder.Conventions.Add<UniqueIndexAttributeConvention>();
            modelBuilder.Conventions.Add<DataTypeAttributeConvention>();
            modelBuilder.Conventions.Add<DecimalPrecisionConvention>();
            modelBuilder.Conventions.Add<NonUnicodeAttributeConvention>();

            modelBuilder.Conventions.Remove<MaxLengthAttributeConvention>();
            modelBuilder.Conventions.Add<CustomMaxLengthAttributeConvention>();

            // TODO: Implement as a convention
            //modelBuilder.Conventions.Add<CompositionAttributeConvention>();
            modelBuilder.Entity<Model.Entities.Organization>()
                .HasOptional(e => e.Address)
                .WithOptionalPrincipal()
                .WillCascadeOnDelete();

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

        public DbSet<ReportDataV2> ReportData { get; set; }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Model.Entities.Place.Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
    }
}

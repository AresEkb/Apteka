using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using Apteka.Model.Entities;
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
		}

		public AptekaDbContext(DbConnection connection) : base(connection, false)
        {
		}

		public AptekaDbContext() : base("name=ConnectionString")
        {
		}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Add<ForeignKeyNamingConvention>();

            modelBuilder.Conventions.Add<AlternateKeyAttributeConvention>();
            modelBuilder.Conventions.Add<DecimalPrecisionConvention>();
            modelBuilder.Conventions.Add<NonUnicodeAttributeConvention>();

            modelBuilder.Conventions.Remove<MaxLengthAttributeConvention>();
            modelBuilder.Conventions.Add<CustomMaxLengthAttributeConvention>();

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
        public DbSet<Model.Entities.Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }

    [DevExpress.Persistent.Base.DefaultClassOptions]
    public class Contact
    {
        [Browsable(false)]
        public Int32 ContactId { get; protected set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public Contact()
        {
            NotesCollection = new List<Note>();
        }
        [DevExpress.ExpressApp.DC.Aggregated]
        public virtual IList<Note> NotesCollection { get; set; }
    }

    //[DefaultClassOptions]
    public class Note
    {
        [Browsable(false)]
        public Int32 NoteId { get; protected set; }
        public String Text { get; set; }
        public virtual Contact Contact { get; set; }
    }
}

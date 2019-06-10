using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

using Apteka.Model.Annotations;
using Apteka.Model.Entities;
using Apteka.Model.Entities.Place;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Apteka.Model.EFCore
{
    public class AptekaDbContext : DbContext
    {
		public AptekaDbContext() : base()
        {
		}

		public AptekaDbContext(DbContextOptions options) : base(options)
        {
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Note: prop.PropertyInfo is null for auto generated properties like CityId, etc.

            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.DisplayName();
            }

            //modelBuilder.Conventions.Add<AlternateKeyAttributeConvention>();
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var propAttrs = from prop in entity.GetProperties()
                                select new { prop, attr = prop.PropertyInfo?.GetCustomAttribute<UniqueIndexAttribute>() };
                var keys = from pa in propAttrs
                           where pa.attr != null
                           group pa by pa.attr.Name;
                foreach (var key in keys)
                {
                    var index = entity.AddIndex(key.OrderBy(k => k.attr.Order).Select(k => k.prop).ToList());
                    index.Relational().Name = key.Key;
                }
            }

            //modelBuilder.Conventions.Add<DecimalPrecisionConvention>();
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var prop in entity.GetProperties())
                {
                    var attr = prop.PropertyInfo?.GetCustomAttribute<DecimalPrecisionAttribute>();
                    if (attr != null)
                    {
                        prop.Relational().ColumnType = String.Format("decimal({0}, {1})", attr.Precision, attr.Scale);
                    }
                }
            }

            //modelBuilder.Conventions.Add<NonUnicodeAttributeConvention>();
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var prop in entity.GetProperties())
                {
                    var attr = prop.PropertyInfo?.GetCustomAttribute<NonUnicodeAttribute>();
                    if (attr != null)
                    {
                        prop.IsUnicode(false);
                    }
                }
            }

            //modelBuilder.Conventions.Remove<MaxLengthAttributeConvention>();
            //modelBuilder.Conventions.Add<CustomMaxLengthAttributeConvention>();
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var prop in entity.GetProperties())
                {
                    var minAttr = prop.PropertyInfo?.GetCustomAttribute<MinLengthAttribute>();
                    if (minAttr != null)
                    {
                        var maxAttr = prop.PropertyInfo?.GetCustomAttribute<MaxLengthAttribute>();
                        if (maxAttr != null && minAttr.Length == maxAttr.Length)
                        {
                            prop.Relational().IsFixedLength = true;
                        }
                    }
                }
            }

            // TODO: Implement as a convention
            //modelBuilder.Conventions.Add<CompositionAttributeConvention>();
            //modelBuilder.Entity<Model.Entities.Organization>()
            //    .HasOptional(e => e.Address)
            //    .WithOptionalPrincipal()
            //    .WillCascadeOnDelete(true);
            // https://github.com/aspnet/EntityFrameworkCore/issues/5871
            modelBuilder.Entity<Organization>()
                .HasOne(e => e.Address)
                .WithOne()
                .HasForeignKey<Address>("OrganizationId")
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<MedicineDosageForm> MedicineDosageForms { get; set; }
    }
}

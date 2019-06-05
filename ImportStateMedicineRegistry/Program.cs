using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

using Apteka.Model.EFCore;

using CsvHelper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Apteka.Model.Mappers;
using Apteka.Model.Dtos;

namespace ImportStateMedicineRegistry
{
    partial class Program
    {
        static void Main(string[] args)
        {
            // Config
            IConfiguration config = new ConfigurationBuilder()
                      .AddJsonFile("appsettings.json", true, true)
                      .Build();

            // Database
            string connection = config.GetConnectionString("DefaultConnection");
            var context = new AptekaDbContext(
                new DbContextOptionsBuilder()
                    .UseSqlServer(connection)
                    .Options);

            // Mapper
            var mapper = new StateMedicineRegistryItemMapper(new EntityFactory(context));

            // Import
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var reader = new StreamReader("grls2019-06-04-1.csv", Encoding.GetEncoding("windows-1251")))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.RegisterClassMap<StateMedicineRegistryItemMap>();
                csv.Read(); // Skip first line
                var records = csv.GetRecords<StateMedicineRegistryItem>();
                foreach (var entity in records
                    .Where(r => r.IsOk)
                    .Select(r => mapper.Map(r, false))
                    .Where(r => r != null)
                    .Take(100))
                {
                    if (entity.Id == 0)
                    {
                        context.Add(entity);
                    }
                }
            }

            // Validation
            var hasErrors = false;
            foreach (var entity in context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .Select(e => e.Entity))
            {
                var validationContext = new ValidationContext(entity);
                var results = new List<ValidationResult>();
                if (!Validator.TryValidateObject(entity, validationContext, results, true))
                {
                    var messages = results.Select(r => r.ErrorMessage).ToList().Aggregate((message, nextMessage) => message + ", " + nextMessage);
                    Console.WriteLine($"Unable to save changes for {entity.ToString()} due to error(s): {messages}");
                    hasErrors = true;
                }
            }

            // Update database
            if (!hasErrors)
            {
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

    }
}

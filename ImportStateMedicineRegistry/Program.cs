using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

using Apteka.Model.Dtos;
using Apteka.Model.EFCore;
using Apteka.Model.Entities;
using Apteka.Model.Entities.Place;
using Apteka.Model.Mappers;

using CsvHelper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ImportStateMedicineRegistry
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // Config
            IConfiguration config = new ConfigurationBuilder()
                      .AddJsonFile("appsettings.json", true, true)
                      .Build();

            // Database
            string connection = config.GetConnectionString("DefaultConnection");
            var context = new AptekaDbContext(
                new DbContextOptionsBuilder()
                    //.UseLoggerFactory(new LoggerFactory().AddConsole())
                    .UseSqlServer(connection)
                    .Options);

            // Mapper
            var entityFactory = new EntityFactory(context);
            var start = DateTime.Now;
            Console.WriteLine("{0} Caching data...", start);
            entityFactory.CacheAll<MedicineDosageForm>();
            entityFactory.CacheAll<Medicine>();
            entityFactory.CacheAll<PharmacotherapeuticGroup>();
            entityFactory.CacheAll<AtcGroup>();
            entityFactory.CacheAll<Organization>();
            entityFactory.CacheAll<Country>();

            // Import
            // ЛП-000001
            // ЛС-000001
            // ЛСР-000001
            // ЛСР-000001/10
            // П N002845
            // П N003014/01
            // П N011506/01-1999
            // Р N002321/01
            // Р N002321/01-2003
            // ФС-000001

            Console.WriteLine("{0} Processing data...", DateTime.Now);
            var mapper = new StateMedicineRegistryItemMapper(entityFactory);
            using (var reader = new StreamReader("grls2019-06-04-1.csv", Encoding.GetEncoding("windows-1251")))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.RegisterClassMap<StateMedicineRegistryItemMap>();
                csv.Read(); // Skip first line
                var records = csv.GetRecords<StateMedicineRegistryItem>();
                foreach (var entity in records
                    .Where(r => r.IsOk)
                    .Select(r => mapper.Map(r, false, true))
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
            Console.WriteLine("{0} Validating data...", DateTime.Now);
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
            Console.WriteLine("{0} Updating database...", DateTime.Now);
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
            var end = DateTime.Now;
            Console.WriteLine("{0} Done in {1}", end, end - start);

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}

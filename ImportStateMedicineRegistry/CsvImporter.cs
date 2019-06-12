using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

using Apteka.Model.Dtos.Base;
using Apteka.Model.EFCore;
using Apteka.Model.Entities.Base;
using Apteka.Model.Mappers.Base;

using CsvHelper;
using CsvHelper.Configuration;

using Microsoft.EntityFrameworkCore;

namespace ImportStateMedicineRegistry
{
    public abstract class CsvImporter<TRecord, TEntity, TCsvMap, TMapperFactory>
        where TRecord : IRecord
        where TEntity : class, IEntity
        where TCsvMap : ClassMap
        where TMapperFactory : IMapperFactory<TRecord, TEntity>, new()
    {
        private readonly DbContext context;

        public CsvImporter(DbContext context)
        {
            this.context = context;
        }

        public void Run(string fileName, Encoding encoding, int linesToSkip, int count)
        {
            // Import
            var start = DateTime.Now;
            Console.WriteLine("{0} Processing data...", start);
            var mapper = new TMapperFactory().Create(new EntityFactory(context), false, true);
            using (var reader = new StreamReader(fileName, encoding))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.HasHeaderRecord = false;
                csv.Configuration.RegisterClassMap<TCsvMap>();
                for (int line = 0; line < linesToSkip; line++)
                {
                    csv.Read();
                }
                var records = csv.GetRecords<TRecord>();
                int i = 0;
                foreach (var entity in records
                    //.Where(r => r.IsOk)
                    .Select(r => mapper.Map(r))
                    .Where(r => r != null)
                    .Take(count))
                {
                    if (entity.Id == 0)
                    {
                        context.Add(entity);
                    }
                    if (++i % 1000 == 0)
                    {
                        Console.WriteLine("{0} {1} records", DateTime.Now, i);
                    }
                }
                if (i % 1000 != 0)
                {
                    Console.WriteLine("{0} {1} records", DateTime.Now, i);
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
            if (!hasErrors)
            {
                Console.WriteLine("{0} Updating database...", DateTime.Now);
                try
                {
                    //context.SaveChanges();
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

//using System;

//using Apteka.Model.EFCore;

//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;

//namespace ImportStateMedicineRegistry
//{
//    public class AptekaDbContextFactory : IDesignTimeDbContextFactory<AptekaDbContext>
//    {
//        public AptekaDbContext CreateDbContext(string[] args)
//        {
//            var builder = new ConfigurationBuilder()
//                .SetBasePath(AppContext.BaseDirectory)
//                .AddJsonFile("appsettings.json");

//            var config = builder.Build();

//            var connstr = config.GetConnectionString("DefaultConnection");

//            if (String.IsNullOrWhiteSpace(connstr) == true)
//            {
//                throw new InvalidOperationException(
//                    "Could not find a connection string named 'DefaultConnection'.");
//            }
//            else
//            {
//                return Create(connstr);
//            }
//        }

//        private AptekaDbContext Create(string connectionString)
//        {
//            if (string.IsNullOrEmpty(connectionString))
//                throw new ArgumentException(
//                $"{nameof(connectionString)} is null or empty.",
//                nameof(connectionString));

//            var optionsBuilder =
//             new DbContextOptionsBuilder<AptekaDbContext>();

//            optionsBuilder.UseSqlServer(connectionString);

//            return new AptekaDbContext(optionsBuilder.Options);
//        }
//    }
//}

using System;
using System.IO;

using Apteka.Model.EFCore;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ImportStateMedicineRegistry
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddDbContext<AptekaDbContext>(o =>
                    o//.UseLoggerFactory(new LoggerFactory().AddConsole())
                     .UseLazyLoadingProxies()
                     .UseSqlServer(config.GetConnectionString("DefaultConnection")))
                .BuildServiceProvider();

            var context = serviceProvider.GetRequiredService<AptekaDbContext>();

            new Startup(context).Run();
        }
    }
}

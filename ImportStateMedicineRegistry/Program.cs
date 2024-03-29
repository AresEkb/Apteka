﻿using System;
using System.IO;
using System.Text;
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
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

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

            //new StateMedicineRegistryImporter(context).Run("grls2019-06-04-1.csv", Encoding.GetEncoding("windows-1251"), 1, 1000);
            new StateMedicinePriceRegistryImporter(context).Run("lp2019-06-04-1.csv", Encoding.GetEncoding("windows-1251"), 3, 5000);
        }
    }
}

using System;
using System.Reflection;
using System.Runtime.Versioning;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using Serilog;
using Serilog.Events;

namespace Apteka.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // TODO: Read settings from appsettings.json
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File("logs/Apteka.Api-.log", rollingInterval: RollingInterval.Day, shared: true)
                .CreateLogger();

            try
            {
                Log.Information("Starting web host");
                string ver = Assembly.GetEntryAssembly()
                    ?.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName;
                Log.Information("Framework: " + ver);
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.Information("Stoping web host");
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace IdSrv4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
          .MinimumLevel.Warning()
          .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
          .Enrich.FromLogContext()
          .WriteTo.File("../../LogFiles/idsrv4.log", fileSizeLimitBytes: 1 * 1024 * 1024, rollingInterval:RollingInterval.Day,retainedFileCountLimit:3,rollOnFileSizeLimit:true)
          .CreateLogger();

            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var configurationDbContext = services.GetRequiredService<ConfigurationDbContext>();
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

                var intializer = new DbInitializer(services);
                try {
                    intializer.InitializeDataAsync();
                }
                catch (Exception ex) {

                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database");

                }
            }
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}

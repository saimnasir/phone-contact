using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace Reporting
{

    [Obsolete]
    public class Program
    {
        public static int Main(string[] args)
        {
            LoggerConfiguration logConfig = logCongifuration();

            Log.Logger = logConfig.CreateLogger();

            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }


        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://localhost:5002/")
                .UseStartup<Startup>()
                .UseSerilog(); // <-- Add this line;


        private static LoggerConfiguration logCongifuration()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var logConfig = new LoggerConfiguration()
                             .Enrich.FromLogContext()
                             .WriteTo.File(
                                path: "Logs\\log.json",
                                outputTemplate: "[{Level:u3}] {Timestamp:yyyy:MM:dd HH:mm:ss} {Message:lj}{NewLine}{Exception}"
                            );


            if (environment == EnvironmentName.Development)
            {
                logConfig.MinimumLevel.Debug();
            }
            else if (environment == EnvironmentName.Staging)
            {
                logConfig.MinimumLevel.Information();
            }
            else if (environment == EnvironmentName.Production)
            {
                logConfig.MinimumLevel.Warning();
            }
            else // as default
            {
                logConfig.MinimumLevel.Warning();
            }

            return logConfig;
        }

    }
}

namespace Website
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Olive;
    using System;

    public class Program
    {
        public static void Main(string[] args) => CreateWebHostBuilder(args).Build().Run();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
              .ConfigureLogging(ConfigureLogging)
              .ConfigureAppConfiguration((context, config) =>
              {
                  var machineName = Environment.MachineName;
                  var env = context.HostingEnvironment;
                  config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                       // Load environment-specific appsettings.{Environment}.json (e.g., Development, Production)
                       .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                       // Load machine-specific appsettings.{machineName}.json
                       .AddJsonFile($"appsettings.{machineName}.json", optional: true, reloadOnChange: true);

              })
              .UseSetting("detailedErrors", "true")
              .CaptureStartupErrors(true)
              .UseStartup<Startup>();
        }

        static void ConfigureLogging(WebHostBuilderContext context, ILoggingBuilder logging)
        {
            // You can customise logging here
            if (!context.HostingEnvironment.IsDevelopment())
                logging.AddFile(x => x.FilePrefix = "log-");
        }
    }
}
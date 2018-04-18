namespace Website
{
    using System;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Logging;
    using Olive;

    public class Program
    {
        public static void Main(string[] args) => BuildWebHost(args).Run();

        public static IWebHost BuildWebHost(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(ConfigureLogging)
                .UseStartup<Startup>();

            builder.UseSetting("detailedErrors", "true").CaptureStartupErrors(true);

            // Is this needed?  
            builder.UseIISIntegration();

            return builder.Build();
        }

        static void ConfigureLogging(ILoggingBuilder logging)
        {
            // You can customise logging here
            // logging.AddFile(x => x.FilePrefix = "log-");
        }
    }
}

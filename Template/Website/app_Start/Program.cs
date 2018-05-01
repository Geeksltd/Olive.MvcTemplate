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
            return WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(ConfigureLogging)
                .UseSetting("detailedErrors", "true").CaptureStartupErrors(true)
                .UseStartup<Startup>()
                .Build();
        }

        static void ConfigureLogging(WebHostBuilderContext context, ILoggingBuilder logging)
        {
            // You can customise logging here
            if (!context.HostingEnvironment.IsDevelopment())
                logging.AddFile(x => x.FilePrefix = "log-");
        }
    }
}
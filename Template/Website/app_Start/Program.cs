namespace Website
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    public class Program
    {
        const bool RELEASE_MODE = false;

        public static void Main(string[] args) => BuildWebHost(args).Run();

        public static IWebHost BuildWebHost(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();

            if (RELEASE_MODE)
                builder.UseSetting("detailedErrors", "true").CaptureStartupErrors(true);

            // Is this needed?
            builder.UseIISIntegration();

            return builder.Build();
        }
    }
}

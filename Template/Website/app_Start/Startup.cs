namespace Website
{
    using System.Globalization;
    using System.Threading.Tasks;
    using Domain;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Olive;
    using Olive.Entities.Data;
    using Olive.Hangfire;
    using Olive.Mvc.Testing;

    public class Startup : Olive.Mvc.Startup
    {
        protected override CultureInfo GetRequestCulture() => new CultureInfo("en-GB");

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services.AddDatabaseLogger();

            AuthenticationBuilder.AddSocialAuth();
            services.AddScheduledTasks();
        }

        public override void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            base.Configure(app, env);
            if (env.IsDevelopment()) app.UseWebTest(config => config.AddTasks());

            if (Config.Get<bool>("Automated.Tasks:Enabled"))
                app.UseScheduledTasks(TaskManager.Run);
        }

        public override async Task OnStartUpAsync(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                await app.InitializeTempDatabase<SqlServerManager>(() => ReferenceData.Create());

            // Add any other initialization logic that needs the database to be ready here.
        }
    }
}
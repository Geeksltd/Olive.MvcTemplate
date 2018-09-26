namespace Website
{
    using Domain;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Olive;
    using Olive.Entities.Data;
    using Olive.Hangfire;
    using Olive.Mvc.Testing;
    using System.Globalization;

    public class Startup : Olive.Mvc.Startup
    {
        public Startup(IHostingEnvironment env, IConfiguration config) : base(env, config) { }

        protected override CultureInfo GetRequestCulture() => new CultureInfo("en-GB");

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services.AddDatabaseLogger();
            services.AddScheduledTasks();

            if (Environment.IsDevelopment())
                services.AddDevCommands(x => x.AddTempDatabase<SqlServerManager, ReferenceData>());
        }

        protected override void ConfigureAuthentication(AuthenticationBuilder auth)
        {
            base.ConfigureAuthentication(auth);
            auth.AddSocialAuth();
        }

        public override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);

            if (Config.Get<bool>("Automated.Tasks:Enabled"))
                app.UseScheduledTasks(TaskManager.Run);
        }
    }
}
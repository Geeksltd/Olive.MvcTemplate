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
    using System.Threading.Tasks;

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

        public override async Task OnStartUpAsync(IApplicationBuilder app)
        {
            await base.OnStartUpAsync(app);
            app.UseScheduledTasks<TaskManager>();
        }

        public override void Configure(IApplicationBuilder app)
        {
            base.Configure(app);
            app.UseGlobalSearch<GlobalSearchSource>();
        }

        #region Show error screen even in production?
        // Uncomment the following:
        // protected override void ConfigureExceptionPage(IApplicationBuilder app) 
        //    => app.UseDeveloperExceptionPage();
        #endregion
    }
}
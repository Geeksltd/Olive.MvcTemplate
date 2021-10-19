namespace Website
{
    using BotDetect.Web;
    using Domain;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Olive;
    using Olive.Entities.Data;
    using Olive.Hangfire;
    using Olive.Mvc.Testing;
    using System.Globalization;
    using System.Threading.Tasks;

    public class Startup : Olive.Mvc.Startup
    {
        public Startup(IWebHostEnvironment env, IConfiguration config, ILoggerFactory loggerFactory)
            : base(env, config, loggerFactory)
        {
        }

        protected override CultureInfo GetRequestCulture() => new CultureInfo("en-GB");

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services.AddDataAccess(x => x.SqlServer());

            services.AddDatabaseLogger();
            services.AddScheduledTasks();

            if (Environment.IsDevelopment())
                services.AddDevCommands(x => x.AddTempDatabase<SqlServerManager, ReferenceData>());
            services.AddSession(options => options.IdleTimeout = 20.Minutes());
            services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);
            services.Configure<IISServerOptions>(options => options.AllowSynchronousIO = true);
        }

        protected override void ConfigureRequestHandlers(IApplicationBuilder app)
        {
            app.UseSession();
            base.ConfigureRequestHandlers(app);
            app.UseCaptcha(Configuration);
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

        #region Show error screen even in production?
        // Uncomment the following:
        // protected override void ConfigureExceptionPage(IApplicationBuilder app) 
        //    => app.UseDeveloperExceptionPage();
        #endregion
    }
}
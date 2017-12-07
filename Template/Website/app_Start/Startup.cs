namespace Website
{
    using System.Globalization;
    using System.Threading.Tasks;
    using Domain;
    using Hangfire;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Olive;
    using Olive.Services.Testing;

    public class Startup : Olive.Mvc.Startup
    {
        protected override CultureInfo GetRequestCulture() => new CultureInfo("en-GB");

        protected override IServiceCollection AddIdentityAndStores(IServiceCollection services)
        {
            services.AddSingleton<IUserStore<User>, UserStore>();
            services.AddSingleton<IRoleStore<string>, RoleStore>();
            services.AddIdentity<User, string>();

            return services;
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            StartupAuth.Configure(AuthenticationBuilder);
            services.AddHangfire(config => config.UseSqlServerStorage(Config.GetConnectionString("AppDatabase")));
        }        

        public override void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            base.Configure(app, env);

            RegisterServiceImplementors();

            if (Config.Get<bool>("Automated.Tasks:Enabled"))
            {
                app.UseHangfireServer();

                if (System.Diagnostics.Debugger.IsAttached)
                    app.UseHangfireDashboard();

                TaskManager.Run();
            }
        }

        void RegisterServiceImplementors()
        {
            if (WebTestManager.IsTddExecutionMode())
            {
                // TODO: Add real service implementors:
                // e.g. CurrencyService.RegisterImplementor<European.Central.Bank.FakeCurrencyServiceImplementor>();
            }
            else
            {
                // TODO: Register fake service implementors:
                // e.g. CurrencyService.RegisterImplementor<European.Central.Bank.CurrencyServiceImplementor>();
            }
        }

        /// <summary>Invoked by the WebTestManager right after creating a new database.</summary>
        protected override Task CreateReferenceData() => ReferenceData.Create();
    }
}
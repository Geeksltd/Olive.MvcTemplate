namespace Website
{
    using System.Globalization;
    using System.Threading.Tasks;
    using Domain;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Olive;
    using Olive.Services.Hangfire;
    using Olive.Services.Testing;

    public class Startup : Olive.Mvc.Startup
    {
        protected override CultureInfo GetRequestCulture() => new CultureInfo("en-GB");

public override void ConfigureServices(IServiceCollection services)
{
    base.ConfigureServices(services);

    AuthenticationBuilder.AddSocialAuth();
    services.AddScheduledTasks();
}

public override void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.UseWebTest(ReferenceData.Create, config => config.AddTasks());
    base.Configure(app, env);

    if (Config.Get<bool>("Automated.Tasks:Enabled"))
        app.UseScheduledTasks(TaskManager.Run);
}
    }
}
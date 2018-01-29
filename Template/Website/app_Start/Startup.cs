namespace Website
{
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Olive;
    using Olive.Services.Hangfire;
    using Olive.Services.Testing;
    using Swashbuckle.AspNetCore.Swagger;

    public class Startup : Olive.Mvc.Startup
    {
        protected override CultureInfo GetRequestCulture() => new CultureInfo("en-GB");

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            AuthenticationBuilder.AddSocialAuth();
            services.AddScheduledTasks();
            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Olive API", Version = "v1" });
                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (apiDesc.HttpMethod == null) return false;
                    var a = apiDesc.SupportedResponseTypes;
                    if (a.Any())
                    {
                        return true;
                    }
                    return false;
                });
                
            });
        }

        public override void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseWebTest(ReferenceData.Create, config => config.AddTasks());
            base.Configure(app, env);

            //app.UseStaticFiles();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Olive API V1");
            });

            if (Config.Get<bool>("Automated.Tasks:Enabled"))
                app.UseScheduledTasks(TaskManager.Run);
        }
    }
}
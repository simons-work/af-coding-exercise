using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Reflection;
using Web.Api.Core;
using Web.Api.Core.Models;
using Web.Api.Infrastructure;

namespace Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services
                .AddSwaggerGen(GetSwaggerGeneratorOptions())
                .AddConnectionProvider(Configuration)
                .AddRepositories()
                .AddServices()
                .AddMvc()
                .AddFluentValidation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(GetSwaggerUIOptions());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private Action<SwaggerUIOptions> GetSwaggerUIOptions()
        {
            return endpoints =>
            {
                endpoints.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API V1");
                endpoints.RoutePrefix = "";
            };
        }

        private Action<SwaggerGenOptions> GetSwaggerGeneratorOptions()
        {
            string[] xmlCommentsFiles = new[]
            {
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml",
                $"{Assembly.GetAssembly(typeof(CustomerDto))?.GetName().Name}.xml"
            };

            return (setupAction) =>
            {
                foreach (var file in xmlCommentsFiles)
                {
                    string xmlCommentsFilePath = Path.Combine(AppContext.BaseDirectory, file);
                    setupAction.IncludeXmlComments(xmlCommentsFilePath);
                }
            };
        }
    }
}

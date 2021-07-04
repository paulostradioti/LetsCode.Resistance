using System;
using LetsCode.Resistance.Infrastructure;
using LetsCode.Resistance.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;

namespace LetsCode.Resistance.API
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "LetsCode Resistance Social Network API",
                        Version = "v1",
                        Contact = new OpenApiContact
                        {
                            Email = "paulo@paulo.eti.br",
                            Name = "Paulo Ricardo Stradioti",
                            Url = new Uri("https://github.com/paulostradioti/LetsCode.Resistance")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "MIT"
                        }
                    });
                c.EnableAnnotations();
                c.UseInlineDefinitionsForEnums();
            });

            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()));
            services.AddSwaggerGenNewtonsoftSupport();

            services.AddDatabaseServices();
            services.RegisterServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "LetsCode Resistance Social Network API v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using var scope = app.ApplicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetService<AppDbContext>();
            ServicesExtensions.SeedDatabase(context);
        }
    }
}
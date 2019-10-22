using AFFHA.API.Application.ExternalServices;
using AFFHA.API.Infrastructure.Extensions;
using AFFHA.API.Infrastructure.ServiceExtension;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Reflection;

namespace AFFHA.API
{
    public class Bootstrap
    {
        [Obsolete]
        public Bootstrap(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        [Obsolete]
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache()
                .AddNpgsqlService(Configuration)
                .AddSwaggerService()
                .AddCors()
                .AddControllers();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(60);
            });

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
            });
            Assembly[] assemblies = { this.GetType().Assembly };
            services.AddAutoMapper(assemblies:assemblies);

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            // Needs following to get current user information outside controller
            // because IHttpContextAccessor is no longer registered by default.
            // Used to save 'CreatedBy' and 'ModifiedBy' information.
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMediatR(typeof(Bootstrap));
            services.AddAFFHARepositories();
            services.AddTransient<IWeatherService, WeatherService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            var pathBase = Configuration["PATH_BASE"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                loggerFactory.CreateLogger<Bootstrap>().LogDebug("Using PATH BASE '{pathBase}'", pathBase);
                app.UsePathBase(pathBase);
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder => {
                    builder.Run(async context => {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c =>
            {
                var prefix = Configuration.GetValue("AppSettings:PublicPath", "")
                    .TrimStart('/')
                    .TrimEnd('/');

                if (!string.IsNullOrEmpty(prefix))
                {
                    prefix = prefix + "/";
                }

                c.RouteTemplate = $"/{prefix}swagger/{{documentName}}/swagger.json";
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                var prefix = Configuration.GetValue("AppSettings:PublicPath", "")
                    .TrimStart('/')
                    .TrimEnd('/');

                if (!string.IsNullOrEmpty(prefix))
                {
                    prefix = prefix + "/";
                }

                c.SwaggerEndpoint($"/{prefix}swagger/v1/swagger.json", "HOPE Service API v1");
                c.RoutePrefix = $"{prefix}swagger"; // starting slash is no-no
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

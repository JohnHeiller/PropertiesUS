using System;
using System.IO;
using System.Reflection;
using API.PropertiesUS.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace API.PropertiesUS
{
    /// <summary>
    /// Base class for Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor method of the class
        /// </summary>
        /// <param name="configuration">Object of type IConfiguration</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Object of type IConfiguration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Object of type IServiceCollection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region BDConnection
            var connectionString = Configuration.GetConnectionString("APIConnection");
            services.AddDbContext<DbContextPropertiesUS>(config =>
            {
                config.UseSqlServer(connectionString);
            });
            #endregion

            #region Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Properties US API", Version = "v1", Description = "API developed as a technical test for Weelo" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.EnableAnnotations();
                options.CustomSchemaIds(type => type.ToString());
            });
            #endregion
        }

        /// <summary>
        /// Method to configure the HTTP request pipeline
        /// </summary>
        /// <param name="app">Object of type IApplicationBuilder</param>
        /// <param name="env">Object of type IWebHostEnvironment</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            #region Swagger
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Properties US API V1");
            });
            #endregion
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Hops.Models;
using Hops.Repositories;
using Hops.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using Hops.Services;

namespace Hops
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HopContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddTransient<IConfigurationService>(r => new ConfigurationService(Configuration));

            services.AddScoped<ISqliteRepository, SqliteRepository>();
            services.AddScoped<IMaltRepository, MaltRepository>();
            services.AddScoped<IYeastRepository, YeastRepository>();

            services.AddSingleton<IResultMapper, ResultMapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables()
                .Build();

            app.UseStaticFiles();

            app.UseDeveloperExceptionPage();

            app.UseMvc();
        }
    }
}

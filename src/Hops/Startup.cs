using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Hops.Models;
using Microsoft.Data.Entity;
using Hops.Repositories;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;

namespace Hops
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var path = PlatformServices.Default.Application.ApplicationBasePath;

            services.AddEntityFramework()
                .AddSqlite()
                .AddDbContext<HopContext>(options => options.UseSqlite("Filename=" + Path.Combine(path, "hops.sqlite")));

            services.AddMvc();

            services.AddScoped<IHopRepository, HopRepository>();
            services.AddScoped<ISearchRepository, SearchRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();

            app.UseDeveloperExceptionPage();

            app.UseMvc();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Hops.Models;
using Microsoft.Data.Entity;
using Hops.Repositories;

namespace Hops
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = "Data Source=D:\\Repos\\Hops\\hops.sqlite";

            services.AddEntityFramework()
                .AddSqlite()
                .AddDbContext<HopContext>(options => options.UseSqlite(connection));
                //.AddDbContext<SubstitutionContext>(options => options.UseSqlite(connection));

            services.AddMvc();
            services.AddScoped<IHopRepository, HopRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseIISPlatformHandler();

            app.UseDeveloperExceptionPage();

            app.UseMvc();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}

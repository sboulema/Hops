using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Hops.Models;
using Hops.Repositories;
using Hops.Mappers;

namespace Hops
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HopContext>();

            services.AddMvc();

            services.AddSingleton<ISqliteRepository, SqliteRepository>();
            services.AddSingleton<IMaltRepository, MaltRepository>();
            services.AddSingleton<IYeastRepository, YeastRepository>();

            services.AddSingleton<IResultMapper, ResultMapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();

            app.UseDeveloperExceptionPage();

            app.UseMvc();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boyner.Config.Domain.Repository;
using Boyner.Config.Infrastructure;
using Boyner.Config.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Boyner.Config.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Boyner.Config.Common.Settings>(options =>
            {
                options.ConnectionString = Configuration.GetConnectionString("ConfigDB");
                options.Database = Configuration.GetSection("DatabaseNames:ConfigDB").Value;
            });

            services.AddSingleton<MongoDbContext>();

            services.AddSingleton<ConfigRepository>();
            services.AddSingleton<ConfigService>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

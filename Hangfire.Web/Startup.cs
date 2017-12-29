using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hangfire.Console;

namespace Hangfire.Web
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
            services.AddMvc();
            services.AddHangfire(config =>
            {
                config.UseConsole();
                //config.UseRedisStorage(Redis);
                config.UseSqlServerStorage(Configuration.GetConnectionString("Hangfire"));//"Data Source=.;Initial Catalog=HangfireDemo;User ID=sa;Password=abcd1234!"
            }).AddHangfireNode();

            GlobalConfiguration.Configuration.UseDashboardMetric(SqlServerStorage.ActiveConnections).UseDashboardMetric(SqlServerStorage.TotalConnections).UseDashboardMetric(DashboardMetrics.FailedCount);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                //wait all jobs performed when BackgroundJobServer shutdown.
                ShutdownTimeout = TimeSpan.FromMinutes(30),
                Queues = new string[] { "default", "io", "net" },
                WorkerCount = Math.Max(Environment.ProcessorCount, 1),
            }).UseHangfireDashboard().UseHangfirePlugin("App_Data/Plugin");

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

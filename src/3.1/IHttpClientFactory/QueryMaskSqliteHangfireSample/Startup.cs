using Hangfire;
using Hangfire.Storage.SQLite;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QueryMaskSample.Models;
using QueryMaskSample.Services;

namespace QueryMaskSample
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
            services.AddScoped<MaskContext>();
            services.AddHttpClient<MaskService>();
            services.AddHangfire(configuration => configuration
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSQLiteStorage());
            services.AddHangfireServer();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBackgroundJobClient backgroundJobs, IRecurringJobManager recurringJob)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseHangfireDashboard();
            // For Hangfire test.
            //BackgroundJob.Enqueue(() => Console.WriteLine("Hello backgroundjobs."));
            //RecurringJob.AddOrUpdate("Hello", () => Console.WriteLine("Hello, recurringJob."), Cron.Minutely);
            backgroundJobs.Enqueue(() => MaskSchedule.InitialDb());
            recurringJob.AddOrUpdate("MaskDataUpdate", () => MaskSchedule.MaskDataUpdate(), "1/10 * * * *");

            app.UseEndpoints(endpoints =>
            {
                // for /hangfire dashboard access
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });
        }
    }
}

using System;
using System.Net.Http;
using HttpClientSample.Handlers;
using HttpClientSample.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;

namespace HttpClientSample
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
            services.AddTransient<ValidateAccessTokenHeaderHandler>();

            services.AddHttpClient();

            // AddHttpMessageHandler
            services.AddHttpClient("blog", b =>
            {
                b.BaseAddress = new Uri("https://blog.kkbruce.net/");
                b.DefaultRequestHeaders.Add("User-Agent", "kkbruce labs");
            }).AddHttpMessageHandler<ValidateAccessTokenHeaderHandler>();

            // No HttpMessageHandler
            //services.AddHttpClient("blog", b =>
            //{
            //    b.BaseAddress = new Uri("https://blog.kkbruce.net/");
            //    b.DefaultRequestHeaders.Add("User-Agent", "kkbruce labs");
            //});

            // Add Polly re-try
            services.AddHttpClient("github", g =>
            {
                g.BaseAddress = new Uri("https://api.github.com/");
                // Two headers required
                g.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                g.DefaultRequestHeaders.Add("User-Agent", "kkbruce labs");
            }).AddTransientHttpErrorPolicy(p =>
                p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(500))
            );

            // No Polly
            //services.AddHttpClient("github", g =>
            //{
            //    g.BaseAddress = new Uri("https://api.github.com/");
            //    // Two headers required
            //    g.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            //    g.DefaultRequestHeaders.Add("User-Agent", "kkbruce labs");
            //});

            services.AddHttpClient("blogApi", c =>
                {
                    c.BaseAddress = new Uri("https://blog.kkbruce.net");
                })
                .AddTypedClient(c => Refit.RestService.For<IBlogApi>(c));

            services.AddHttpClient("long-time")
                    .SetHandlerLifetime(TimeSpan.FromMinutes(10));

            services.AddHttpClient("disable-cookies")
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    return new SocketsHttpHandler()
                    {
                        UseCookies = false,
                    };
                });

            services.AddHttpClient("configured-handler")
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    return new HttpClientHandler()
                    {
                        AllowAutoRedirect = false,
                        UseDefaultCredentials = true
                    };
                });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
        }
    }
}

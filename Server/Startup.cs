using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;

namespace signalrchat
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();

            services.AddHostedService<CountingService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var hub = app.ApplicationServices.GetService<IHubContext<LogHub>>();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.SignalRLogger<LogHub>(hub)
                .WriteTo.Console()
                .CreateLogger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSignalR(config =>
            {
                config.MapHub<ChatHub>("/chat");
                config.MapHub<LogHub>("/log");
            });

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Hello World!");

                await next();
            });

            // app.Run(async context =>
            // {
            //     await context.Response.WriteAsync("Hello World!");
            // });
        }
    }
}

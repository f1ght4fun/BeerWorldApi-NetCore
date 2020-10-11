using BeerWorld.Interfaces;
using BeerWorld.Models;
using BeerWorld.Repo;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BeerWorld
{
    public class Startup
    {
        private readonly ILogger _logger;

        public Startup(ILogger<Startup> logger)
        {
            _logger = logger;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            _logger.LogInformation("Start: Configure Services");

            services.AddCors();
            services.AddResponseCompression();

            services.AddMemoryCache();
            services.AddScoped(typeof(IBeerRepository), typeof(BeerRepository));

            services.AddControllers();

            _logger.LogInformation("End: Configure Services");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            _logger.LogInformation("Start: Configure App");

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseCors(options =>
                options
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseResponseCompression();

            app.UseRouting();

            app.UseEndpoints(options => options.MapControllers());

            _logger.LogInformation("End: Configure App");
        }
    }
}

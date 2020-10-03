using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TaxRateScheduler.Context;
using TaxRateScheduler.Repository;
using TaxRateScheduler.Services;

namespace TaxRateScheduler
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
            services.AddDbContext<MunicipalityTaxRateContext>(item =>
            item.UseSqlServer(Configuration.GetConnectionString("sqlconstring")));
            services.AddControllers();
            NewMethod(services);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tax Rate", Version = "v1" });
            });

            static void NewMethod(IServiceCollection services)
            {
                services.AddScoped<ITaxRateRepository, TaxRateRepository>();
                services.AddScoped<IFileProcessService, FileProcessService>();
                services.AddScoped<ITaxRateService, TaxRateService>();
                services.AddScoped<ITaxAddService, TaxAddService>();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tax Rate V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

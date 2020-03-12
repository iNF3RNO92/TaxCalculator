using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaxCalculator.API.Models;
using TaxCalculator.API.Models.DataManager;
using TaxCalculator.API.Models.Repository;

namespace TaxCalculator
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
            //services.AddDbContext<ApplicationContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:ApplicationDB"]));
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer("server=.;Database=TaxCalculator; Trusted_Connection=True;"));
            //services.AddScoped(typeof(IDataRepository<TaxType, int>), typeof(TaxTypeManager));
            services.AddScoped<ITaxTypeRepository, TaxTypeRepository>();
            services.AddControllers();
            services.AddMvc();
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

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

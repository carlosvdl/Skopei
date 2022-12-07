using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Skopei.Database;
using Skopei.Services;

namespace Skopei
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
            // Registers all the existing Controllers in the Controllers directory.
            services.AddControllers();
            
            // Registers the existing Services in the Service directory.
            services.AddTransient<UserService>();
            services.AddTransient<ProductService>();

            // Setting the Database name as the Connection string which is in the appsettings.json file.
            // Adding the ApplicationContext class as the DbContext with MySQL.
            var connectionString = Configuration.GetConnectionString("Skopei");
            services.AddDbContext<ApplicationContext>(builder =>
            {
                builder.UseMySql(connectionString);
            });
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

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}

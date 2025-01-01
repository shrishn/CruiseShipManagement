using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using CruiseShipManagement.DataAccess; // Reference the namespace of the context class

using log4net;
using log4net.Config;
using System.IO;
using Microsoft.AspNetCore.Identity;
using CruiseShipManagement.Models;

namespace CruiseShipManagement
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;

            // Configure Log4net
            var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo(_configuration["Logging:Log4Net:ConfigFileName"]));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add DbContext using the connection string from appsettings.json
            services.AddDbContext<CruiseShipContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            // Add Identity services with ApplicationUser and IdentityRole
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<CruiseShipContext>()
                    .AddDefaultTokenProviders();

            // Add MVC services for Controllers and Views
            services.AddControllersWithViews();

            // Add Razor Pages services
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseRouting();

            // Add authentication middleware before authorization
            app.UseAuthentication(); // This is required for handling user authentication
            app.UseAuthorization();   // Authorization middleware

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}

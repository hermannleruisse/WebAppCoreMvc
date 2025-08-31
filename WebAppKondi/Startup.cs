using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using WebAppCoreMVC.Helpers;
using WebAppCoreMVC.Models;
using WebAppCoreMVC.Services;
using BCryptNet = BCrypt.Net.BCrypt;

namespace WebAppCoreMVC
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
            services.AddControllersWithViews();
            services.AddDbContext<ApplicationDbContext>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAboutService, AboutService>();
            //services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            //app.UseSession();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            createTestUsers(context);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "admin",
                    areaName: "ADMIN",
                    pattern: "ADMIN/{controller=Dashboard}/{action=Index}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");

            });
        }

        private void createTestUsers(ApplicationDbContext context)
        {
            // add hardcoded test users to db on startup
            var testUsers = new List<User>
            {
                new User { FirstName = "Admin", LastName = "User", Username = "admin", Password = BCryptNet.HashPassword("admin"), Role = Role.Admin },
                new User { FirstName = "Normal", LastName = "User", Username = "user", Password = BCryptNet.HashPassword("user"), Role = Role.User }
            };

            context.Users.AddRange(testUsers);
            context.SaveChanges();
        }
    }
}

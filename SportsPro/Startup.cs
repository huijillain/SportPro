using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.EntityFrameworkCore;
using SportsPro.Models;
using Microsoft.AspNetCore.Identity;

namespace SportsPro
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //adding Identity services
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<SportsProContext>();

            //part5, Session state for Tech ID
            services.AddMemoryCache();
            services.AddSession();
            services.AddControllersWithViews();

            // Unit testing
            services.AddHttpContextAccessor();

            services.AddDbContext<SportsProContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("SportsProContext")));

            services.AddRouting(options => {
                options.LowercaseUrls = true;
                options.AppendTrailingSlash = true;
            });

            //services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
            //services.AddTransient(typeof(IGRepository<>), typeof(GRepository<>));
        }

        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            //});

            app.UseSession();  // must be called before UseEndpoints()
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
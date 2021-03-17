using Domaine.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository.Implementation;
using Repository.Interfaces;
using Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourMe.Data;

namespace WebApplication2
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddDbContext<TourMeContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("myconn")));
            services.AddIdentity<Utilisateur, IdentityRole>().AddEntityFrameworkStores<TourMeContext>();
            services.AddControllersWithViews();
            services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = "886221468838215";
                options.AppSecret = "33c5819cb80ae5e5ab64b9043bbf9b9b";


            });
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //sprint 1
            //add Repositories 
            services.AddScoped(typeof(IUserRepo), typeof(UserRepo));
            //sprint 1 
            // add services 
            services.AddTransient<IUserService, UserService>();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

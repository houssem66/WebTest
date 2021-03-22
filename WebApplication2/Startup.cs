using Domaine.Entities;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.OAuth;
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
using System.Security.Claims;
using System.Threading.Tasks;
using TourMe.Data;
using TourMe.Web;
using Twilio;

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
            TwilioClient.Init("AC57fc209fe337678b3790258f07270630", "54cdda9763c43829eed0c2a660b98d88");
            services.AddSingleton<CountryService>();
            services.AddControllersWithViews();
            services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = "886221468838215";
                options.AppSecret = "33c5819cb80ae5e5ab64b9043bbf9b9b";
                options.Scope.Add("public_profile");

                options.Fields.Add("picture");
                options.Events = new OAuthEvents
                {
                    OnCreatingTicket = context =>
                    {
                        var identity = (ClaimsIdentity)context.Principal.Identity;
                        var profileImg = context.User.GetProperty("picture").GetProperty("data").GetProperty("url").ToString();
                        identity.AddClaim(new Claim(JwtClaimTypes.Picture, profileImg));
                        return Task.CompletedTask;
                    }
                };


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

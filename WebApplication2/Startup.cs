using Domaine.Entities;
using MailKit;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using Repository.Implementation;
using Repository.Interfaces;
using Services.Implementation;
using Services.Interfaces;
using System.Net;
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
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));

            });
           
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
                options.HttpsPort = 465;
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CreateExperiencePolicy",
                policy => policy.RequireClaim("Create Experience"));
                options.AddPolicy("EditExperiencePolicy",
                policy => policy.RequireClaim("Edit Experience"));    
                options.AddPolicy("DeleteExperiencePolicy",
                policy => policy.RequireClaim("Delete Experience"));
                options.AddPolicy("EditProfilPolicy",
                policy => policy.RequireClaim("Edit Profil"));
                options.AddPolicy("SuperUserPolicy",
             policy => policy.RequireRole("Utilisateur").RequireAuthenticatedUser().RequireClaim("Create Experience").RequireClaim("Edit Experience")
             .RequireClaim("Delete Experience")
             
             );



            });
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
           
            var mailKilOptions = Configuration.GetSection("Email").Get<MailKitOptions>();
            services.AddMailKit(config=> { 
               
                config.UseMailKit(mailKilOptions);
             });
           
            services.AddDbContext<TourMeContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("myconn")));
            services.AddIdentity<Utilisateur, IdentityRole>(options=>
            {
                options.SignIn.RequireConfirmedEmail = true;

            }
            
            ).AddEntityFrameworkStores<TourMeContext>()
             .AddDefaultTokenProviders();
            
            TwilioClient.Init("AC57fc209fe337678b3790258f07270630", "54cdda9763c43829eed0c2a660b98d88");
            services.AddSingleton<CountryService>();
            services.AddControllersWithViews();
            services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = "886221468838215";
                options.AppSecret = "33c5819cb80ae5e5ab64b9043bbf9b9b";
                //options.Scope.Add("public_profile");

                //options.Fields.Add("picture");


            }).AddGoogle(options =>
            {
                options.ClientId = "472485709376-6kroqnpbdfmu8gp9k2dk1k3f6glhlcgl.apps.googleusercontent.com";
                options.ClientSecret = "1Egum6FjRrIJzxv3W_d7zchi";
            });
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //sprint 1
            //add Repositories 
            services.AddScoped(typeof(IUserRepo), typeof(UserRepo));
            //sprint 1 
            // add services 
            services.AddTransient<IUserService, UserService>();
            //sprint 2
            //add Repositories 
            services.AddScoped(typeof(IExperienceRepo), typeof(ExperienceRepo));
            services.AddScoped(typeof(IActiviteRepo), typeof(ActiviteRepo));
            services.AddScoped(typeof(IRatingRepo), typeof(RatingRepo));
            // add services
            services.AddTransient<IExperienceService, ExperienceService>();
            services.AddTransient<IActiviteService, ActiviteService>();
            services.AddTransient<IRatingService, RatingService>();

            services.AddCors();
            //sprint 3
            // add Repositories
            services.AddScoped(typeof(ILogementextRepo), typeof(LogementextRepo));
            services.AddScoped(typeof(IFournisseurRepo), typeof(FournisseurRepo));
            services.AddScoped(typeof(ILogementRepo), typeof(LogementRepo));
            services.AddScoped(typeof(INourritureRepo), typeof(NourritureRepo));
            services.AddScoped(typeof(ICommercantRepo), typeof(CommercantRepo));
            services.AddScoped(typeof(INourritureExtRepo), typeof(NourritureExtRepo));
            services.AddScoped(typeof(IReservationRepo), typeof(ReservationRepo));
            services.AddScoped(typeof(ITransportRepo), typeof(TransportRepo));
            services.AddScoped(typeof(ITransportExtRepo), typeof(TransportExtRepo));
            //add Services
            services.AddTransient<INourritureService, NourritureService>();
            services.AddTransient<ILogementService, LogementService>();
            services.AddTransient<ILogementextService, LogementextService>();
            services.AddTransient<IFournisseurService, FournisseurService>();
            services.AddTransient<ICommercantService, CommercantService>();
            services.AddTransient<INourritureExtService, NourritureExtService>();
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<ITransportService, TransportService>();
            services.AddTransient<ITransportExtService, TransportExtService>();
     
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

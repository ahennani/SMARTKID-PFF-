using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SMARTKID.Models;
using SMARTKID.Models.Repositories;
using SMARTKID.App_Data;
using SMARTKID.Models.Entities;

namespace SMARTKID
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("SmartKidDb")));

            services.AddScoped<AppDbContext>();
            services.AddScoped<ISchoolRepository<Kid>, KidRepository>();

            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 6;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireDigit = true;
                opt.Password.RequireNonAlphanumeric = false;

                opt.SignIn.RequireConfirmedEmail = false;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc(opt =>
            {
                opt.EnableEndpointRouting = false;

                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

                opt.Filters.Add(new AuthorizeFilter(policy));
            });

            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = "/Account/SignIn";
            });

            services.AddAuthentication()
                           .AddGoogle(opt =>
                           {
                               opt.ClientId = Configuration.GetValue<string>("ExternalKeys:ClientId");
                               opt.ClientSecret = Configuration.GetValue<string>("ExternalKeys:ClientSecret");
                           })
                           .AddFacebook(opt =>
                           {
                               opt.AppId = Configuration.GetValue<string>("ExternalKeys:AppId");
                               opt.AppSecret = Configuration.GetValue<string>("ExternalKeys:AppSecret");
                           })
                           .AddTwitter(opt =>
                           {
                               opt.ConsumerKey = Configuration.GetValue<string>("ExternalKeys:ConsumerKey");
                               opt.ConsumerSecret = Configuration.GetValue<string>("ExternalKeys:ConsumerSecret");
                               opt.RetrieveUserDetails = true;
                           });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMvc(route =>
            {
                route.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });
        }
    }
}

using FutbolowaJaskinia.Data;
using FutbolowaJaskinia.Data.Security;
using FutbolowaJaskinia.Hubs;
using FutbolowaJaskinia.Models;
using FutbolowaJaskinia.Utilities;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace FutbolowaJaskinia
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UtiDbContext>(opts =>
            {

                opts.UseNpgsql(Configuration.GetConnectionString("UtiConn"));
            });

            services.AddDbContext<AppDbContext>(opts =>
            {
                opts.UseNpgsql(Configuration.GetConnectionString("IdentityConn"));
            })
            .AddIdentity<ApplicationUser, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 8;
                opts.SignIn.RequireConfirmedEmail = true;
                opts.Lockout.MaxFailedAccessAttempts = 5;
                opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("AdminPolicy", policyOpts =>
                {
                    policyOpts.AddRequirements(new AdminRequirement());
                });
            });

            services.AddControllersWithViews();

            services.AddCors();
            services.AddSignalR();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IAuthorizationHandler, AdminAuthHandler>();

            services.AddHangfire(opts => opts.UsePostgreSqlStorage(Configuration.GetConnectionString("HangfireConn")));

            services.AddScoped<IEmailSender, FluentEmailSender>();
            services.AddScoped<ApiAccess>();

            services.AddScoped<IUtiRepo, SqlUtiRepo>();
            services.AddScoped<HangfireJobs>();

            services.AddProgressiveWebApp();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, HangfireJobs jobs)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHangfireDashboard("/dash");
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/Home/ErrorCode/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(opts =>
            {
                opts.AllowAnyOrigin().AllowAnyMethod();
            });


            app.UseHangfireServer();

            jobs.HighlightsJob();
            app.UseSerilogRequestLogging();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<MainHub>("/main");
                endpoints.MapHub<ChatHub>("/chat");
            });
        }
    }
}

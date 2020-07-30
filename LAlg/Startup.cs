using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using LAlg.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TelegramBot;
using VkBot;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace LAlg
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
            services.AddSingleton(bot =>
            {
                TelegramClient telegramClient = new TelegramClient(
                    Configuration.GetConnectionString("DefaultConnection"),
                    Configuration.GetSection("BotsTokens").GetSection("Telegram").Value);
                return telegramClient;
            });
            services.AddSingleton(bot =>
            {
                BotVk botVk = new BotVk(
                    Configuration.GetConnectionString("DefaultConnection"),
                    Configuration.GetSection("BotsTokens").GetSection("Vk").Value,
                    Configuration.GetSection("BotsTokens").GetSection("VkCode").Value);
                return botVk;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var builder = new NpgsqlDbContextOptionsBuilder(options);
                builder.SetPostgresVersion(new Version(9, 5));
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<BotAppData.BotAppContext>(options =>
            {
                var builder = new NpgsqlDbContextOptionsBuilder(options);
                builder.SetPostgresVersion(new Version(9, 5));
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection"));
            });

            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseNpgsql(
            //        Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddRazorPages();
            services.AddHostedService<TimedHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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
                endpoints.MapRazorPages();
            });
        }
    }
}

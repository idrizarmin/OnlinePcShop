using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PCWebShop.Data;

using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using PCWebShop.Extensions;
using Hangfire;
using Hangfire.MemoryStorage;
using PCWebShop.Core.Interfaces;
using PCWebShop.Core.Services;
using PCWebShop.Database;
using Microsoft.AspNetCore.Identity;

namespace PCWebShop
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

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p =>
                {
                    p.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                   
                    
                });
            });
            

            services.AddDbContext<Context>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("connectionpc")));


            

            var origins = Configuration.GetSection("AllowedDomains").Value;
            
            services.AddCors(options =>
            {
                options.AddPolicy("AppPolicy",
                builder =>
                {
                    builder.WithOrigins(origins)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                   
                });
            });

            services.AddSingleton<IEmailSender, EmailSender>();
            //Dependecy injection
            services.ConfigureServices(Configuration);

            services.AddIdentity<IdentityUser, IdentityRole>(opt =>
            {
                
            }
           ).AddEntityFrameworkStores<Context>().AddDefaultTokenProviders();


            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();


            //HANGFIRE CONFIG
            services.AddHangfire(config =>
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeSerializer()
                .UseMemoryStorage());

            services.AddHangfireServer();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IBackgroundJobClient backgroundJobClient, //Hangfire
            IRecurringJobManager recurringJobManager, //Hangfire recurring jobs
            IServiceProvider serviceProvider)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseDefaultFiles();
            app.UseStaticFiles();

           
            app.UseSwagger();

            

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseCors("AppPolicy");

            app.UseCors(
               options => options
               .SetIsOriginAllowed(x => _ = true)
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials()
           ); //This needs to set everything allowed

          

            app.UseHttpsRedirection();


               

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");

               
            });






            app.UseHangfireDashboard();

            //Client hangfire
            recurringJobManager.AddOrUpdate(
          "Run at 00:10 every day//CreateBirthdayNotification",
          () => serviceProvider.GetService<IObavjestService>().CreateBirthdayNotifications(),
          "10 0 * * *"
          );
            recurringJobManager.AddOrUpdate(
         "Run at 00:15 every day//CreateContractExpirationNotification",
         () => serviceProvider.GetService<IObavjestService>().CreateContractExpirationNotification(),
         "15 0 * * *"
         );
        }
    }
}

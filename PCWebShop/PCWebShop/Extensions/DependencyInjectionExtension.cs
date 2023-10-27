using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PCWebShop.Core.Interfaces;
using PCWebShop.Core.Services;
using PCWebShop.Data;
using PCWebShop.Modul0_Autentifikacija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCWebShop.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfig = configuration.GetSection("Jwt").Get<JwtConfiguration>();
            services.AddSingleton(jwtConfig);




           



            // Services DI.
            services.AddLogging();
            services.AddHttpContextAccessor();

            services.AddScoped<IOglasService, OglasService>();
            services.AddScoped<IObavjestService, ObavjestService>();
            services.AddScoped<INarudzbaService, NarudzbaService>();
           
        }
    }
}

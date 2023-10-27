using System.Linq;
using System.Text.Json.Serialization;
using AutoMapper.QueryableExtensions;
using PCWebShop.Data;
using PCWebShop.Modul0_Autentifikacija.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PCWebShop.Database;

namespace PCWebShop.Helper.AutentifikacijaAutorizacija
{
    public static class MyAuthTokenExtension
    {
        public class LoginInformacije
        {
            public LoginInformacije(AutentifikacijaToken autentifikacijaToken)
            {
                this.autentifikacijaToken = autentifikacijaToken;
            }

            [JsonIgnore]
            public KorisnickiNalog korisnickiNalog => autentifikacijaToken?.korisnickiNalog;
            public AutentifikacijaToken autentifikacijaToken { get; set; }
            
            public bool isLogiran => korisnickiNalog != null;
            public bool isPermisijaAdmin => isLogiran && ( korisnickiNalog.isAdmin ||korisnickiNalog.administrator!=null);
            public bool isPermisijaKorisnik => isLogiran && (korisnickiNalog.isKupac ||korisnickiNalog.korisnik!=null);

        }


        public static LoginInformacije GetLoginInfo(this HttpContext httpContext)
        {
            var token = httpContext.GetAuthToken();

            return new LoginInformacije(token);
        }
    
        public static AutentifikacijaToken GetAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.GetMyAuthToken();

            Context db = httpContext.RequestServices.GetService<Context>();
            AutentifikacijaToken korisnickiNalog = db.AutentifikacijaToken
                .Include(s=>s.korisnickiNalog)
                .SingleOrDefault(x => token != null && x.vrijednost == token);
            
            return korisnickiNalog;
        }

        public static string GetMyAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["autentifikacija-token"];
            return token;
        }
    }
}

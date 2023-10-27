using System;
using System.Collections.Generic;
using System.Linq;
using PCWebShop.Data;
using PCWebShop.Helper;
using PCWebShop.Helper.AutentifikacijaAutorizacija;
using PCWebShop.Modul0_Autentifikacija.Models;
using PCWebShop.Modul0_Autentifikacija.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static PCWebShop.Helper.AutentifikacijaAutorizacija.MyAuthTokenExtension;
using PCWebShop.Database;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using IdentityServer4.Models;
using System.Threading;
using PCWebShop.Core.Infrastructure.Enums;

namespace PCWebShop.Modul0_Autentifikacija.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class AutentifikacijaController : ControllerBase
    {
        private readonly Context _dbContext;
      
        public AutentifikacijaController(Context dbContext)
        {
            this._dbContext = dbContext;
        }

        
      


        [HttpPost]
        public ActionResult<LoginInformacije> Login([FromBody] LoginVM x)
        {
            //1- provjera logina
            KorisnickiNalog logiraniKorisnik = _dbContext.KorisnickiNalog
                .FirstOrDefault(k =>
                k.korisnickoIme != null && k.korisnickoIme == x.korisnickoIme && k.lozinka == x.lozinka);

            var korisnik = _dbContext.Korisnik.FirstOrDefault(a => a.korisnickoIme == x.korisnickoIme);

            if (korisnik.ConfirmedEmail == false)
                logiraniKorisnik = null;
          



            if (logiraniKorisnik == null)
            {
                //pogresan username i password
                return new LoginInformacije(null);
            }
            


            //2- generisati random string
            string randomString = TokenGenerator.Generate(10);

            //3- dodati novi zapis u tabelu AutentifikacijaToken za logiraniKorisnikId i randomString
            var noviToken = new AutentifikacijaToken()
            {
                ipAdresa = Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                vrijednost = randomString,
                korisnickiNalog = logiraniKorisnik,
                vrijemeEvidentiranja = DateTime.Now
            };

            _dbContext.Add(noviToken);
            _dbContext.SaveChanges();

            //4- vratiti token string
            return new LoginInformacije(noviToken);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            AutentifikacijaToken autentifikacijaToken = HttpContext.GetAuthToken();

            if (autentifikacijaToken == null)
                return Ok();

            _dbContext.Remove(autentifikacijaToken);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public ActionResult<AutentifikacijaToken> Get()
        {
            AutentifikacijaToken autentifikacijaToken = HttpContext.GetAuthToken();

            return autentifikacijaToken;
        }
    }
}
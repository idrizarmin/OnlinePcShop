using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PCWebShop.Core.Interfaces;
using PCWebShop.Data;
using PCWebShop.Database;
using PCWebShop.Helper;
using PCWebShop.Helper.AutentifikacijaAutorizacija;
using PCWebShop.ViewModels;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace PCWebShop.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class KorisnikController : ControllerBase
    {
        private readonly Context _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IEmailSender _emailSender;
        public KorisnikController(Context context, UserManager<IdentityUser> userManager, IConfiguration config, IEmailSender emailSender)
        {
            this._context = context;
            _emailSender = emailSender;
            _userManager = userManager;
            _config = config;
        }

        [HttpGet]
        public List<KorisnikVM> GetAll(string ime_prezime)
        {
            //if (!HttpContext.GetLoginInfo().isLogiran)
            //    return BadRequest("Nije logiran");

            var data = _context.Korisnik.OrderBy(s => s.id)
                .Where(x => ime_prezime == null || (x.Ime + " " + x.Prezime).StartsWith(ime_prezime) || (x.Ime + " " + x.Prezime).StartsWith(ime_prezime))
               .Select(s => new KorisnikVM()
               {
                   ID = s.id,
                   Ime = s.Ime,
                   DatumRodjenja = s.DatumRodjenja,
                   KorisnickoIme = s.korisnickoIme,
                   drzava = s.Drzava,
                   DrzavaID = s.DrzavaID,
                   Pretplacen = s.Pretplacen,
                   Prezime = s.Prezime,
                   Email=s.Email,
                   Spol = s.Spol,
                   Lozinka=s.lozinka,
                   Adresa1=s.Adresa1,
                   Adresa2=s.Adresa2
                   
               }).AsQueryable();
            return data.Take(100).ToList();
        }

        [HttpGet("{id}")]
        public  ActionResult Get(int id)
        {
            return Ok(_context.Korisnik.FirstOrDefault(k => k.id == id));
        }

        [HttpPost]
        public  ActionResult Add([FromBody] KorisnikAddVM k)
        {
            if (k == null || !ModelState.IsValid)
                return BadRequest();
            try
            {
                var newKorisnik = new Korisnik
                {
                    DrzavaID = k.DrzavaID,
                    Ime = k.Ime,
                    Prezime = k.Prezime,
                    korisnickoIme = k.korisnickoIme,
                    DatumRodjenja = k.DatumRodjenja,
                    Spol = k.Spol,
                    lozinka = k.Lozinka,
                    Pretplacen = true,
                    Email=k.Email
                    
                };
               
               
                string token = TokenGenerator.Generate(100);

                newKorisnik.UserToken = token;

                _context.Add(newKorisnik);
                _context.SaveChanges();




                var uriBuilder = new UriBuilder(_config["ReturnPaths:ConfirmEmail"]);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["token"] = token.ToString();
                query["userid"] = newKorisnik.id.ToString();
                uriBuilder.Query = query.ToString();

                var urlString = "<html><body><p>Molimo vas da potvrdite vašu email adresu.</p><a href='"+ uriBuilder.ToString() + "'>" + token + "  </ a > <br> <p>Vaš PCShop.</p></ body ></ html > ";


                var senderEmail = _config["ReturnPaths:SenderEmail"];

                  _emailSender.SendEmailAsync(senderEmail, newKorisnik.Email, "Confirm your email address", urlString);

                




                return Get(newKorisnik.id);
            }
            catch (Exception)
            {

                throw;
            }
           
            
        }


        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] KorisnikUpdateVM x)
        {
            Korisnik korisnik = _context.Korisnik.Where(k =>k.id ==id ).FirstOrDefault(s => s.id == id);

            if (korisnik == null)
                return BadRequest("pogresan ID");
            korisnik.LokacijaSlike = x.LokacijaSlike;
            korisnik.Ime = x.Ime;
            korisnik.Prezime = x.Prezime;
            korisnik.korisnickoIme = x.korisnickoIme;
            korisnik.Pretplacen = x.Pretplacen;          
            korisnik.DatumRodjenja = x.DatumRodjenja;
            korisnik.DrzavaID = x.DrzavaID;
            korisnik.Spol = x.Spol;
            korisnik.Email = x.Email;
            korisnik.Adresa1 = x.Adresa1;
            korisnik.Adresa2 = x.Adresa2;

            _context.SaveChanges();
            return Get(id);
        }

        [HttpPost]
        public  ActionResult ConfirmEmail(ConfirmEmailVM model)
        {
            int id = int.Parse(model.UserId);
            var user = _context.Korisnik.FirstOrDefault(x => x.id == id   &&  x.UserToken == model.Token);

            
            if(user == null)
            {
                return BadRequest();
            }
            
           

            user.ConfirmedEmail = true;
            
            _context.SaveChanges();
            return Ok();
        }


        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            Korisnik korisnik = _context.Korisnik.Find(id);

            if (korisnik == null  )
                return BadRequest("pogresan ID");

            _context.Remove(korisnik);

            _context.SaveChanges();
            return Ok(korisnik);
        }
    }
}

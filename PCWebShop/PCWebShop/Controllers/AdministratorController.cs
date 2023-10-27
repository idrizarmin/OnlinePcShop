using Microsoft.AspNetCore.Mvc;
using PCWebShop.Data;
using PCWebShop.Database;
using PCWebShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCWebShop.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AdministratorController:ControllerBase
    {
        private readonly Context _context;

        public AdministratorController(Context context)
        {
            this._context = context;
        }

        [HttpGet]
        public List<AdministratorVM> GetAll(string ime_prezime)
        {
            //if (!HttpContext.GetLoginInfo().isLogiran)
            //    return Forbid();

            
            var data = _context.Administrator.OrderBy(s => s.id)
                .Where(x=> ime_prezime == null ||(x.Ime+ " "+ x.Prezime).StartsWith(ime_prezime)|| (x.Ime + " " + x.Prezime).StartsWith(ime_prezime))
               .Select(s => new AdministratorVM()
               {
                   ID = s.id,
                   Ime = s.Ime,
                   DatumRodjenja = s.DatumRodjenja,
                   KorisnickoIme = s.korisnickoIme,
                   DrzavaID = s.DrzavaID,
                   Prezime=s.Prezime,
                   Spol=s.Spol,
                   trajanjeUgovora=s.trajanjeUgovora
               }).AsQueryable();

            return data.Take(100).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_context.Administrator.FirstOrDefault(k => k.id == id)); 
        }

        [HttpPost]
        public ActionResult Add([FromBody] AdministratorAddVM k) {
            var newAdministrator = new Administrator
            {
                Ime = k.Ime,
                Prezime = k.Prezime,
                korisnickoIme = k.korisnickoIme,
                DatumRodjenja = k.DatumRodjenja,
                Spol = k.Spol,
                DrzavaID = k.DrzavaID,
                lozinka=k.Lozinka,
                trajanjeUgovora=k.trajanjeUgovora
            };
            _context.Add(newAdministrator);
            _context.SaveChanges();
            return Get(newAdministrator.id);
            
        }
        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] AdministratorUpdateVM x)
        {
            Administrator administrator= _context.Administrator.Where(k =>k.id ==id ).FirstOrDefault(s => s.id == id);

            if (administrator == null)
                return BadRequest("pogresan ID");

            administrator.Ime = x.Ime;
            administrator.Prezime = x.Prezime;
            administrator.korisnickoIme = x.korisnickoIme;
            administrator.lozinka = x.Lozinka;
            administrator.DatumRodjenja = x.DatumRodjenja;
            administrator.DrzavaID = x.DrzavaID;
            administrator.Spol = x.Spol;

            _context.SaveChanges();
            return Get(id);
        }
        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            Administrator administrator = _context.Administrator.Find(id);

            if (administrator == null  )
                return BadRequest("pogresan ID");

            _context.Remove(administrator);

            _context.SaveChanges();
            return Ok(administrator);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using PCWebShop.Data;
using PCWebShop.Database;
using PCWebShop.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace PCWebShop.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DostavljacController : ControllerBase
    {
        private readonly Context _context;

        public DostavljacController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public List<DostavljacVM> GetAll()
        {

            //KorisnickiNalog korisnik = ControllerContext.HttpContext.GetKorisnikOfAuthToken();

            //if (korisnik == null || korisnik is Korisnik)
            //    return null;

            var data = _context.Dostavljac.OrderBy(s => s.ID)
                .Select(s => new DostavljacVM()
                {
                    ID = s.ID,
                    Adresa=s.Adresa,
                    KontaktTelefon=s.KontaktTelefon,
                    NazivDostave=s.NazivDostave,
                    
                }).AsQueryable();

            return data.Take(100).ToList();
        }

        [HttpPost]
        public ActionResult Add([FromBody] DostavljacAddVM x)
        {
            //KorisnickiNalog korisnik = ControllerContext.HttpContext.GetKorisnikOfAuthToken();

            //if (korisnik == null || korisnik is Korisnik)
            //    return null;
            var newDostavljac = new Dostavljac
            {
                Adresa = x.Adresa,
                KontaktTelefon = x.KontaktTelefon,
                NazivDostave = x.NazivDostave,
             
            };

            _context.Add(newDostavljac);
            _context.SaveChanges();
            return Get(newDostavljac.ID);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_context.Dostavljac.FirstOrDefault(p => p.ID== id));
        }
    }
}

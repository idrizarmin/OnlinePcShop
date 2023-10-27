using Microsoft.AspNetCore.Http;
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
    public class KategorijaController : ControllerBase
    {
        private readonly Context _context;

        public KategorijaController(Context context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult Add([FromBody] KategorijaAddVM x)
        {

            var newKategorija = new Kategorija
            {

               NazivKategorije=x.NazivKategorije
            };
            _context.Add(newKategorija);
            _context.SaveChanges();

            return Ok(newKategorija.KategorijaID);

        }
        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            Kategorija kategorija = _context.Kategorija.Find(id);

            if (kategorija == null)
                return BadRequest("pogresan ID");

            _context.Remove(kategorija);

            _context.SaveChanges();
            return Ok(kategorija);
        }
        [HttpGet]
        public List<KategorijaVM> GetAll()
        {

            var data = _context.Kategorija.OrderBy(p => p.KategorijaID)
                .Select(p => new KategorijaVM()
                {
                    KategorijaID=p.KategorijaID,
                    NazivKategorije=p.NazivKategorije
                }).AsQueryable();
            return data.Take(100).ToList();
        }
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_context.Kategorija.FirstOrDefault(k => k.KategorijaID == id));
        }

    }
}

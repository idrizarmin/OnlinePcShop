using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PCWebShop.Database
{
    public class KorisnikOglas
    {
        
        public int ID { get; set; }
        public Korisnik Korisnik { get; set; }
        public int KorisnikID { get; set; }
        public Oglas Oglas { get; set; }
        public int OglasID { get; set; }
        public DateTime DatumPrijave { get; set; }
    }
}

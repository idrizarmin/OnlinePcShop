using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PCWebShop.Database
{
    public class BankovniRacun
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey(nameof(KorisnikID))]
        public Korisnik Korisnik { get; set; }
        public int KorisnikID { get; set; }
        [ForeignKey(nameof(BankaID))]
        public Banka Banka { get; set; }
        public int BankaID { get; set; }
        public string BrojRacuna { get; set; }
        public DateTime DatumAktiviranjaRacuna { get; set; }
        public double StanjeRacuna { get; set; }
    }
}

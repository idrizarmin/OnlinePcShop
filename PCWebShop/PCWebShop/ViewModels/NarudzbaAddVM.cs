using PCWebShop.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace PCWebShop.ViewModels
{
  public  class NarudzbaAddVM
    {
        public DateTime DatumKreiranja { get; set; }
        public int NaruciocID { get; set; }
        public int DostavljacID { get; set; }
        public bool Aktivna { get; set; }
        public bool Potvrdjena { get; set; }
        public Korisnik narucioc { get; set; }
        public Dostavljac dostavljac { get; set; }
    }
}

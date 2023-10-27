using PCWebShop.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PCWebShop.ViewModels
{
    public class ProizvodVM
    {
        public int ProizvodID { get; set; }
        public string NazivProizvoda { get; set; }
        public double Cijena { get; set; }
        public int Kolicina { get; set; }
        public string Opis { get; set; }
        public Kategorija Kategorija { get; set; }
        public int KategorijaID { get; set; }
        public string LokacijaSlike { get; set; }
        public int NaStanju { get; set; }
        public bool Snizen { get; set; } = false;
        public Proizvodjac Proizvodjac { get; set; }
        public int ProizvodjacID { get; set; }
    }
}

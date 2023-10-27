using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PCWebShop.Database
{
    public class Proizvod
    {
        [Key]
        public int ProizvodID { get; set; }
        public string NazivProizvoda { get; set; }
        public double Cijena { get; set; }
        public int Kolicina { get; set; }
        public int NaStanju { get; set; }
        public string Opis { get; set; }
        [ForeignKey(nameof(KategorijaID))]
        public Kategorija Kategorija { get; set; }
        public int KategorijaID { get; set; }
        public string LokacijaSlike { get; set; }
        public bool Snizen { get; set; } = false;
        [ForeignKey(nameof(ProizvodjacID))]
        public Proizvodjac Proizvodjac { get; set; }
        public int ProizvodjacID { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PCWebShop.Database
{
    public class NarudzbaStavka
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey(nameof(PropizvodID))]
        public Proizvod Proizvod { get; set; }
        public int PropizvodID { get; set; }
        [ForeignKey(nameof(NarudzbaID))]
        public Narudzba Narudzba { get; set; }
        public int NarudzbaID { get; set; }
        public int Kolicina { get; set; }
        public double Cijena { get; set; }
    }
}

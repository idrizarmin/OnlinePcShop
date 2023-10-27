using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PCWebShop.Database
{
    public class Recenzija
    {
        [Key]
        public int RecenzijaID { get; set; }
        public int Ocjena { get; set; }
        public string Komentar { get; set; }
        [ForeignKey(nameof(ProizvodId))]
        public Proizvod Proizvod { get; set; }
        public int ProizvodId { get; set; }
    }
}

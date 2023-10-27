using PCWebShop.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PCWebShop.ViewModels
{
    public class ProizvodjacVM
    {
        public int ID { get; set; }
        public string NazivProizvodjaca { get; set; }
        [ForeignKey(nameof(SjedisteID))]
        public Drzava Sjediste { get; set; }
        public int SjedisteID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCWebShop.ViewModels
{
    public class DostavljacVM
    {
        public int ID { get; set; }
        public string NazivDostave { get; set; }
        public string Adresa { get; set; }
        public string KontaktTelefon { get; set; }
        public string Ime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCWebShop.ViewModels
{
    public class AdministratorVM
    {
        public int ID { get; set; }
        public string KorisnickoIme { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Spol { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public int DrzavaID { get; set; }
        public DateTime trajanjeUgovora { get; set; }
    }
}

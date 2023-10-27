using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCWebShop.ViewModels
{
    public class AdministratorUpdateVM
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Spol { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public int DrzavaID { get; set; }
        public string korisnickoIme { get; set; }
        public string Lozinka { get; set; }
    }
}

using PCWebShop.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace PCWebShop.ViewModels
{
    public class KorisnikVM
    {
        public int ID { get; set; }
        public string KorisnickoIme { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Spol { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public int DrzavaID { get; set; }
        public Drzava drzava{ get; set; }
        public bool Pretplacen { get; set; }
        public string Adresa1 { get; set; }
       
        public string Adresa2 { get; set; }
        public string Lozinka { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PCWebShop.ViewModels
{
    public class KorisnikAddVM
    {
        [Required(ErrorMessage = "Obavezan unos polja!")]
        public string korisnickoIme { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        [Required(ErrorMessage = "Obavezan unos polja!")]
        public string Email { get; set; }
        
        public string Spol { get; set; }
        //[Required(ErrorMessage = "Obavezan unos polja!")]
        public DateTime DatumRodjenja { get; set; }
        [Required(ErrorMessage = "Obavezan unos polja!")]
        public int DrzavaID { get; set; }
        [Required(ErrorMessage = "Obavezan unos polja!")]
        public string Lozinka { get; set; }
    }
}

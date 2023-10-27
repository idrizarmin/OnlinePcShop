using System;
using System.Collections.Generic;
using System.Text;

namespace PCWebShop.ViewModels
{
  public  class OglasAddVM
    {
        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }
        public int BrojPozicja { get; set; }
        public string Lokacija { get; set; }
        public DateTime DatumObjave { get; set; } = DateTime.Now;
        public int TrajanjeOglasa { get; set; } = 0;
        public DateTime DatumIsteka { get; set; }
        public int AutorOglasaID { get; set; }
        public bool Aktivan { get; set; } = true;
        public string CVEmail { get; set; }
    }
}

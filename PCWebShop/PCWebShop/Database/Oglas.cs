using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PCWebShop.Database
{
    public class Oglas
    {
        [Key]
        public int ID { get; set; }
        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }
        public int BrojPozicja { get; set; }
        public string Lokacija { get; set; }
        public DateTime DatumObjave { get; set; } = DateTime.Now;
        public int TrajanjeOglasa { get; set; } = 0;
        public DateTime DatumIsteka { get; set; }
        [ForeignKey(nameof(AutorOglasaID))]
        public Administrator AutorOglasa { get; set; }
        public int AutorOglasaID { get; set; }
        public bool Aktivan { get; set; } = true;
        public string CVEmail { get; set; }
    }
}

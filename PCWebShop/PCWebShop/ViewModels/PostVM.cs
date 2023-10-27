using PCWebShop.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PCWebShop.ViewModels
{
    public class PostVM
    {
        public int ID { get; set; }
        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }
        [ForeignKey(nameof(AutorPostaID))]
        public Administrator AutorPosta { get; set; }
        public int AutorPostaID { get; set; }
        public string LokacijaSlike { get; set; }
        public DateTime DatumObjave { get; set; }
    }
}

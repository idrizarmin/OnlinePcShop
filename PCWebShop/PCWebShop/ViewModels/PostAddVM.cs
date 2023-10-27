using PCWebShop.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace PCWebShop.ViewModels
{
    public class PostAddVM
    {
        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }
        public int AutorPostaID { get; set; }
        public string LokacijaSlike { get; set; }
        public DateTime DatumObjave { get; set; }
        public Administrator AutorPosta { get; set; }
    }
}

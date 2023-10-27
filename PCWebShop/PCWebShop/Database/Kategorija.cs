using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCWebShop.Database
{
    public class Kategorija
    {
        [Key]
        public int KategorijaID { get; set; }
        public string NazivKategorije { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCWebShop.Database
{
    public class Banka
    {
        [Key]
        public int Id { get; set; }
        public string NazivBanke { get; set; }
        public string KontaktTel { get; set; }
    }
}

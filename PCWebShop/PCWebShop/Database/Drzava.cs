using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCWebShop.Database
{
    public class Drzava
    {
        [Key]
        public int ID { get; set; }
        public string Naziv { get; set; }
    }
}

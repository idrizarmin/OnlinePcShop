using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCWebShop.Helper.SearchObjects
{
    public class OglasSearchObject : SearchObject
    {
        //Polja po kojima će biti omogućeno filtriranje
        public int? BrojPozicja { get; set; }
    }
}

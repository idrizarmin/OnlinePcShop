
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCWebShop.Helper
{
    public class Config
    {
        public static string AplikacijURL = "http://localhost:51433/";
        public static string Slike => "profile_images/";
        public static string SlikeURL => AplikacijURL + Slike;
        public static string SlikeFolder => "wwwroot/" + Slike;
    }
}

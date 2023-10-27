using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCWebShop.Modul0_Autentifikacija
{
    public class JwtConfiguration
    {
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public int ExpirationAccessTokenInMinutes { get; set; }
        public int ExpirationRefreshTokenInMinutes { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PCWebShop.Modul0_Autentifikacija.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PCWebShop.Helper.AutentifikacijaAutorizacija
{
    public class AutorizacijaAttribute : TypeFilterAttribute
    {
        public AutorizacijaAttribute(bool admin, bool korisnik)
            : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] {  };
        }
    }


    public class MyAuthorizeImpl : IActionFilter
    {
       
        private readonly bool _admin;
        private readonly bool _korisnik;


        public MyAuthorizeImpl(bool admin, bool korisnik)
        {
            
            _admin = admin;
            _korisnik = korisnik;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {


        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (filterContext.HttpContext.GetLoginInfo().isLogiran)
            {
                filterContext.Result = new UnauthorizedResult();
                return;
            }

            KretanjePoSistemu.Save(filterContext.HttpContext);

            if (filterContext.HttpContext.GetLoginInfo().isPermisijaAdmin)
            {
                return;//ok - ima pravo pristupa
            }
            if (filterContext.HttpContext.GetLoginInfo().isPermisijaKorisnik && _korisnik)
            {
                return;//ok - ima pravo pristupa
            }

            //else nema pravo pristupa
            filterContext.Result = new UnauthorizedResult();
        }
    }
}

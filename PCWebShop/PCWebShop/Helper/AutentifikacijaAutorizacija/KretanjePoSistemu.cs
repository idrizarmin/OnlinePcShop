﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PCWebShop.Data;
using PCWebShop.Modul0_Autentifikacija.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using PCWebShop.Database;

namespace PCWebShop.Helper.AutentifikacijaAutorizacija
{
    public class KretanjePoSistemu
    {
        public static int Save(HttpContext httpContext, IExceptionHandlerPathFeature exceptionMessage = null)
        {
            KorisnickiNalog korisnik = httpContext.GetLoginInfo().korisnickiNalog;

            var request = httpContext.Request;

            var queryString = request.Query;

            if (queryString.Count == 0 && !request.HasFormContentType)
                return 0;

            //IHttpRequestFeature feature = request.HttpContext.Features.Get<IHttpRequestFeature>();
            string detalji = "";
            if (request.HasFormContentType)
            {
                foreach (string key in request.Form.Keys)
                {
                    detalji += " | " + key + "=" + request.Form[key];
                }
            }

            var x = new LogKretanjePoSistemu
            {
                korisnik = korisnik,
                vrijeme = DateTime.Now,
                queryPath = request.GetEncodedPathAndQuery(),
                postData = detalji,
                ipAdresa = request.HttpContext.Connection.RemoteIpAddress?.ToString(),
            };

            if (exceptionMessage != null)
            {
                x.isException = true;
                x.exceptionMessage = exceptionMessage.Error.Message + " |" + exceptionMessage.Error.InnerException;
            }

            Context db = httpContext.RequestServices.GetService<Context>();

            db.Add(x);
            db.SaveChanges();

            return x.id;
        }




    }
}

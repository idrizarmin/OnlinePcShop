using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PCWebShop.Core.Infrastructure;
using PCWebShop.Core.Infrastructure.Enums;
using PCWebShop.Core.Interfaces;
using PCWebShop.Data;
using PCWebShop.Database;
using PCWebShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PCWebShop.Core.Services
{
    public class NarudzbaService : INarudzbaService
    {
        private readonly Context _context;

        public NarudzbaService(Context context)
        {
            _context = context;
        }

        public async Task<Message> AddNarudzba(KorpaVM request, CancellationToken cancellationToken)
        {
           
            var status = ExceptionCodeEnum.Success;



            var user =  _context.Korisnik.Where(x=>x.id==request.KorisnikID).FirstOrDefault();
            var order = new Narudzba()
            {
                Aktivna = true,
                DatumKreiranja = DateTime.Now,
                NaruciocID = request.KorisnikID,
                Potvrdjena = false,
                DostavljacID = 1
            };
            var orderResult = await _context.Narudzba.AddAsync(order);
            await _context.SaveChangesAsync();

            for (int i = 0; i < request.ID.Length; i++)
            {
                for (int j = 0; j < request.Kolicina[i]; j++)
                {
                    var stavka = new NarudzbaStavka()
                    {
                        NarudzbaID = orderResult.Entity.ID,
                        PropizvodID = request.ID[i]
                    };
                    await _context.NarudzbaStavka.AddAsync(stavka);
                }
                await _context.SaveChangesAsync();
            }




            return new Message
            {
                IsValid = true,
                Status = ExceptionCodeEnum.Success,
                Info = "USPJEH!!!"
            };
        }
    }
}

using Microsoft.EntityFrameworkCore;
using PCWebShop.Core.Infrastructure;
using PCWebShop.Core.Infrastructure.Enums;
using PCWebShop.Core.Interfaces;
using PCWebShop.Data;
using PCWebShop.Database;
using PCWebShop.Extensions;
using PCWebShop.Helper;
using PCWebShop.Helper.SearchObjects;
using PCWebShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace PCWebShop.Core.Services
{
    public class OglasService : IOglasService
    {
        private readonly Context _context;
        

        public OglasService(Context context)
        {
            _context = context;
        }

        private Expression<Func<Oglas, object>> GetColumnMapSorting(string sortBy)
        {
            return sortBy switch
            {
                "BrojPozicja" => t => t.BrojPozicja,
                "DatumObjave" => t => t.DatumObjave,
                _ => t => t.Naslov,
            };
        }

        //private Expression<Func<Oglas, bool>> GetFilter(OglasSearchObject searchQuery, OglasVM vm)
        //{
        //    string quickSearch = !string.IsNullOrEmpty(searchQuery.QuickSearch)
        //           ? searchQuery.QuickSearch.ToLower()
        //           : string.Empty;


        //    return o => (o.ID == vm.ID)
        //                  && (!searchQuery.BrojPozicja.HasValue || o.BrojPozicja == searchQuery.BrojPozicja);
                          
        //}

        private Expression<Func<Oglas, OglasVM>> GetMapperFromOglasToOglasVM()
        {
            return x => new OglasVM
            {
                Aktivan = x.Aktivan,
                AutorOglasaID = x.AutorOglasaID,
                CVEmail = x.CVEmail,
                BrojPozicja = x.BrojPozicja,
                DatumIsteka = x.DatumIsteka,
                DatumObjave = x.DatumObjave,
                ID = x.ID,
                Lokacija = x.Lokacija,
                Naslov = x.Naslov,
                Sadrzaj = x.Sadrzaj,
                TrajanjeOglasa = x.TrajanjeOglasa,

            };
        }

        public async Task<Message> CreateOglasAsMessageAsync(OglasAddVM x, CancellationToken cancellationToken)
        {

            //KorisnickiNalog korisnik = ControllerContext.HttpContext.GetKorisnikOfAuthToken();

            //if (korisnik == null || korisnik is Korisnik)
            //    return null;

            var status = ExceptionCodeEnum.Success;

            try
            {
                var oglas = new Oglas();


                oglas.Naslov = x.Naslov;
                oglas.Sadrzaj = x.Sadrzaj;
                oglas.Aktivan = x.Aktivan;
                oglas.BrojPozicja = x.BrojPozicja;
                oglas.DatumObjave = x.DatumObjave;
                oglas.DatumIsteka = x.DatumIsteka;
                oglas.Lokacija = x.Lokacija;
                oglas.TrajanjeOglasa = x.TrajanjeOglasa;
                oglas.CVEmail = x.CVEmail;




                await _context.Oglas.AddAsync(oglas);
                    await _context.SaveChangesAsync(cancellationToken);
                
            }
            catch (Exception ex)
            {
                return new Message
                {
                    IsValid = false,
                    Info = ex.Message,
                    Status = status
                };
            }
            return new Message
            {
                Status = ExceptionCodeEnum.Success,
                IsValid = true
            };

        }
        public async Task<Message> GetAlOglasiAsync(SearchObject searchObject, CancellationToken cancellationToken)
        {
            
            
            // Sorting by table columns
          //  var columnsMapSorting = GetColumnMapSorting(searchObject.SortBy);

            var totalItems = await _context.Oglas
                                .Include(x=>x.AutorOglasa)     
                                .CountAsync(cancellationToken);

            var oglasi = await _context.Oglas                    
                 .Select(GetMapperFromOglasToOglasVM())
                 .ApplyPagination(searchObject.Skip(), searchObject.PageSize)
                 .ToListAsync(cancellationToken);

            return new Message
            {
                IsValid = true,
                Status = ExceptionCodeEnum.Success,
                Data = oglasi,
                PagedResult = new PagedResult
                {
                    TotalItems = totalItems,
                    TotalPages = searchObject.CalculatePages(totalItems),
                    CurrentPage = searchObject.Page,
                }
            };
        }
    }
}

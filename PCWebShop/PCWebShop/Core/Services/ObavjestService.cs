
using Microsoft.EntityFrameworkCore;
using PCWebShop.Core.Infrastructure;
using PCWebShop.Core.Infrastructure.Enums;
using PCWebShop.Core.Interfaces;
using PCWebShop.Data;
using PCWebShop.Database;
using System;
using PCWebShop.Helper;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PCWebShop.Helper.AutentifikacijaAutorizacija;
using Microsoft.AspNetCore.Mvc;
using PCWebShop.Helper;
using PCWebShop.Modul0_Autentifikacija.Models;
using System.Linq.Expressions;
using PCWebShop.ViewModels;
using PCWebShop.Extensions;

namespace PCWebShop.Core.Services
{
    public class ObavjestService : IObavjestService   
    {
        private readonly Context _context;

        public ObavjestService(Context context)
        {
            this._context = context;
        }

        private Expression<Func<Obavjest, ObavjestVM>> GetMapperFromObavjestToObavjestiVM()
        {
            return x => new ObavjestVM
            {
                Content = x.Content,
                DateRead = x.DateRead,
                Deleted = x.Deleted,
                ID = x.ID,
                TipObavjesti = x.TipObavjesti,
                Read = x.Read,
                KorisnikId = x.KorisnikId,
                SendOnDate = x.SendOnDate,
                Korisnik=x.Korisnik,
                Seen = x.Seen
            };
        }
        private Expression<Func<AdministratorObavjesti , AdministratorObavjestVM>> GetMapperFromObavjestToAdministratorObavjestiVM()
        {
            return x => new AdministratorObavjestVM
            {
                Content = x.Content,
                DateRead = x.DateRead,
                Deleted = x.Deleted,
                ID = x.ID,
                TipObavjesti = x.TipObavjesti,
                Read = x.Read,
                AdministratorId = x.AdministratorId,
                SendOnDate = x.SendOnDate,
                Administrator = x.Administrator,
                Seen = x.Seen
            };
        }
        #region Hangfire
        public async Task CreateBirthdayNotifications()
        {
            //var clientsRoles = await _context.KorisnickiNalog.Where(x => x.isKupac == true).ToListAsync();

            var clients = await _context.Korisnik.ToListAsync();

            var today = DateTime.Now;
            var hours = new TimeSpan(12, 00, 00);
            today = today.Date + hours; 

            using var transaction = await _context.Database.BeginTransactionAsync();
            {
                try
                {
                    foreach (var client in clients)
                    {
                        if (client.DatumRodjenja.Month == DateTime.Now.Month && client.DatumRodjenja.Day == DateTime.Now.Day)
                        {

                            var obavjest = new Obavjest();
                            obavjest.Content = $"Sretan rođendan želi Vam vaš PcShop";
                            obavjest.SendOnDate = today;
                            obavjest.KorisnikId = client.id;                            
                            obavjest.TipObavjesti = TipObavjesti.App;

                            await _context.AddAsync(obavjest);
                        }
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }


            }

        }
        public async Task CreateContractExpirationNotification()
        {
            //var clientsRoles = await _context.KorisnickiNalog.Where(x => x.isKupac == true).ToListAsync();

            var clients = await _context.Administrator.ToListAsync();

            var today = DateTime.Now;
            var hours = new TimeSpan(12, 00, 00);
            today = today.Date + hours;

            using var transaction = await _context.Database.BeginTransactionAsync();
            {
                try
                {
                    foreach (var client in clients)
                    {
                        if (client.trajanjeUgovora.Month == DateTime.Now.Month && client.trajanjeUgovora.Day == DateTime.Now.Day)
                        {

                            var obavjest = new AdministratorObavjesti();
                            obavjest.Content = $"Admin vam poručuje da date otkaz";
                            obavjest.SendOnDate = today;
                            obavjest.AdministratorId = client.id;
                            obavjest.TipObavjesti = TipObavjesti.App;

                            await _context.AddAsync(obavjest);
                        }
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }


            }

        }
        #endregion

        public async Task<Message> GetObavjestByUserIdAsMessageAsync(int id, CancellationToken cancellationToken)
        {
            var user = _context.Korisnik.Where(x => x.id == id).FirstOrDefault();

           

            var notifications = await _context.Obavjest.Where(x => x.KorisnikId == user.id && !x.Deleted && x.TipObavjesti == TipObavjesti.App && x.SendOnDate < DateTime.Now)
                .Select(GetMapperFromObavjestToObavjestiVM())
                .OrderByDescending(x=>x.ID)
                .ToListAsync(cancellationToken);

           
            return new Message
            {
                Data = notifications,
                Info = "Notifications returned successfully!",
                IsValid = true,
                Status = ExceptionCodeEnum.Success
            };
        }
        public async Task<Message> GetObavjestByAdministratorIdAsMessageAsync(int id, CancellationToken cancellationToken)
        {
            var user = _context.Administrator.Where(x => x.id == id).FirstOrDefault();



            var notifications = await _context.aAdministratorObavjest.Where(x => x.AdministratorId == user.id && !x.Deleted && x.TipObavjesti == TipObavjesti.App && x.SendOnDate < DateTime.Now)
                .Select(GetMapperFromObavjestToAdministratorObavjestiVM())
                .OrderByDescending(x => x.ID)
                .ToListAsync(cancellationToken);


            return new Message
            {
                Data = notifications,
                Info = "Notifications returned successfully!",
                IsValid = true,
                Status = ExceptionCodeEnum.Success
            };
        }

        public async Task<Message> GetUnReadObavjestByUserIdAsMessageAsync(int id, CancellationToken cancellationToken)
        {
            var user = _context.Korisnik.Where(x => x.id == id).FirstOrDefault();



            var notifications = await _context.Obavjest.Where(x => x.KorisnikId == user.id && !x.Deleted && x.TipObavjesti == TipObavjesti.App && x.SendOnDate < DateTime.Now && x.Read==false)
                .Select(GetMapperFromObavjestToObavjestiVM())
                .OrderByDescending(x => x.ID)
                .ToListAsync(cancellationToken);


            return new Message
            {
                Data = notifications,
                Info = "Notifications returned successfully!",
                IsValid = true,
                Status = ExceptionCodeEnum.Success
            };
        }
        public async Task<Message> GetUnReadAdministratorObavjestByUserIdAsMessageAsync(int id, CancellationToken cancellationToken)
        {
            var user = _context.Administrator.Where(x => x.id == id).FirstOrDefault();



            var notifications = await _context.aAdministratorObavjest.Where(x => x.AdministratorId == user.id && !x.Deleted && x.TipObavjesti == TipObavjesti.App && x.SendOnDate < DateTime.Now && x.Read == false)
                .Select(GetMapperFromObavjestToAdministratorObavjestiVM())
                .OrderByDescending(x => x.ID)
                .ToListAsync(cancellationToken);


            return new Message
            {
                Data = notifications,
                Info = "Notifications returned successfully!",
                IsValid = true,
                Status = ExceptionCodeEnum.Success
            };
        }
        public async Task<Message> SetObavjestAsDeletedAsync(int obavjestId, CancellationToken cancellationToken)
        {
            var obavjest = _context.Obavjest.Where(x => x.ID == obavjestId).FirstOrDefault();

            obavjest.Deleted = true;

            _context.SaveChanges();

            return new Message
            {
                Info = "Obavjesti obrisane!",
                IsValid = true,
                Status = ExceptionCodeEnum.Success
            };
        }
        public async Task<Message> SetAdministratorObavjestAsDeletedAsync(int obavjestId, CancellationToken cancellationToken)
        {
            var obavjest =  _context.aAdministratorObavjest.Where(x=>x.ID==obavjestId).FirstOrDefault();

            obavjest.Deleted = true;
            
            _context.SaveChanges();

            return new Message
            {
                Info = "Obavjesti obrisane!",
                IsValid = true,
                Status = ExceptionCodeEnum.Success
            };
        }
        public async Task<Message> SetObavjestAsReadAsMessageAsync(int id, CancellationToken cancellationToken)
        {
            var notifications = await _context.Obavjest.Where(x => x.KorisnikId == id && !x.Read).ToListAsync(cancellationToken);

            using var transaction = await _context.Database.BeginTransactionAsync();
            {
                try
                {
                    for (int i = 0; i < notifications.Count; i++)
                    {
                        notifications[i].Read = true;
                        notifications[i].DateRead = DateTime.Now;
                    }

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                }
                return new Message
                {
                    Info = "Obavjesti pročitane!",
                    IsValid = true,
                    Status = ExceptionCodeEnum.Success
                };
            }

        }
        public async Task<Message> SetAdministratorObavjestAsReadAsMessageAsync(int id, CancellationToken cancellationToken)
        {
            var notifications = await _context.aAdministratorObavjest.Where(x => x.AdministratorId == id && !x.Read).ToListAsync(cancellationToken);

            using var transaction = await _context.Database.BeginTransactionAsync();
            {
                try
                {
                    for (int i = 0; i < notifications.Count; i++)
                    {
                        notifications[i].Read = true;
                        notifications[i].DateRead = DateTime.Now;
                    }

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                }
                return new Message
                {
                    Info = "Obavjesti pročitane!",
                    IsValid = true,
                    Status = ExceptionCodeEnum.Success
                };
            }

        }
    }
}

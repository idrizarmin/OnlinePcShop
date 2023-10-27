using Microsoft.AspNetCore.Mvc;
using PCWebShop.Core.Interfaces;
using PCWebShop.Data;
using PCWebShop.Helper.AutentifikacijaAutorizacija;
using PCWebShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PCWebShop.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ObavjestController : ControllerBase
    {
        private readonly IObavjestService _obavjestService;

        public readonly Context _context;

        public ObavjestController(IObavjestService oglasService, Context context)
        {
            _obavjestService = oglasService;
            _context = context;
        }

        //[HttpGet]
        //public List<ObavjestVM> GetAll()
        //{
        //    var user = HttpContext.GetLoginInfo().korisnickiNalog;
               

        //    var data = _context.Obavjest.OrderByDescending(o=>o.ID)
        //        .Where(o=>o.KorisnikId == user.id)
        //        .Select(s => new ObavjestVM()
        //        {
        //            ID = s.ID,
        //            Content=s.Content,
        //            DateRead=s.DateRead,
        //            Deleted=s.Deleted,
        //            Korisnik=s.Korisnik,
        //            KorisnikId=s.KorisnikId,
        //            Read=s.Read,
        //            Seen=s.Seen,
        //            SendOnDate=s.SendOnDate,
        //            TipObavjesti=s.TipObavjesti
        //        }).AsQueryable();

        //    return data.Take(100).ToList();


        //}
        [HttpGet]
        public async Task<IActionResult> GetUserNotifications(int id, CancellationToken cancellationToken)
        {
            var result = await _obavjestService.GetObavjestByUserIdAsMessageAsync(id, cancellationToken);

            if (!result.IsValid)
                return BadRequest();

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAdministratorNotifications(int id, CancellationToken cancellationToken)
        {
            var result = await _obavjestService.GetObavjestByAdministratorIdAsMessageAsync(id, cancellationToken);

            if (!result.IsValid)
                return BadRequest();

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GeUnReadUserNotifications(int id, CancellationToken cancellationToken)
        {
            var result = await _obavjestService.GetUnReadObavjestByUserIdAsMessageAsync(id, cancellationToken);

            if (!result.IsValid)
                return BadRequest();

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GeUnReadUnAdministratorNotifications(int id, CancellationToken cancellationToken)
        {
            var result = await _obavjestService.GetUnReadAdministratorObavjestByUserIdAsMessageAsync(id, cancellationToken);

            if (!result.IsValid)
                return BadRequest();

            return Ok(result);
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> SetObavjestiAsDeletedAsync(int id, CancellationToken cancellationToken)
        {
            var message = await _obavjestService.SetObavjestAsDeletedAsync(id, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);

            return Ok(message);
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> SetAministratorObavjestiAsDeletedAsync(int id, CancellationToken cancellationToken)
        {
            var message = await _obavjestService.SetAdministratorObavjestAsDeletedAsync(id, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);

            return Ok(message);
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> SetObavjestAsReadAsync(int id, CancellationToken cancellationToken)
        {
            var message = await _obavjestService.SetObavjestAsReadAsMessageAsync(id, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);

            return Ok(message);
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> SetAdministratorObavjestAsReadAsync(int id, CancellationToken cancellationToken)
        {
            var message = await _obavjestService.SetAdministratorObavjestAsReadAsMessageAsync(id, cancellationToken);
            if (message.IsValid == false)
                return BadRequest(message);

            return Ok(message);
        }
    }
}

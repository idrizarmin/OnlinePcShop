using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCWebShop.Data;
using PCWebShop.Database;
using PCWebShop.Helper;
using PCWebShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCWebShop.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PostController : ControllerBase
    {
        private readonly Context _context;

        public PostController(Context context)
        {
            _context = context;
        }
        [HttpGet]
        public List<PostVM> GetAll()
        {
            var data = _context.Post.OrderBy(p => p.ID)
                .Select(p => new PostVM()
                {
                    AutorPosta=p.AutorPosta,
                    AutorPostaID=p.AutorPostaID,
                    DatumObjave=p.DatumObjave,
                    ID=p.ID,
                    LokacijaSlike=p.LokacijaSlike,
                    Naslov=p.Naslov,
                    Sadrzaj=p.Sadrzaj
                }).AsQueryable();
            return data.Take(100).ToList();
        }
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_context.Post.FirstOrDefault(p => p.ID == id));
        }

        [HttpGet]
        public ActionResult<PagedList<Post>> GetAllPaged(int items_per_page, int page_number)
        {

            var data = _context.Post
                .Include(x => x.AutorPosta)
                .AsQueryable();

            return PagedList<Post>.Create(data, page_number, items_per_page);


        }

        [HttpPost]
        public ActionResult Add([FromBody] PostAddVM x)
        {

            var newPost= new Post
            {
                DatumObjave = x.DatumObjave,
                AutorPostaID=x.AutorPostaID,
                LokacijaSlike=x.LokacijaSlike,
                Naslov=x.Naslov,
                Sadrzaj=x.Sadrzaj                
            };
            _context.Add(newPost);
            _context.SaveChanges();

            return Ok(newPost.ID);
        }
     
       
        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] PostUpdateVM x)
        {
            Post post = _context.Post.Where(p => p.ID == id).FirstOrDefault(p => p.ID == id);

            if (post == null)
                return BadRequest("pogresan ID");

            post.AutorPostaID = x.AutorPostaID;
            post.LokacijaSlike = x.LokacijaSlike;
            post.Sadrzaj = x.Sadrzaj;
            post.Naslov = x.Naslov;
            post.DatumObjave = x.DatumObjave;
            

            _context.SaveChanges();
            return Get(id);
        }
        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            Post post= _context.Post.Find(id);

            if (post == null)
                return BadRequest("pogresan ID");

            _context.Remove(post);

            _context.SaveChanges();
            return Ok(post);
        }
    }
}

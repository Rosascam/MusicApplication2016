using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicFall2016.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicFall2016.Controllers
{
    public class ArtistController : Controller
    {
        private readonly MusicDbContext db;

        public ArtistController(MusicDbContext context)
        {
            db = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var artist = db.Artists.ToList();
            return View(artist);
        }
    }


}
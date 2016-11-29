using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using MusicFall2016.Models;
using Microsoft.AspNetCore.Authorization;
using static MusicFall2016.Models.MusicDbContext;


namespace MusicFall2016.Controllers
{
    [Authorize]
    public class PlaylistsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MusicDbContext _context;

        public PlaylistsController(MusicDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
            ViewBag.User = user;
            var playlists = _context.Playlist.Where(p => p.Owner == user).ToList();

            return View(playlists);

        }
        public IActionResult Add(int? id)
        {

            var playlists = _context.Playlist.Where(p => p.Owner.UserName == User.Identity.Name).ToList();
            ViewBag.Playlist = new SelectList(playlists, "PlaylistID", "name");
            Album album = _context.Albums.Where(a => a.AlbumID == id).Single();

            return View(album);
        }

        [HttpPost]
        public IActionResult Add(int AlbumID, int PlayListID)
        {
            PlaylistAlbums playListAlbums = new PlaylistAlbums { AlbumId = AlbumID, PlaylistID = PlayListID };

            _context.Add(playListAlbums);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Playlist playlist)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
                playlist.Owner = user;
                _context.Add(playlist);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(playlist);
        }

        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = _context.Playlist
                .SingleOrDefault(p => p.PlaylistID == id);

            if (playlist == null)
            {
                return NotFound();
            }
            return View(playlist);
        }

        [HttpPost]
        public IActionResult Update(Playlist playlist)
        {
            _context.Playlist.Update(playlist);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            {
                if (id == null)
                {
                    return NotFound();
                }

                var playlist = _context.Playlist

                    .SingleOrDefault(p => p.PlaylistID == id);

                if (playlist == null)
                {
                    return NotFound();
                }
                return View(playlist);
            }
        }
        [HttpPost]
        public IActionResult Delete(Playlist playlist)
        {
            _context.Playlist.Remove(playlist);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}

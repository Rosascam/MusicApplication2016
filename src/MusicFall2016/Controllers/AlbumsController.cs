using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicFall2016.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace MusicFall2016.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly MusicDbContext _context;

        public AlbumsController(MusicDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Details(String searchString, string sortOrder)
        {
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_asc" : "title_dec";
            ViewData["ArtistSortParm"] = String.IsNullOrEmpty(sortOrder) ? "artist_asc" : "artist_dec";
            ViewData["GenreSortParm"] = String.IsNullOrEmpty(sortOrder) ? "genre_asc" : "genre_dec";
            ViewData["PriceSortParm"] = String.IsNullOrEmpty(sortOrder) ? "price_asc" : "price_dec";
            ViewData["LikeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "like_asc" : "like_dec";
            
            var album = from s in _context.Albums.Include(a => a.Artist).Include(a => a.Genre)
                        select s;
            switch (sortOrder)

            {
                case "title_asc":
                    album = album.OrderBy(s => s.Title);
                    break;
                case "title_dec":
                    album = album.OrderByDescending(s => s.Title);
                    break;
                case "artist_asc":
                    album = album.OrderBy(s => s.Artist.Name);
                    break;
                case "artist_dec":
                    album = album.OrderByDescending(s => s.Artist.Name);
                    break;
                case "genre_asc":
                    album = album.OrderBy(s => s.Genre.Name);
                    break;
                case "genre_dec":
                    album = album.OrderByDescending(s => s.Genre.Name);
                    break;
                case "price_asc":
                    album = album.OrderBy(s => s.Price);
                    break;
                case "price_dec":
                    album = album.OrderByDescending(s => s.Price);
                    break;
                case "like_asc":
                    album = album.OrderBy(s => s.Like);
                    break;
                case "like_dec":
                    album = album.OrderByDescending(s => s.Like);
                    break;
            }

            if (searchString != null)
            {
                var albumSearch = from s in _context.Albums.Include(a => a.Artist).Include(a => a.Genre)
                                  select s;
           
            if (!String.IsNullOrEmpty(searchString))
             {
                albumSearch = albumSearch.Where(s => s.Title.Contains(searchString) || s.Artist.Name.Contains(searchString) || s.Genre.Name.Contains(searchString) || s.Price.ToString().Contains(searchString));
             }

               return View(albumSearch.ToList());
            }

            else
            {
                var albums = _context.Albums.Include(a => a.Artist).Include(a => a.Genre).ToList();                
            }

            return View(await album.AsNoTracking().ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.ArtistList = new SelectList(_context.Artists, "ArtistID", "Name");
            ViewBag.GenreList = new SelectList(_context.Genres, "GenreID", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Album album, String addNewArtist, String AddNewGenre)
        {
            if (ModelState.IsValid)
            {
                if(addNewArtist != null)
                {
                    foreach(var artist in _context.Artists.ToList())
                    {
                        String name = artist.Name;
                        if (name == addNewArtist)
                        {
                            addNewArtist = "";
                        }
                    }

                    if (addNewArtist != "")
                    {
                        Artist artist = new Artist();
                        artist.Name = addNewArtist;
                        artist.Bio = "";
                        _context.Artists.Add(artist);
                        _context.SaveChanges();
                        album.Artist = _context.Artists.Last();
                    }
                }

                if (AddNewGenre != null)
                {
                    foreach(var genres in _context.Genres.ToList())
                    {
                        String name = genres.Name;
                        if (name == AddNewGenre)
                        {
                            AddNewGenre = "";
                        }
                    }

                    if(AddNewGenre != "")
                    {
                        Genre genre = new Genre();
                        genre.Name = AddNewGenre;
                        _context.Genres.Add(genre);
                        _context.SaveChanges();
                        album.Genre = _context.Genres.Last();
                    }
                }

                _context.Albums.Add(album);
                _context.SaveChanges();
                return RedirectToAction("Details");
            }
            ViewBag.ArtistList = new SelectList(_context.Artists, "ArtistID", "Name");
            ViewBag.GenreList = new SelectList(_context.Genres, "GenreID", "Name");
            return View();
        }

        public IActionResult Retrieve(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var album = _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .SingleOrDefault(a => a.AlbumID == id);

            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        public IActionResult Like(int? id)
        {
            if (id == null)
            {
                return NotFound();
    }

    var album = _context.Albums.Include(a => a.Artist).Include(a => a.Genre).SingleOrDefault(a => a.AlbumID == id);

            if (album == null)
            {
                return NotFound();
            }
            album.Like++;
            
            _context.SaveChanges();

            return RedirectToAction("Details");
        }
        public IActionResult Update(int? id)
        {
            {
                if (id == null)
                {
                    return NotFound();
                }
                ViewBag.ArtistList = new SelectList(_context.Artists, "ArtistID", "Name");
                ViewBag.GenreList = new SelectList(_context.Genres, "GenreID", "Name");
                var album = _context.Albums
                    .Include(a => a.Artist)
                    .Include(a => a.Genre)
                    .SingleOrDefault(a => a.AlbumID == id);

                if (album == null)
                {
                    return NotFound();
                }
                return View(album);
            }
        }
        [HttpPost]
        public IActionResult Update(Album album)
        {
            _context.Albums.Update(album);
            _context.SaveChanges();
            return RedirectToAction("Details");
        }
        public IActionResult Delete(int? id)
        {
            {
                if (id == null)
                {
                    return NotFound();
                }
                var album = _context.Albums
                    .Include(a => a.Artist)
                    .Include(a => a.Genre)
                    .SingleOrDefault(a => a.AlbumID == id);

                if (album == null)
                {
                    return NotFound();
                }
                return View(album);
            }
        }
        [HttpPost]
        public IActionResult Delete(Album album)
        {
            _context.Albums.Remove(album);
            _context.SaveChanges();
            return RedirectToAction("Details");
        }
      
    }
}

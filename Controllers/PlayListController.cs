using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlaylistApp.Data;
using PlaylistApp.Entities;
using PlaylistApp.Models.ViewModels;

namespace PlaylistApp.Controllers
{
    public class PlayListController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayListController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Album
        public async Task<IActionResult> Index()
        {
            var playlists = await _context.PlayLists
                .Include(a => a.PlayListSongs)
                .ThenInclude(a=>a.Song)
                .ToListAsync();

            if (playlists.Count() == 0)
            {
                return View("Empty");
            }


            return View(playlists);
        }

        // GET: Album/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Albums
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        // GET: Album/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Album/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id","Name")] PlayList playlist)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.PlayLists.Add(playlist);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

      
        public async Task<IActionResult> AddSongToPlaylist(int playlistID, int? contain)
        {
            var vm = new AddSongPlaylistViewModel()
            {
                PlayList = await _context.PlayLists.FindAsync(playlistID),
                Songs = await _context.Songs
                    .Include(s=>s.Album)
                    .Include(s=>s.ArtistSongs)
                    .ThenInclude(s=>s.Artist)
                    .ToListAsync()
            };
            ViewData["Contain"] = contain;


            return View(vm);
        }

      
        public async Task<IActionResult> AddSongToPlaylistDb(int songId , int playlistId)
        {
            var song = await _context.Songs.FirstOrDefaultAsync(s => s.Id == songId);
            if(song == null)
            {
                return RedirectToAction(nameof(AddSongToPlaylist), new { playlistID = playlistId , contain = 1});
            }
            var playlistSong = new PlayListSong() { 
                SongId = songId,
                PlayListId = playlistId
            };

            var playlistSongs = _context.PlayListSongs.FirstOrDefault(s=>s.PlayListId == playlistId && s.SongId == songId);
           
            if (playlistSongs != null)
            {
            
                return RedirectToAction(nameof(AddSongToPlaylist), new { playlistID = playlistId, contain = 1 });
            }
            ViewData["Contain"] = 0;
            _context.PlayListSongs.Add(playlistSong);
            await _context.SaveChangesAsync();



            return RedirectToAction(nameof(Index));
        }

        // GET: Album/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        // POST: Album/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] Album album)
        {
            if (id != album.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(album);
        }


        // GET: Album/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.PlayLists.FindAsync(id);
            if (id == null)
            {
                return NotFound();
            }
            _context.PlayLists.Remove(playlist);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AlbumExists(int id)
        {
            return _context.Albums.Any(e => e.Id == id);
        }
    }
}

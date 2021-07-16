using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlaylistApp.Data;

namespace PlaylistApp.Entities
{
    public class ArtistSongController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtistSongController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ArtistSong
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ArtistSongs.Include(a => a.Artist).Include(a => a.Song);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ArtistSong/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artistSong = await _context.ArtistSongs
                .Include(a => a.Artist)
                .Include(a => a.Song)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artistSong == null)
            {
                return NotFound();
            }

            return View(artistSong);
        }

        // GET: ArtistSong/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id");
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Id");
            return View();
        }

        // POST: ArtistSong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ArtistId,SongId")] ArtistSong artistSong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artistSong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id", artistSong.ArtistId);
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Id", artistSong.SongId);
            return View(artistSong);
        }

        // GET: ArtistSong/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artistSong = await _context.ArtistSongs.FindAsync(id);
            if (artistSong == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id", artistSong.ArtistId);
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Id", artistSong.SongId);
            return View(artistSong);
        }

        // POST: ArtistSong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ArtistId,SongId")] ArtistSong artistSong)
        {
            if (id != artistSong.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artistSong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistSongExists(artistSong.Id))
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
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id", artistSong.ArtistId);
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Id", artistSong.SongId);
            return View(artistSong);
        }

        // GET: ArtistSong/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artistSong = await _context.ArtistSongs
                .Include(a => a.Artist)
                .Include(a => a.Song)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artistSong == null)
            {
                return NotFound();
            }

            return View(artistSong);
        }

        // POST: ArtistSong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artistSong = await _context.ArtistSongs.FindAsync(id);
            _context.ArtistSongs.Remove(artistSong);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistSongExists(int id)
        {
            return _context.ArtistSongs.Any(e => e.Id == id);
        }
    }
}

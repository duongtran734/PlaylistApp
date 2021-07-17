using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlaylistApp.Data;
using PlaylistApp.Entities;
using PlaylistApp.Extensions;

namespace PlaylistApp.Controllers
{
    public class SongController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SongController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Song
        public async Task<IActionResult> Index()
        {
            return View(await _context.Songs.ToListAsync());
        }
        public async Task<IActionResult> AlbumSongIndex(int albumID)
        {
            var song = await _context.Songs
                .Include(s=>s.Album)
                .Where(s => s.AlbumId == albumID).ToListAsync();
            return View(song);
        }

        // GET: Song/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(m=>m.Album)
                .Include(m=>m.ArtistSongs)
                .ThenInclude(m=>m.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // GET: Song/Create
        public async Task<IActionResult> Create()
        {
            var albums = await _context.Albums.ToListAsync();
            var artists = await _context.Artists.ToListAsync();
            Song song = new Song();
        
            song.AlbumCollection = albums;
            song.ArtistCollection = artists;
            return View(song);
        }

        // POST: Song/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Duration,AlbumId, SelectArtistIds")] Song song)
        {
            if (ModelState.IsValid)
            {
               //First Add the Song to the Song DB
                _context.Add(song);
                await _context.SaveChangesAsync();

                //Then Add the the mapping to SongArtist
                if (song.SelectArtistIds.Count() > 0)
                {

                    foreach (int artistID in song.SelectArtistIds)
                    {
                        ArtistSong artist_song = new ArtistSong() { ArtistId = artistID, SongId = song.Id };
                        _context.ArtistSongs.Add(artist_song);
                    }
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(song);
        }

        // GET: Song/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var song = await _context.Songs
                .Include(s => s.ArtistSongs)
                .ThenInclude(s=> s.Artist)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (song == null)
            {
                return NotFound();
            }

            // 3 NotMapped attributes initialize
            song.AlbumCollection = await _context.Albums.ToListAsync();
            song.ArtistCollection = await _context.Artists.ToListAsync();
            //Add Artist ID to selectArtistIds [] to display in multi dropdown select
            List<int> temp = new List<int>();
            foreach (ArtistSong artistSong in song.ArtistSongs)
            {
                temp.Add(artistSong.ArtistId);
            }
            song.SelectArtistIds = temp.ToArray();

            return View(song);
        }

        // POST: Song/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int[] SelectArtistIds)
        {
            if (id == 0)
            {
                return NotFound();
            }
            

            var song = await _context.Songs
                .Include(s => s.ArtistSongs).ThenInclude(s => s.Artist)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (await TryUpdateModelAsync<Song>(
               song,
               "",
               i => i.Title, i => i.Duration, i => i.Album))
            {
                UpdateSongArtists(SelectArtistIds, song);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));

            }

            UpdateSongArtists(song.SelectArtistIds, song);
            return View(song);
        }

        private void UpdateSongArtists(int[] selectedArtist, Song songToUpdate)
        {
            //If no checkboxes were selected, the code in UpdateInstructorCourses initializes
            //the CourseAssignments navigation property with an empty collection and returns:
            if (selectedArtist == null)
            {
                songToUpdate.ArtistSongs = new List<ArtistSong>();
                return;
            }


            var selectedCArtistsHS = new HashSet<int>(selectedArtist);
            var songArtists = new HashSet<int>(songToUpdate.ArtistSongs.Select(a => a.Artist.Id));
                //(instructorToUpdate.CourseAssignments.Select(c => c.Course.CourseID));

            foreach (var artist in _context.Artists)
            {
                if (selectedCArtistsHS.Contains(artist.Id))
                {
                    if (!songArtists.Contains(artist.Id))
                    {
                       songToUpdate.ArtistSongs.Add(new ArtistSong { ArtistId = artist.Id , SongId = songToUpdate.Id });
                    }
                }
                else
                {
                    if (songArtists.Contains(artist.Id))
                    {
                        // the course is removed from the navigation property.
                        ArtistSong artistToRemove = songToUpdate.ArtistSongs.FirstOrDefault(i => i.ArtistId == artist.Id);
                        _context.Remove(artistToRemove);
                    }
                }
            }
        }

            // GET: Song/Delete/5
            public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Song/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
            return _context.Songs.Any(e => e.Id == id);
        }
    }
}

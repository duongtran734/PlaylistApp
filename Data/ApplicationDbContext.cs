using Microsoft.EntityFrameworkCore;
using PlaylistApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaylistApp.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ) : base(options)
        {
        }

        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<PlayList> PlayLists { get; set; }
        public DbSet<PlayListSong> PlayListSongs { get; set; }
        public DbSet<ArtistSong> ArtistSongs { get; set; }

    }
}

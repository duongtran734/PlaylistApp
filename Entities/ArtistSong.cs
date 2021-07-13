using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaylistApp.Entities
{
    public class ArtistSong
    {
        public int Id { get; set; }
        public int ArtistId { get; set; }
        public int SongId { get; set; }

        public Song Song { get; set; }
        public Artist Artist { get; set; }

    }
}

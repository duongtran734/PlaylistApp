using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlaylistApp.Entities
{
    public class Song
    {
        public int Id { get; set; }
        public string Duration { get; set; }

        public int AlbumId { get; set; } //1:N with Album
        
        [ForeignKey("SongId")]
        public virtual ICollection<ArtistSong> ArtistSongs { get; set; }
    }
}

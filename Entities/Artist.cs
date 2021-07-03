using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlaylistApp.Entities
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("ArtistId")]
        public virtual ICollection<ArtistSong> ArtistSongs { get; set; }
    }
}

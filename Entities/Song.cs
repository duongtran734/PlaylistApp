using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlaylistApp.Entities
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Duration { get; set; }

        [DisplayName("Album")]public int AlbumId { get; set; } //1:N with Album
        public Album Album { get; set; }

        
        public virtual ICollection<ArtistSong> ArtistSongs { get; set; } //N:M
        /*
        [NotMapped] //telling EF to ignore this
        public virtual ICollection<SelectListItem> Albums { get; set; } // this is use for dropdown list (SelectListItem from .Rendering)
        */
        
        [NotMapped] public IEnumerable<Album> AlbumCollection { get; set; }
        [NotMapped] public IEnumerable<Artist> ArtistCollection { get; set; }
        [DisplayName("Artist")][NotMapped] public int[] SelectArtistIds { get; set; }

    }
}

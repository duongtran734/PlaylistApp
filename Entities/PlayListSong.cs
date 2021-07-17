using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaylistApp.Entities
{
    public class PlayListSong
    {
        public int Id { get; set; }
        public int PlayListId { get; set; }
        public int SongId { get; set; }

        public Song Song { get; set; }
        public PlayList PlayList { get; set; }

    }
}

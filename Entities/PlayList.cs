using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaylistApp.Entities
{
    public class PlayList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PlayListSong> PlayListSongs { get; set; }
    }
}

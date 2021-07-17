using PlaylistApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaylistApp.Models.ViewModels
{
    public class AddSongPlaylistViewModel
    {
        public ICollection<Song> Songs { get; set; }
        public PlayList PlayList { get; set; }
    }
}

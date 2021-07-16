using PlaylistApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaylistApp.Models.ViewModels
{
    public class ArtistViewModel
    {
        public Artist Artist { get; set; }
        public string Referer { get; set; }
    }
}

using PlaylistApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaylistApp.Models.ViewModels
{
    public class AlbumViewModel
    {
        public Album Album { get; set; }
        public string Referer { get; set; }
    }
}

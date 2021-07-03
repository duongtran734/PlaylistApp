using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaylistApp.Entities
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string About { get; set; }

        [ForeignKey("AlbumId")]
        public virtual ICollection<Song> Songs{ get; set; }
    }
}
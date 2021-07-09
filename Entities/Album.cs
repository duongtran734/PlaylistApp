using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaylistApp.Entities
{
    public class Album
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }


        [ForeignKey("AlbumId")]
        public virtual ICollection<Song> Songs{ get; set; }
    }
}
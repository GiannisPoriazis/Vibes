using System.ComponentModel.DataAnnotations;
using Vibes.Interfaces;

namespace Vibes.Models
{
    public class Track: IEntity<int>
    {
        [Key]
        public int Id { get; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public virtual ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
    }
}

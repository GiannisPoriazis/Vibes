using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vibes.Interfaces;

namespace Vibes.Models
{
    public class Playlist: IEntity<int>
    {
        [Key]
        public int Id { get; }
        [Required]
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        [NotMapped]
        public Bitmap? CachedCover { get; set; }
        public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vibes.Interfaces;

namespace Vibes.Models
{
    public enum TrackType
    {
        Song,
        Podcast,
    }

    public class Track: IEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public virtual TrackType Type { get; set; } = TrackType.Song;
        public string Album { get; set; } = string.Empty;
        public string StreamUrl { get; set; } = string.Empty;
        public int Duration { get; set; } 
        public string FormattedDuration => TimeSpan.FromSeconds(Duration).ToString(@"m\:ss");
        public string CoverUrl { get; set; } = string.Empty;
        [NotMapped]
        public Bitmap? CachedCover { get; set; }
        public virtual ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
    }
}

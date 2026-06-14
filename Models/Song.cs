using System.ComponentModel.DataAnnotations;

namespace Vibes.Models
{
    public class Song: Track
    {
        public override TrackType Type { get; set; } = TrackType.Song;
    }
}

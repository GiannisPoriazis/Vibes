using System.ComponentModel.DataAnnotations;

namespace Vibes.Models
{
    public class Podcast: Track
    {
        public override TrackType Type { get; set; } = TrackType.Podcast;
    }
}

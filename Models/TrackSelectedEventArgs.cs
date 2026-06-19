using Vibes.Interfaces;

namespace Vibes.Models
{
    public class TrackSelectedEventArgs
    {
        public string PageTitle { get; }
        public string Metadata { get; }
        public IEnumerable<Track> Tracks { get; }
        public IEntity<int> Entity { get;  }
        public bool Autoplay { get; }

        public TrackSelectedEventArgs(string pageTitle, string metadata, IEnumerable<Track> tracks, IEntity<int> entity, bool autoplay = true)
        {
            PageTitle = pageTitle;
            Metadata = metadata;
            Tracks = tracks;
            Entity = entity;
            Autoplay = autoplay;
        }
    }
}

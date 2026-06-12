using Vibes.Models;

namespace Vibes.Database
{
    public static class DbInitializer
    {
        public static void SeedMockDataForUser(string userId)
        {
            using (var context = new Database.VibesDbContext())
            {
                if (context.Playlists.Any(p => p.UserId == userId)) return;

                // 1. Create unique Master Tracks first
                var track1 = new Track { Title = "Hired (Clean Code Mix)", Artist = "The Developers" };
                var track2 = new Track { Title = "Resume Approved", Artist = "SaaS Masters" };
                var track3 = new Track { Title = "Lo-Fi Coding Beats", Artist = "ChilledCow" };

                context.Tracks.AddRange(track1, track2, track3);

                var playlist1 = new Playlist { Name = "🔥 Recruiter's Top Hits", UserId = userId };
                var playlist2 = new Playlist { Name = "☕ Coding Focus Beats", UserId = userId };

                playlist1.Tracks.Add(track1);
                playlist1.Tracks.Add(track2);

                playlist2.Tracks.Add(track2);
                playlist2.Tracks.Add(track3);

                context.Playlists.AddRange(playlist1, playlist2);
                context.SaveChanges();
            }
        }
    }
}
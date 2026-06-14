using Vibes.Models;

namespace Vibes.Database
{
    public static class DbInitializer
    {
        public static void SeedMockDataForUser(string userId)
        {
            using (var context = new VibesDbContext())
            {
                if (context.Playlists.Any(p => p.UserId == userId)) return;

                var playlist1 = new Playlist { Name = "🔥 Recruiter's Top Hits", UserId = userId };
                var playlist2 = new Playlist { Name = "☕ Coding Focus Beats", UserId = userId };

                context.Playlists.AddRange(playlist1, playlist2);
                context.SaveChanges();
            }
        }
    }
}
using System.Text.Json;
using Vibes;
using Vibes.Interfaces;

public class AudioStreamingService: IAudioStreamingService
{
    private static readonly HttpClient _client = new HttpClient();
    private readonly string _apiClientId;

    public AudioStreamingService()
    {
        _apiClientId = AppConfig.Get("Jamendo:ClientId") ?? throw new InvalidOperationException("Jamendo:ClientId needs to be set in appsettings.json");
    }

    public async Task<List<TrackSearchResult>> SearchTracksAsync(string query)
    {
        string url = $"https://api.jamendo.com/v3.0/tracks/?client_id={_apiClientId}&format=json&search={Uri.EscapeDataString(query)}&limit=10";

        var response = await _client.GetStringAsync(url);
        using var doc = JsonDocument.Parse(response);
        var root = doc.RootElement.GetProperty("results");

        var results = new List<TrackSearchResult>();
        foreach (var item in root.EnumerateArray())
        {
            results.Add(new TrackSearchResult
            {
                Title = item.GetProperty("name").GetString() ?? "",
                Artist = item.GetProperty("artist_name").GetString() ?? "",
                CoverUrl = item.GetProperty("image").GetString() ?? "",
                StreamUrl = item.GetProperty("audio").GetString() ?? "", 
                Duration = item.GetProperty("duration").GetInt32().ToString()
            });
        }
        return results;
    }
}

public class TrackSearchResult
{
    public string Title { get; set; } = string.Empty;
    public string Artist { get; set; } = string.Empty;
    public string Type { get; set; } = "Song"; 
    public string CoverUrl { get; set; } = string.Empty;
    public string StreamUrl { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public Bitmap? CachedCover { get; set; }
}
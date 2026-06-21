using Microsoft.Extensions.Logging;
using System.Text.Json;
using Vibes;
using Vibes.Interfaces;
using Vibes.Models;

public class AudioStreamingService : IAudioStreamingService
{
    private static readonly HttpClient _client = new HttpClient();
    private readonly string _apiClientId;
    private readonly ILogger<AudioStreamingService> _logger;

    public AudioStreamingService(ILogger<AudioStreamingService> logger)
    {
        _apiClientId = AppConfig.Get("Jamendo:ClientId") ?? throw new InvalidOperationException("Jamendo:ClientId needs to be set in appsettings.json");
        _logger = logger;
    }

    public async Task<List<Track>> SearchTracksAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query)) return new List<Track>();

        string url = $"https://api.jamendo.com/v3.0/tracks/?client_id={_apiClientId}&format=json&search={Uri.EscapeDataString(query.Trim())}&limit=10&include=musicinfo";
        return await FetchAndParseTracksAsync(url);
    }

    public async Task<List<Track>> GetTracksAsync(int limit, string? order = null, bool featured = false)
    {
        string url = $"https://api.jamendo.com/v3.0/tracks/?client_id={_apiClientId}&format=json&limit={limit}&include=musicinfo";

        if (!string.IsNullOrEmpty(order))
        {
            url += $"&order={order}";
        }

        if (featured)
        {
            url += "&featured=true";
        }

        return await FetchAndParseTracksAsync(url);
    }

    private async Task<List<Track>> FetchAndParseTracksAsync(string url)
    {
        try
        {
            var response = await _client.GetStringAsync(url);
            return await ParseTracksFromResponseAsync(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Jamendo Fetch Error URL [{Url}]: {Message}", url, ex.Message);
            return new List<Track>();
        }
    }

    private async Task<List<Track>> ParseTracksFromResponseAsync(string jsonResponse)
    {
        using var doc = JsonDocument.Parse(jsonResponse);

        if (!doc.RootElement.TryGetProperty("results", out var root))
            return new List<Track>();

        var results = new List<Track>();
        foreach (var item in root.EnumerateArray())
        {
            int durationSeconds = item.GetProperty("duration").GetInt32();
            bool isPodcast = false;

            if (item.TryGetProperty("musicinfo", out var musicInfo) &&
                musicInfo.TryGetProperty("tags", out var tagsNode))
            {
                isPodcast = EvaluatePodcastStatus(tagsNode);
            }

            Track trackItem;
            string idStr = item.GetProperty("id").GetString() ?? "0";
            int trackId = int.TryParse(idStr, out int parsedId) ? parsedId : 0;

            if (isPodcast)
            {
                trackItem = new Podcast
                {
                    Id = trackId,
                    Type = TrackType.Podcast,
                    Title = item.GetProperty("name").GetString() ?? string.Empty,
                    Artist = item.GetProperty("artist_name").GetString() ?? string.Empty,
                    Album = item.GetProperty("album_name").GetString() ?? string.Empty,
                    CoverUrl = item.GetProperty("image").GetString() ?? string.Empty,
                    StreamUrl = item.GetProperty("audio").GetString() ?? string.Empty,
                    Duration = durationSeconds
                };
            }
            else
            {
                trackItem = new Song
                {
                    Id = trackId,
                    Type = TrackType.Song,
                    Title = item.GetProperty("name").GetString() ?? string.Empty,
                    Artist = item.GetProperty("artist_name").GetString() ?? string.Empty,
                    Album = item.GetProperty("album_name").GetString() ?? string.Empty,
                    CoverUrl = item.GetProperty("image").GetString() ?? string.Empty,
                    StreamUrl = item.GetProperty("audio").GetString() ?? string.Empty,
                    Duration = durationSeconds
                };
            }

            if (!string.IsNullOrEmpty(trackItem.CoverUrl))
            {
                try
                {
                    byte[] imgBytes = await _client.GetByteArrayAsync(trackItem.CoverUrl);
                    using var ms = new MemoryStream(imgBytes);
                    trackItem.CachedCover = new Bitmap(ms);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Failed to load track cover from {Url}: {Message}", trackItem.CoverUrl, ex.Message);
                }
            }

            results.Add(trackItem);
        }

        return results;
    }

    private bool EvaluatePodcastStatus(JsonElement tagsNode)
    {
        if (tagsNode.ValueKind == JsonValueKind.Array)
        {
            foreach (var tag in tagsNode.EnumerateArray())
            {
                if (tag.ValueKind == JsonValueKind.String && IsPodcastTag(tag.GetString()))
                    return true;
            }
        }
        else if (tagsNode.ValueKind == JsonValueKind.Object)
        {
            foreach (var prop in tagsNode.EnumerateObject())
            {
                if (prop.Value.ValueKind == JsonValueKind.String && IsPodcastTag(prop.Value.GetString()))
                {
                    return true;
                }
                else if (prop.Value.ValueKind == JsonValueKind.Array)
                {
                    foreach (var subTag in prop.Value.EnumerateArray())
                    {
                        if (subTag.ValueKind == JsonValueKind.String && IsPodcastTag(subTag.GetString()))
                            return true;
                    }
                }
            }
        }
        return false;
    }

    private bool IsPodcastTag(string? tagText)
    {
        if (string.IsNullOrWhiteSpace(tagText)) return false;

        return tagText.Contains("podcast") ||
               tagText.Contains("spokenword") ||
               tagText.Contains("interview") ||
               tagText.Contains("speech");
    }
}
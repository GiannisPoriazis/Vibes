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

        try
        {
            var response = await _client.GetStringAsync(url);
            using var doc = JsonDocument.Parse(response);
            
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
                    if (tagsNode.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var tag in tagsNode.EnumerateArray())
                        {
                            if (tag.ValueKind == JsonValueKind.String && IsPodcastTag(tag.GetString()))
                            {
                                isPodcast = true;
                                break;
                            }
                        }
                    }
                    else if (tagsNode.ValueKind == JsonValueKind.Object)
                    {
                        foreach (var prop in tagsNode.EnumerateObject())
                        {
                            if (prop.Value.ValueKind == JsonValueKind.String)
                            {
                                if (IsPodcastTag(prop.Value.GetString()))
                                {
                                    isPodcast = true;
                                    break;
                                }
                            }
                            else if (prop.Value.ValueKind == JsonValueKind.Array)
                            {
                                foreach (var subTag in prop.Value.EnumerateArray())
                                {
                                    if (subTag.ValueKind == JsonValueKind.String && IsPodcastTag(subTag.GetString()))
                                    {
                                        isPodcast = true;
                                        break;
                                    }
                                }
                            }

                            if (isPodcast) break;
                        }
                    }
                }

                Track trackItem;
                if (isPodcast)
                {
                    trackItem = new Podcast
                    {
                        Id = int.Parse(item.GetProperty("id").GetString()!),
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
                        Id = int.Parse(item.GetProperty("id").GetString()!),
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
                        _logger.LogError("Failed to load track cover: {1}", ex.Message);
                    }
                }

                results.Add(trackItem);
            }
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Jamendo Fetch Error: {Message}", ex.Message);
            return new List<Track>();
        }
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
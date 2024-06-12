using CodingTestEdwinMoreno.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace CodingTestEdwinMoreno.Services
{
    public class HackerNewsExercise : IHackerNewsExercise
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _cache;
        private const string BestStoriesUrl = "https://hacker-news.firebaseio.com/v0/beststories.json";
        private const string StoryUrl = "https://hacker-news.firebaseio.com/v0/item/21233041.json ";

        public HackerNewsExercise(IHttpClientFactory httpClientFactory, IMemoryCache cache)
        {
            _httpClientFactory = httpClientFactory;
            _cache = cache;
        }

        public async Task<List<Story>> GetBestStoriesAsync(int count)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var storyIds = await GetBestStoryIdsAsync(httpClient);

            var stories = new List<Story>();
            var tasks = storyIds.Take(count).Select(async id =>
            {
                var story = await GetStoryAsync(httpClient, id);
                if (story != null)
                {
                    stories.Add(story);
                }
            });

            await Task.WhenAll(tasks);
            return stories.OrderByDescending(s => s.Score).ToList();
        }

        private async Task<List<int>> GetBestStoryIdsAsync(HttpClient httpClient)
        {
            if (!_cache.TryGetValue("BestStoryIds", out List<int> storyIds))
            {
                var response = await httpClient.GetStringAsync(BestStoriesUrl);
                storyIds = JsonSerializer.Deserialize<List<int>>(response);
                _cache.Set("BestStoryIds", storyIds, TimeSpan.FromMinutes(10));
            }
            return storyIds;
        }

        private async Task<Story> GetStoryAsync(HttpClient httpClient, int id)
        {
            var url = string.Format(StoryUrl, id);
            var response = await httpClient.GetStringAsync(url);
            var storyData = JsonSerializer.Deserialize<JsonElement>(response);

            return new Story
            {
                Title = storyData.GetProperty("title").GetString(),
                Uri = storyData.GetProperty("url").GetString(),
                PostedBy = storyData.GetProperty("by").GetString(),
                Time = DateTimeOffset.FromUnixTimeSeconds(storyData.GetProperty("time").GetInt64()).UtcDateTime,
                Score = storyData.GetProperty("score").GetInt32(),
                CommentCount = storyData.GetProperty("descendants").GetInt32()
            };
        }
    }
}
using CodingTestEdwinMoreno.Models;

namespace CodingTestEdwinMoreno.Services
{
    public interface IHackerNewsExercise
    {
        Task<List<Story>> GetBestStoriesAsync(int count);

    }
}

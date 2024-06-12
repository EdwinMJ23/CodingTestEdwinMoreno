using CodingTestEdwinMoreno.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodingTestEdwinMoreno.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoriesController : ControllerBase
    {
        private readonly IHackerNewsExercise _hackerNewsExercise;

        public StoriesController(IHackerNewsExercise hackerNewsExercise)
        {
            _hackerNewsExercise = hackerNewsExercise;
        }
        [HttpGet]
        public async Task<IActionResult> GetBestStories([FromQuery] int count = 10)
        {
            var stories = await _hackerNewsExercise.GetBestStoriesAsync(count);
            return Ok(stories);
        }

    }
}
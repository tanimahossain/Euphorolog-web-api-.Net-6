using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Euphorolog.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController] 
    public class StoriesController : ControllerBase
    {
        static volatile public List<Stories> _stories= new List<Stories> {
                new Stories
                {
                    storyId = "tanima-1",
                    storyNo = 1,  
                    storyTitle = "Testing Title",
                    storyDescription = "Testing Description",
                    authorName = "Tanima",
                    openingLines = "Testing Opening"
                }
        };
        [HttpGet]
        public async Task<ActionResult<List<Stories>>> Get()
        {
            var ret = _stories;
            return Ok(ret);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Stories>> Get(string id)
        {
            var ret = _stories.Find(s => s.storyId == id );
            if(ret == null)
                return BadRequest("Wrong Story Id");
            return Ok(ret);
        }
        [HttpPost]
        public async Task<ActionResult<List<Stories>>> Post(Stories story)
        {
            int mx = _stories.Max(s => s.authorName == story.authorName ? s.storyNo : 0);
            story.storyId = $"{story.authorName}-{(mx + 1)}";
            story.storyNo = mx + 1;
            _stories.Add(story);
            return Ok(_stories);
        }
        [HttpPut]
        public async Task<ActionResult<List<Stories>>> Put(Stories story)
        {
            var ret = _stories.Find(s => s.storyId == story.storyId);
            if (ret == null)
                return BadRequest("Wrong Story Id");
            if (story.storyDescription != null)
                ret.storyDescription = story.storyDescription;
            if (story.storyTitle != null)
                ret.storyTitle = story.storyTitle;
            return Ok(_stories);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Stories>>> Delete(string id)
        {
            var ret = _stories.Find(s => s.storyId == id);
            if (ret == null)
                return BadRequest("Wrong Story Id");
            _stories.Remove(ret);
            return Ok(_stories);
        }
    }
}

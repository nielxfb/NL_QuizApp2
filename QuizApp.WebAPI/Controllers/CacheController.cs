using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace QuizApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private IMemoryCache _memoryCache;

        public CacheController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet("clear-cache")]
        [Authorize]
        public IActionResult ClearCache()
        {
            _memoryCache.Dispose();
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            return Ok("Cache cleared!");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TypedClientsPattern.Services;

namespace TypedClientsPattern.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _postsService;
        private readonly ILogger<PostsController> _logger;

        public PostsController(IPostsService postsService, ILogger<PostsController> logger)
        {
            _postsService = postsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var posts = await _postsService.GetPostsAsync().ConfigureAwait(false);
                return Ok(posts);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);

                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
        }
    }

    
}

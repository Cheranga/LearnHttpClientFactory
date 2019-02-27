using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BasicPattern.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BasicPattern.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PostsController : ControllerBase
    {
        private const string FakePostsApi = @"https://jsonplaceholder.typicode.com/posts";

        private readonly HttpClient _httpClient;

        public PostsController(IHttpClientFactory httpClientFactory)
        {
            if (httpClientFactory == null)
            {
                throw new ArgumentNullException(nameof(HttpClient));
            }

            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var httpResponse = await _httpClient.GetAsync(FakePostsApi).ConfigureAwait(false);
            if (!httpResponse.IsSuccessStatusCode)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, httpResponse.ReasonPhrase);
            }

            var posts = JsonConvert.DeserializeObject<List<Post>>(await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false));
            return Ok(posts);
        }
    }
}
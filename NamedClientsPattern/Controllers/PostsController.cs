using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DTO.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace NamedClientsPattern.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        

        public PostsController(IHttpClientFactory httpClientFactory)
        {
            if (httpClientFactory == null)
            {
                throw new ArgumentNullException(nameof(httpClientFactory));
            }

            _httpClient = httpClientFactory.CreateClient("FakePosts");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, "/posts");
            var httpResponse = await _httpClient.SendAsync(httpRequest).ConfigureAwait(false);
            if (!httpResponse.IsSuccessStatusCode)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, httpResponse.ReasonPhrase);
            }

            var posts = JsonConvert.DeserializeObject<List<PostResponse>>(await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false));
            return Ok(posts);
        }
    }
}

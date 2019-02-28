using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DTO.Requests;
using DTO.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace NamedClientsPattern.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class GitHubController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public GitHubController(IHttpClientFactory httpClientFactory)
        {
            if (httpClientFactory == null)
            {
                throw new ArgumentNullException(nameof(httpClientFactory));
            }

            _httpClient = httpClientFactory.CreateClient("GitHub");
        }

        [HttpGet("{UserName}")]
        public async Task<IActionResult> Get([FromRoute]GetUserRepositoriesRequest request)
        {
            if (!request?.IsValid() ?? false)
            {
                return BadRequest();
            }

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $@"/users/{request.UserName}/repos");
            var httpResponse = await _httpClient.SendAsync(httpRequest).ConfigureAwait(false);
            if (!httpResponse.IsSuccessStatusCode)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, httpResponse.ReasonPhrase);
            }

            var repositories = JsonConvert.DeserializeObject<List<GitHubRepositoryResponse>>(await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false));
            return Ok(repositories);

        }
    }
}

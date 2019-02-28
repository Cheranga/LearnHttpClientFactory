using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DTO.Responses;
using Newtonsoft.Json;

namespace TypedClientsPattern.Services
{
    public class PostsService : IPostsService
    {
        private readonly HttpClient _httpClient;

        public PostsService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            InitHttpClient();
        }

        public async Task<List<PostResponse>> GetPostsAsync()
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, @"/posts");
            var httpResponse = await _httpClient.SendAsync(httpRequest).ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(httpResponse.ReasonPhrase);
            }

            var posts = JsonConvert.DeserializeObject<List<PostResponse>>(await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false));
            return posts;
        }

        private void InitHttpClient()
        {
            _httpClient.BaseAddress = new Uri(@"https://jsonplaceholder.typicode.com");
        }
    }
}
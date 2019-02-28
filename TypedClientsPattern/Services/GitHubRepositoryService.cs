using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DTO.Requests;
using DTO.Responses;
using Newtonsoft.Json;

namespace TypedClientsPattern.Services
{
    public class GitHubRepositoryService : IGitHubRepositoryService
    {
        private readonly HttpClient _httpClient;

        public GitHubRepositoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            InitHttpClient();
        }

        public async Task<List<GitHubRepositoryResponse>> GetRepositoriesAsync(GetUserRepositoriesRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $@"/users/{request.UserName}/repos");
            var httpResponse = await _httpClient.SendAsync(httpRequest).ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(httpResponse.ReasonPhrase);
            }

            var repositories = JsonConvert.DeserializeObject<List<GitHubRepositoryResponse>>(await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false));
            return repositories;
        }

        private void InitHttpClient()
        {
            _httpClient.BaseAddress = new Uri(@"https://api.github.com");
            // Github API versioning
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            // Github requires a user-agent
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
        }
    }
}
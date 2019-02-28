using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DTO.Requests;
using DTO.Responses;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using Xunit;

namespace NamedClientsPattern.Tests
{
    public class GitHubControllerTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;

        public GitHubControllerTests(TestWebApplicationFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            _httpClient = factory.CreateClient();
        }

        [Theory]
        [InlineData("cheranga")]
        [InlineData("odetocode")] // My Hero - Scott Allen
        public async Task An_Active_Developer_Must_Have_Repositories(string userName)
        {
            //
            // Arrange and Act
            //
            var httpResponse = await _httpClient.GetAsync($@"/api/github/{userName}").ConfigureAwait(false);
            //
            // Assert
            //
            httpResponse.EnsureSuccessStatusCode();

            var repositories = JsonConvert.DeserializeObject<List<GitHubRepositoryResponse>>(await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false));
            Assert.True(repositories != null && repositories.Any());
        }
    }
}
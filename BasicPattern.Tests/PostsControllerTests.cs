using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BasicPattern.DTO;
using Newtonsoft.Json;
using Xunit;

namespace BasicPattern.Tests
{
    public class PostsControllerTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;

        public PostsControllerTests(TestWebApplicationFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task Get_All_Posts_Must_Have_Posts()
        {
            //
            // Arrange and Act
            //
            var httpResponse = await _httpClient.GetAsync(@"/api/Posts").ConfigureAwait(false);

            httpResponse.EnsureSuccessStatusCode();

            var posts = JsonConvert.DeserializeObject<List<Post>>(await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false));
            //
            // Assert
            //
            Assert.NotNull(posts);
        }
    }
}

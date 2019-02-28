using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DTO.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TypedClientsPattern.Services;

namespace TypedClientsPattern.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class GitHubController : ControllerBase
    {
        private readonly IGitHubRepositoryService _gitHubRepositoryService;
        private readonly ILogger<GitHubController> _logger;

        public GitHubController(IGitHubRepositoryService gitHubRepositoryService, ILogger<GitHubController> logger)
        {
            _gitHubRepositoryService = gitHubRepositoryService;
            _logger = logger;
        }

        [HttpGet("{UserName}")]
        public async Task<IActionResult> Get([FromRoute] GetUserRepositoriesRequest request)
        {
            try
            {
                var repositories = await _gitHubRepositoryService.GetRepositoriesAsync(request).ConfigureAwait(false);
                return Ok(repositories);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
        }

    }
}

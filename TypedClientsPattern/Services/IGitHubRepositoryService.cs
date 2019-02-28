using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.Requests;
using DTO.Responses;

namespace TypedClientsPattern.Services
{
    public interface IGitHubRepositoryService
    {
        Task<List<GitHubRepositoryResponse>> GetRepositoriesAsync(GetUserRepositoriesRequest request);
    }
}
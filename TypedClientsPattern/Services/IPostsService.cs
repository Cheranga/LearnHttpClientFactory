using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.Responses;

namespace TypedClientsPattern.Services
{
    public interface IPostsService
    {
        Task<List<PostResponse>> GetPostsAsync();
    }
}
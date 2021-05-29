using System.Threading.Tasks;
using TutorBackend.Core.Dto;
using TutorBackend.Core.Requests;
using TutorBackend.Core.Responses;

namespace TutorBackend.Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        Task<JwtResponse> CreateUser(CreateUserRequest request);

        Task<JwtResponse> LoginUser(LoginRequest request);

        Task<UserDto> GetUser(string username);
    }
}

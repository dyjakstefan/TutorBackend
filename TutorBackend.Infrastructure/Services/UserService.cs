using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TutorBackend.Core.Dto;
using TutorBackend.Core.Entities;
using TutorBackend.Core.Requests;
using TutorBackend.Core.Responses;
using TutorBackend.Infrastructure.Repositories.Interfaces;
using TutorBackend.Infrastructure.Services.Interfaces;
using TutorBackend.Infrastructure.SqlServerContext;

namespace TutorBackend.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext dbContext;
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly IMapper mapper;
        private readonly IJwtService jwtService;

        public UserService(DatabaseContext dbContext, IPasswordHasher<User> passwordHasher, IMapper mapper, IJwtService jwtService, IUserRepository userRepository)
        {
            this.dbContext = dbContext;
            this.passwordHasher = passwordHasher;
            this.mapper = mapper;
            this.jwtService = jwtService;
            this.userRepository = userRepository;
        }

        public async Task<JwtResponse> CreateUser(CreateUserRequest request)
        {
            if (await dbContext.Users.AnyAsync(x => x.Email == request.Email || x.Username == request.Username))
            {
                return null;
            }
            
            var user = mapper.Map<CreateUserRequest, User>(request);
            var passwordHash = passwordHasher.HashPassword(user, request.Password);
            user.Password = passwordHash;
            dbContext.Add(user);
            await dbContext.SaveChangesAsync();
            var jwt = jwtService.CreateToken(user.Id, user.Username);
            return jwt;
        }

        public async Task<JwtResponse> LoginUser(LoginRequest request)
        {
            var user = await dbContext.Users.SingleOrDefaultAsync(x => x.Username == request.Username);
            if (user == null)
            {
                return null;
            }

            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);
            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                return null;
            }

            var jwt = jwtService.CreateToken(user.Id, user.Username);
            return jwt;
        }

        public async Task<UserDto> GetUser(string username)
        {
            var user = await userRepository.GetByUsername(username);

            var userDto = mapper.Map<UserDto>(user);

            return userDto;
        }
    }
}

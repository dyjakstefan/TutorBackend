using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TutorBackend.Core.Entities;
using TutorBackend.Core.Requests;
using TutorBackend.Core.Responses;
using TutorBackend.Infrastructure.Services;
using TutorBackend.Infrastructure.Services.Interfaces;
using TutorBackend.Infrastructure.SqlServerContext;
using Xunit;

namespace TutorBackend.Infrastructure.Tests
{
    public class UserServiceTests
    {
        private readonly DatabaseContext dbContext;
        private readonly Mock<IPasswordHasher<User>> passwordHasher;
        private readonly Mock<IMapper> mapper;
        private readonly Mock<IJwtService> jwtService;

        public UserServiceTests()
        {
            var data = new List<User>
            {
                new User { Username = "test1", Email = "test1"}
            }.AsQueryable();

            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TutorDatabase")
                .Options;

            dbContext = new DatabaseContext(options);
            dbContext.Users.Add(new User { Username = "test1", Email = "test1" });
            dbContext.SaveChanges();

            passwordHasher = new Mock<IPasswordHasher<User>>();
            mapper = new Mock<IMapper>();
            jwtService = new Mock<IJwtService>();
        }


        [Fact]
        public async Task ShouldReturnNullWhenUserExists()
        {
            //Arrange
            var userService = new UserService(dbContext, passwordHasher.Object, mapper.Object, jwtService.Object);

            var request = new CreateUserRequest
            {
                Username = "test1",
                Email = "test1"
            };

            //Act
            var result = await userService.CreateUser(request);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ShouldReturnJwtWhenUserIsCreated()
        {
            //Arrange
            mapper.Setup(m => m.Map<CreateUserRequest, User>(It.IsAny<CreateUserRequest>())).Returns(new User { Id = new Guid(), Email = "test" });
            jwtService.Setup(j => j.CreateToken(It.IsAny<Guid>(), It.IsAny<string>())).Returns(new JwtResponse());
            var userService = new UserService(dbContext, passwordHasher.Object, mapper.Object, jwtService.Object);

            var request = new CreateUserRequest
            {
                Username = "test2",
                Email = "test2"
            };

            //Act
            var result = await userService.CreateUser(request);

            //Assert
            Assert.NotNull(result);
        }
    }
}

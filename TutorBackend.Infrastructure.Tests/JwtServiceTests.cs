using System;
using TutorBackend.Infrastructure.Services;
using Xunit;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using TutorBackend.Infrastructure.Settings;

namespace TutorBackend.Infrastructure.Tests
{
    public class JwtServiceTests
    {
        [Theory]
        [InlineData(10)]
        [InlineData(60)]
        public void ShouldReturnTokenWithMinutesToExpiration(int expiryMinutes)
        {
            //Arrange
            var jwtSettings = new JwtSettings
            {
                Key = "ABCDdsfsie34dsd5",
                Issuer = "test",
                ExpiryMinutes = expiryMinutes
            };
            var jwtOptions = Options.Create(jwtSettings);
            var jwtService = new JwtService(jwtOptions);

            //Act
            var jwt = jwtService.CreateToken(Guid.NewGuid(), "test");

            //Assert
            Assert.True(jwt.Expires < DateTime.Now.AddMinutes(expiryMinutes));
            var token = new JwtSecurityTokenHandler().ReadJwtToken(jwt.Token);
            Assert.True(token.ValidTo < DateTime.Now.AddMinutes(expiryMinutes));
        }
    }
}

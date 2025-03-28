using DrugIndication.Application.Model;
using DrugIndication.Application.Services;
using DrugIndication.Domain.Entities;
using Microsoft.Extensions.Options;
using Moq;

namespace DrugIndication.UnitTests
{
    public class JwtServiceTests
    {
        [Fact]
        public async Task GenerateToken_ShouldReturnValidJwtToken()
        {
            // Arrange
            var settings = new JwtSettings { Key = "this-is-a-super-secret-key-12345" };

            var optionsMock = new Mock<IOptions<JwtSettings>>();
            optionsMock.Setup(x => x.Value).Returns(settings);

            var jwtService = new JwtService(optionsMock.Object);

            var user = new User
            {
                Id = "123",
                Email = "admin@test.com",
                Role = "Admin"
            };

            // Act
            var token = await jwtService.GenerateToken(user);

            // Assert
            Assert.False(string.IsNullOrEmpty(token));
        }
    }
}

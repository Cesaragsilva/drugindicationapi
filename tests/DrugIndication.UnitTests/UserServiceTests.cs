using DrugIndication.Application.Dtos;
using DrugIndication.Application.Interfaces.Repositories;
using DrugIndication.Application.Interfaces.Services;
using DrugIndication.Application.Services;
using DrugIndication.Domain.Entities;
using FluentAssertions;
using Moq;

namespace DrugIndication.UnitTests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<IJwtService> _jwtServiceMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _jwtServiceMock = new Mock<IJwtService>();
            _userService = new UserService(_userRepoMock.Object, _jwtServiceMock.Object);
        }

        [Fact]
        public async Task CreateUser_ShouldFail_WhenUserAlreadyExists()
        {
            var dto = new CreateUserDto { Email = "test@test.com", Password = "123" };
            _userRepoMock.Setup(r => r.GetByEmailAsync(dto.Email, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(new User());

            var result = await _userService.CreateUser(dto, CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Contain("User already exists.");
        }

        [Fact]
        public async Task CreateUser_ShouldSucceed_WhenUserIsNew()
        {
            var dto = new CreateUserDto { Email = "test@test.com", Password = "123" };
            _userRepoMock.Setup(r => r.GetByEmailAsync(dto.Email, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(value: null);

            var result = await _userService.CreateUser(dto, CancellationToken.None);

            _userRepoMock.Verify(r => r.CreateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task GenerateToken_ShouldFail_WhenUserNotFound()
        {
            var dto = new UserDto { Email = "notfound@test.com", Password = "123" };
            _userRepoMock.Setup(r => r.GetByEmailAsync(dto.Email, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(value: null);

            var result = await _userService.GenerateToken(dto, CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Contain("Invalid credentials.");
        }

        [Fact]
        public async Task GenerateToken_ShouldFail_WhenPasswordDoesNotMatch()
        {
            var dto = new UserDto { Email = "user@test.com", Password = "wrongpass" };
            var user = new User(dto.Email, "_wrong");

            _userRepoMock.Setup(r => r.GetByEmailAsync(dto.Email, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(user);

            var result = await _userService.GenerateToken(dto, CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Contain("Invalid credentials.");
        }

        [Fact]
        public async Task GenerateToken_ShouldSucceed_WhenCredentialsAreValid()
        {
            var dto = new UserDto { Email = "valid@test.com", Password = "123" };
            var user = new User(dto.Email, "123");

            _userRepoMock.Setup(r => r.GetByEmailAsync(dto.Email, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(user);

            _jwtServiceMock.Setup(j => j.GenerateToken(user)).ReturnsAsync("token-abc");

            var result = await _userService.GenerateToken(dto, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data!.AccessToken.Should().Be("token-abc");
        }
    }
}

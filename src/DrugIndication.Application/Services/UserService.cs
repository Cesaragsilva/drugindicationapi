using DrugIndication.Application.Dtos;
using DrugIndication.Application.Interfaces.Repositories;
using DrugIndication.Application.Interfaces.Services;
using DrugIndication.Application.Model;

namespace DrugIndication.Application.Services
{
    public class UserService(IUserRepository userRepository, IJwtService jwtService) : IUserService
    {
        public async Task<ResultService> CreateUser(CreateUserDto createUserDto, CancellationToken cancellationToken)
        {
            var currentUser = await userRepository.GetByEmailAsync(createUserDto.Email, cancellationToken);
            if (currentUser is not null)
                return ResultService.Fail("User already exists.");

            var user = createUserDto.ToEntity();
            await userRepository.CreateAsync(user, cancellationToken);

            return ResultService.Ok();
        }

        public async Task<ResultService<JwtDto>> GenerateToken(UserDto userLoginDto, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByEmailAsync(userLoginDto.Email, cancellationToken);
            if (user is null || !user.Password.Equals(userLoginDto.GetPasswordHash()))
                return ResultService.Fail<JwtDto>("Invalid credentials.");

            var token = await jwtService.GenerateToken(user);

            return ResultService.Ok(new JwtDto(token));
        }
    }
}

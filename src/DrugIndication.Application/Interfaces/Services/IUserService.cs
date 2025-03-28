using DrugIndication.Application.Dtos;
using DrugIndication.Application.Model;

namespace DrugIndication.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<ResultService<JwtDto>> GenerateToken(UserDto userLoginDto, CancellationToken cancellationToken);
        Task<ResultService> CreateUser(CreateUserDto createUserDto, CancellationToken cancellationToken);
    }
}

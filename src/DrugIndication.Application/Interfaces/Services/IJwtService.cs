using DrugIndication.Domain.Entities;

namespace DrugIndication.Application.Interfaces.Services
{
    public interface IJwtService
    {
        Task<string> GenerateToken(User user);
    }
}

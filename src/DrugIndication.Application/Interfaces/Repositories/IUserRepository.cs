using DrugIndication.Domain.Entities;

namespace DrugIndication.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task CreateAsync(User user, CancellationToken cancellationToken);
    }
}

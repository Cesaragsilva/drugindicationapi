using DrugIndication.Domain.Entities;

namespace DrugIndication.Application.Interfaces.Repositories
{
    public interface IProgramRepository
    {
        Task<IEnumerable<Program>> GetAllAsync(CancellationToken cancellationToken);
        Task<Program?> GetByIdAsync(string id, CancellationToken cancellationToken);
        Task CreateAsync(Program program, CancellationToken cancellationToken);
        Task UpdateAsync(Program program, CancellationToken cancellationToken);
        Task DeleteAsync(string id, CancellationToken cancellationToken);
    }
}

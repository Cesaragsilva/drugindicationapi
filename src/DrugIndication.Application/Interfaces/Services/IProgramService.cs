using DrugIndication.Application.Dtos;
using DrugIndication.Application.Model;
using DrugIndication.Domain.Entities;

namespace DrugIndication.Application.Interfaces.Services
{
    public interface IProgramService
    {
        Task<IEnumerable<Program>> GetAllProgramAsync(CancellationToken cancellationToken);
        Task<ResultService<Program>> GetProgramByIdAsync(string id, CancellationToken cancellationToken);
        Task<ResultService<Program>> CreateProgramAsync(Program programDto, CancellationToken cancellationToken);
        Task<ResultService<Program>> UpdateProgramAsync(string id, Program programDto, CancellationToken cancellationToken);
        Task<ResultService> DeleteProgramAsync(string id, CancellationToken cancellationToken);
    }
}

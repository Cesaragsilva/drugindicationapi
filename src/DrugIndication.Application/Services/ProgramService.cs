using DrugIndication.Application.Interfaces.Repositories;
using DrugIndication.Application.Interfaces.Services;
using DrugIndication.Application.Model;
using DrugIndication.Domain.Constants;
using DrugIndication.Domain.Entities;

namespace DrugIndication.Application.Services
{
    public class ProgramService(IProgramRepository programRepository) : IProgramService
    {
        public async Task<ResultService<Program>> GetProgramByIdAsync(string id, CancellationToken cancellationToken)
        {
            var program = await programRepository.GetByIdAsync(id, cancellationToken);
            if (program is null)
                return ResultService.Fail<Program>(ErrorMessages.NotFound);

            return ResultService.Ok(program);
        }

        public async Task<IEnumerable<Program>> GetAllProgramAsync(CancellationToken cancellationToken)
        {
            var programs = await programRepository.GetAllAsync(cancellationToken);

            return programs;
        }
        public async Task<ResultService<Program>> CreateProgramAsync(Program programDto, CancellationToken cancellationToken)
        {
            var program = await programRepository.GetByIdAsync(programDto.Id, cancellationToken);
            if (program is not null)
                return ResultService.Fail<Program>(ErrorMessages.AlreadyExist);

            await programRepository.CreateAsync(programDto, cancellationToken);

            return ResultService.Ok(programDto);
        }

        public async Task<ResultService<Program>> UpdateProgramAsync(string id, Program program, CancellationToken cancellationToken)
        {
            var currentProgram = await programRepository.GetByIdAsync(id, cancellationToken);
            if (currentProgram is null)
                return ResultService.Fail<Program>(ErrorMessages.NotFound);

            currentProgram = program;
            await programRepository.UpdateAsync(currentProgram, cancellationToken);

            return ResultService.Ok(currentProgram);
        }

        public async Task<ResultService> DeleteProgramAsync(string id, CancellationToken cancellationToken)
        {
            var program = await programRepository.GetByIdAsync(id, cancellationToken);
            if (program is null)
                return ResultService.Fail<Program>(ErrorMessages.NotFound);

            await programRepository.DeleteAsync(id, cancellationToken);

            return ResultService.Ok();
        }
    }
}

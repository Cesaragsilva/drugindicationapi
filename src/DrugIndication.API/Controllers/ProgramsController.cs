using DrugIndication.Application.Interfaces.Services;
using DrugIndication.Application.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrugIndication.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class ProgramsController(IProgramService programService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ResultService<Domain.Entities.Program>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultService), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] Domain.Entities.Program programModel, CancellationToken cancellationToken)
    {
        var result = await programService.CreateProgramAsync(programModel, cancellationToken);
        if (!result.IsSuccess)
            return NotFound(result);

        return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(IEnumerable<Domain.Entities.Program>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await programService.GetAllProgramAsync(cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ResultService<Domain.Entities.Program>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultService), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var result = await programService.GetProgramByIdAsync(id, cancellationToken);
        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ResultService<Domain.Entities.Program>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultService), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string id, [FromBody] Domain.Entities.Program programModel, CancellationToken cancellationToken)
    {
        var result = await programService.UpdateProgramAsync(id, programModel, cancellationToken);
        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ResultService), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultService), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var result = await programService.DeleteProgramAsync(id, cancellationToken);
        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }
}
using DrugIndication.Application.Dtos;
using DrugIndication.Application.Interfaces.Services;
using DrugIndication.Application.Model;
using Microsoft.AspNetCore.Mvc;

namespace DrugIndication.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IUserService userService) : ControllerBase
    {
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResultService), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] CreateUserDto userDto, CancellationToken cancellationToken)
        {
            var result = await userService.CreateUser(userDto, cancellationToken);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok();
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResultService), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CreateUserDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] UserDto userDto, CancellationToken cancellationToken)
        {
            var result = await userService.GenerateToken(userDto, cancellationToken);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result.Data);
        }
    }
}

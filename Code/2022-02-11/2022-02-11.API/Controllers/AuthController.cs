using _2022_02_11.Entities.Models;
using _2022_02_11.Services;
using Microsoft.AspNetCore.Mvc;

namespace _2022_02_11.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public AuthController(IUsersService usersSerivce)
        {
            _usersService = usersSerivce;
        }

        [ProducesResponseType(200, Type = typeof(LoginResponse))]
        [ProducesResponseType(400, Type = typeof(LoginResponse))]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            var result = await _usersService.GenerateTokenAsync(model);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [ProducesResponseType(200, Type = typeof(OperationResponse<string>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<string>))]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            var result = await _usersService.RegisterUserAsync(model);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
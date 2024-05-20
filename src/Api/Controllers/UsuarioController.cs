using Api.Dtos.Login;
using Api.Dtos.UsuarioRegister;
using Api.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class UsuarioController: ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public UsuarioController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register
        (
            [FromBody] UsuarioRegisterRequestDto request
        )
        {
            return await _authenticationService.RegisterAsync(request);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login
        (
            [FromBody] LoginRequestDto request
        )
        {
            return await _authenticationService.LoginAsync(request);
        }
    }
}
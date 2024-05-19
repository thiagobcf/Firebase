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
    }
}
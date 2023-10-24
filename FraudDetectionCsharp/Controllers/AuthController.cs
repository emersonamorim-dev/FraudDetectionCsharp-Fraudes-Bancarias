using FraudDetectionCsharp.Domain.entities;
using FraudDetectionCsharp.Domain.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FraudDetectionCsharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authenticationService.AuthenticateAsync(request.Username, request.Password);

            if (result.IsAuthenticated)
            {
                return Ok(new { result.Token });
            }

            return Unauthorized(result.ErrorMessage);
        }
    }
}

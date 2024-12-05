using Core.Models.DTO;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signIn")]
        public async Task<ActionResult<string>> SignIn(SignInCustomerDTO signInCustomerDTO)
        {
            string token = await _authService.SignIn(signInCustomerDTO.Email, signInCustomerDTO.Password);

            return CreatedAtAction(nameof(SignIn), token);
        }
    }
}

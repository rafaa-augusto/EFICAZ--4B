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
        public async Task<IActionResult> SignIn(SignInCustomerDTO signInCustomerDTO)
        {
            try
            {
                var result = await _authService.SignIn(signInCustomerDTO.Email, signInCustomerDTO.Password);

                var returno = new
                {
                    Token = result.Item1,
                    UserId = result.Item2,
                };

                return Ok(returno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

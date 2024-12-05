using Core.Models;
using Core.Repositories;
using Core.Services;
using System.Security.Claims;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IAuthRepository authRepository, ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
        }

        public async Task<string> SignIn(string email, string password)
        {
            UsuarioModels usuario = await GetUsuarioByEmailAndPassword(email, password);
            string token = _tokenService.CreateToken(usuario);

            return token;
        }

        private async Task<UsuarioModels> GetUsuarioByEmailAndPassword(string email, string password)
        {
            UsuarioModels? usuario = await _authRepository.GetUsuarioByEmailAndPassword(email, password);
            if (usuario == null)
            {
                throw new Exception("Usuário e/ou senha inválidos");
            }

            return usuario;
        }

        public string GetAuthenticatedUserId(ClaimsPrincipal User)
        {
            string? userId = User.Claims.First(c => c.Type == "id")?.Value;
            if (userId == null)
            {
                throw new Exception("User not found on token JWT");
            }

            return userId;
        }
    }
}

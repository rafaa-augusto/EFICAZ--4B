using Core.Models;

namespace Core.Services
{
    public interface ITokenService
    {
        string CreateToken(UsuarioModels usuario);
    }
}
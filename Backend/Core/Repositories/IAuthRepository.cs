using Core.Models;

namespace Core.Repositories
{
    public interface IAuthRepository
    {
        public Task<UsuarioModels?> GetUsuarioByEmailAndPassword(string email, string password);
    }
}

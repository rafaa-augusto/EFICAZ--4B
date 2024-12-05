using Core.Models;
using Core.Repositories;
using Infrastructure.Repositories.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly CadastroUsuariosDbContext _context;

        public AuthRepository(CadastroUsuariosDbContext context)
        {
            _context = context;
        }

        public async Task<UsuarioModels?> GetUsuarioByEmailAndPassword(string email, string password)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(c => c.Email == email && c.Senha == password);
        }
    }
}

using Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core.Repositories
{
    public interface IUsuarioRepository
    {
        public  Task<UsuarioModels> AddUsuario(UsuarioModels usuario);
        public Task<UsuarioModels?> GetUsuario(string usuarioId);
        Task<UsuarioModels> AtualizarUsuario(UsuarioModels usuarioAtualizado);
        Task<IEnumerable<UsuarioModels>> GetUsuarios();
        Task UpdateUsuario();
    }
}

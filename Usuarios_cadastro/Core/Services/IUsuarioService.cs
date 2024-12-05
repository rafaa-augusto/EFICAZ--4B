using System.Threading.Tasks;
using Core.Models;
using Core.Repositories;

namespace Core.Services {
    public interface IUsuarioService {
        Task<UsuarioModels> RegistrarUsuario(UsuarioModels novoUsuario, List<EnderecoModels> enderecos);
        Task<UsuarioModels> GetUsuarioOrThrowException(string usuarioId);
        Task<IEnumerable<UsuarioModels>>GetUsuarios();
        Task AtualizarUsuario(UsuarioModels usuarioAtualizado);
        Task<string> UploadUsuarioImage(string usuarioId, FileData file);
    }
}

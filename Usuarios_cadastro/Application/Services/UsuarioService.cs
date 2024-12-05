using Core.Models; 
using Core.Repositories;
using Core.Services;

namespace Application.Services {
    public class UsuarioService : IUsuarioService {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEnderecoService _enderecoService;
        private readonly IImageService _imageService;

        public UsuarioService(IUsuarioRepository usuarioRepository, IEnderecoService enderecoService, IImageService imageService)
        {
            _usuarioRepository = usuarioRepository;
            _enderecoService = enderecoService;
            _imageService = imageService;
        }

        private async Task<UsuarioModels?> GetUsuario(string usuarios) {
            return await _usuarioRepository.GetUsuario(usuarios);
        }

        public async Task<string> UploadUsuarioImage(string usuarioId, FileData file)
        {
            UsuarioModels usuario = await GetUsuario(usuarioId);

            string uploadedFileUrl = await _imageService.UploadImage(file, "usuarios", usuarioId);

            usuario.ImageUrl = uploadedFileUrl;

            await _usuarioRepository.UpdateUsuario();

            return uploadedFileUrl;
        }

        public async Task<UsuarioModels> GetUsuarioOrThrowException(string usuarioId) {
			UsuarioModels? usuario = await GetUsuario(usuarioId);
			if (usuario == null) {
				throw new Exception("Usuário não encontrado");
			}
			return usuario;
		}

		public async Task<UsuarioModels> RegistrarUsuario(UsuarioModels novoUsuario, List<EnderecoModels> enderecos) {
            if (novoUsuario == null) {
                throw new ArgumentNullException(nameof(novoUsuario), "Usuário não pode ser nulo");
            }

            await _usuarioRepository.AddUsuario(novoUsuario);
            await _enderecoService.RegistrarEnderecos(enderecos);

            return novoUsuario;
        }


        public async Task AtualizarUsuario(UsuarioModels usuarioAtualizado)
        {
            await _usuarioRepository.AtualizarUsuario(usuarioAtualizado);
            await _enderecoService.AtualizarEnderecos(usuarioAtualizado.Enderecos);
        }

        public async Task<IEnumerable<UsuarioModels>> GetUsuarios()
        {
            return await _usuarioRepository.GetUsuarios();
        }

		
	}
}

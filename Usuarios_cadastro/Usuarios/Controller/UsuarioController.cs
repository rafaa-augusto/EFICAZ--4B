using Microsoft.AspNetCore.Mvc;
using Core.Models.DTO;
using Core.Services;
using Core.Models;
using System.Runtime.ConstrainedExecution;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarUsuario([FromBody] UsuarioDto usuarioDto)
        {
            if (usuarioDto == null)
                return BadRequest("Usuário não pode ser nulo.");


            var usuario = new UsuarioModels(Guid.NewGuid().ToString(), usuarioDto.Nome, usuarioDto.Sobrenome, usuarioDto.CPF, usuarioDto.Telefone, usuarioDto.Genero, DateTime.Now, usuarioDto.DataNascimento, usuarioDto.Nacionalidade, "admin", usuarioDto.Email, usuarioDto.Senha, null);

            var enderecos = new List<EnderecoModels>();
            foreach (var endereco in usuarioDto.Enderecos)
                enderecos.Add(new EnderecoModels(Guid.NewGuid().ToString(), usuario.Id, endereco.Logradouro, endereco.Numero, endereco.Complemento, endereco.Bairro, endereco.Cidade, endereco.Estado, endereco.CEP));

            await _usuarioService.RegistrarUsuario(usuario, enderecos);

            return CreatedAtAction(nameof(GetUsuarios), new { id = usuario.Id }, usuarioDto);
        }

        [HttpPost("{usuarioId}/UploadImage")]
        public async Task<ActionResult<string>> UploadImage(string usuarioId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No image found");
            }

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var fileData = new FileData
            {
                FileName = file.FileName,
                Content = memoryStream.ToArray(),
                ContentType = file.ContentType,
                Extension = Path.GetExtension(file.FileName),
            };

            string imageUrl = await _usuarioService.UploadUsuarioImage(usuarioId, fileData);

            return CreatedAtAction(nameof(UploadImage), imageUrl);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                IEnumerable<UsuarioModels> usuarios = await _usuarioService.GetUsuarios();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarUsuario(string id, [FromBody] UsuarioDto usuarioDto)
        {
            if (usuarioDto == null)
            {
                return BadRequest("Dados do usuário não podem ser nulos.");
            }

            try
            {
                var usuarioExistente = await _usuarioService.GetUsuarioOrThrowException(id);

                usuarioExistente.Nome = usuarioDto.Nome;
                usuarioExistente.Sobrenome = usuarioDto.Sobrenome;
                usuarioExistente.CPF = usuarioDto.CPF;
                usuarioExistente.Telefone = usuarioDto.Telefone;
                usuarioExistente.Genero = usuarioDto.Genero;
                usuarioExistente.DataNascimento = usuarioDto.DataNascimento;
                usuarioExistente.Nacionalidade = usuarioDto.Nacionalidade;
                usuarioExistente.Email = usuarioDto.Email;


                if (usuarioExistente.Enderecos != null)
                {
                    foreach (var endereco in usuarioExistente.Enderecos)
                    {
                        var enderecoAtualizar = (from i in usuarioDto.Enderecos where i.IdEndereco == endereco.Id select i).FirstOrDefault();
                        if (enderecoAtualizar != null)
                        {
                            endereco.Logradouro = enderecoAtualizar.Logradouro;
                            endereco.Numero = enderecoAtualizar.Numero;
                            endereco.Complemento = enderecoAtualizar.Complemento;
                            endereco.Bairro = enderecoAtualizar.Bairro;
                            endereco.Cidade = enderecoAtualizar.Cidade;
                            endereco.Estado = enderecoAtualizar.Estado;
                            endereco.CEP = enderecoAtualizar.CEP;
                        }
                    }
                }
                await _usuarioService.AtualizarUsuario(usuarioExistente);

                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

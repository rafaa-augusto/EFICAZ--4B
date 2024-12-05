using Core.Models;
using Core.Repositories;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecoService(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        public async Task<List<EnderecoModels>> RegistrarEnderecos(List<EnderecoModels> novoEnderecos)
        {
            if (novoEnderecos == null)
            {
                throw new ArgumentNullException(nameof(novoEnderecos), "Endereco não pode ser nulo");
            }

            await _enderecoRepository.AddEndereco(novoEnderecos);

            return novoEnderecos;
        }

        public async Task<EnderecoModels> AtualizarEnderecos(List<EnderecoModels> enderecoAtualizado)
        {
            await _enderecoRepository.AtualizarEnderecos(enderecoAtualizado);
            return enderecoAtualizado.FirstOrDefault();
        }
    }
}

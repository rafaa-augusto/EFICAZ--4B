using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IEnderecoRepository
    {
        Task<List<EnderecoModels>> AddEndereco(List<EnderecoModels> enderecos);
        Task<EnderecoModels> AtualizarEnderecos(List<EnderecoModels> enderecoAtualizado);
    }
}

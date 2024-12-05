using Core.Models;
using Core.Repositories;
using Infrastructure.Repositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly CadastroUsuariosDbContext _context;

        public EnderecoRepository(CadastroUsuariosDbContext context)
        {
            _context = context;
        }

        public async Task<List<EnderecoModels>> AddEndereco(List<EnderecoModels> enderecos)
        {
            _context.Enderecos.AddRange(enderecos);
            await _context.SaveChangesAsync();
            return enderecos;
        }

        public async Task<EnderecoModels> AtualizarEnderecos(List<EnderecoModels> enderecoAtualizado)
        {
            _context.Enderecos.UpdateRange(enderecoAtualizado);
            await _context.SaveChangesAsync();
            return enderecoAtualizado.FirstOrDefault();
        }
    }
}

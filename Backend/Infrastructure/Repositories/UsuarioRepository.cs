using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Core.Repositories;
using Infrastructure.Repositories.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly List<UsuarioModels> _usuarios;

        private readonly CadastroUsuariosDbContext _context;

        public UsuarioRepository(CadastroUsuariosDbContext context)
        {
            _usuarios = new List<UsuarioModels>();
            _context = context;
        }

        public async Task<UsuarioModels> AddUsuario(UsuarioModels usuario)
        {
            _usuarios.Add(usuario);
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task UpdateUsuario()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<UsuarioModels?> GetUsuario(string usuarioId)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId);
            if (usuario != null)
                usuario.Enderecos = (from i in _context.Enderecos where i.UsuarioId == usuario.Id select i).ToList();   

            return await Task.FromResult(usuario);
        }

        public async Task<IEnumerable<UsuarioModels>> GetUsuarios()
        {
            IEnumerable<UsuarioModels> usuarios = _context.Usuarios.ToList();
            (from i in usuarios 
             let addEndereco = i.Enderecos = (from j in _context.Enderecos where j.UsuarioId == i.Id select j).ToList()
             select i).ToList();
            return await Task.FromResult(usuarios);
        }

        public async Task<UsuarioModels> AtualizarUsuario(UsuarioModels usuarioAtualizado)
        {
            _context.Usuarios.Update(usuarioAtualizado);
            await _context.SaveChangesAsync();
            return usuarioAtualizado;
        }
    }
}

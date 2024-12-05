using Core.DTOs;
using Microsoft.AspNetCore.Http;
using System;

namespace Core.Models.DTO
{
    public class UsuarioDto
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Genero { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Nacionalidade { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public List<EnderecoDto> Enderecos {  get; set; } 

        public UsuarioDto() { }

        public UsuarioDto(string nome, string sobrenome, string cPF, string telefone, string genero, DateTime dataNascimento, string nacionalidade, string email, string senha, List<EnderecoDto> enderecos)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            CPF = cPF;
            Telefone = telefone;
            Genero = genero;
            DataNascimento = dataNascimento;
            Nacionalidade = nacionalidade;
            Email = email;
            Senha = senha;
            Enderecos = enderecos;
        }
    }
}

using System;

namespace Core.Models {
    public class UsuarioModels {

        public string Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Genero { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Nacionalidade { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string? ImageUrl { get; set; }

        public List<EnderecoModels> Enderecos { get; set; }

        public UsuarioModels() { }

        public UsuarioModels(string id, string nome, string sobrenome, string cPF, string telefone, string genero, DateTime dataCadastro, DateTime dataNascimento, string nacionalidade, string role, string email, string senha, string? imageUrl)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            CPF = cPF;
            Telefone = telefone;
            Genero = genero;
            DataCadastro = dataCadastro;
            DataNascimento = dataNascimento;
            Role = role;
            Nacionalidade = nacionalidade;
            Email = email;
            Senha = senha;
            ImageUrl = imageUrl;
        }
    }
}

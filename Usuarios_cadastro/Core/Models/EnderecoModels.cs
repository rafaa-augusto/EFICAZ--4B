using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class EnderecoModels
    {
        public string Id { get; set; }
        public string UsuarioId { get; set; } 
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }

        public UsuarioModels Usuario { get; set; }
        public EnderecoModels()
        {

        }
 
            public EnderecoModels(string id, string usuarioId, string logradouro, string numero, string complemento, string bairro, string cidade, string estado, string cEP)
        {
            Id = id;
            UsuarioId = usuarioId;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            CEP = cEP;
        }
    }
}

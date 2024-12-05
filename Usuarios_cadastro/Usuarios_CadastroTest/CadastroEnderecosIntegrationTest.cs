using Core.Models;
using Infrastructure.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;
using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Usuarios_CadastroTest
{
    internal class CadastroEnderecosIntegrationTest
    {

    }



namespace Usuarios_CadastroTest
{
    public class CadastroEnderecosIntegrationTest : IDisposable
    {
        public readonly CadastroUsuariosDbContext _context;
        public CadastroEnderecosIntegrationTest()
        {
            // Configurar o contexto para usar o banco de dados MySQL real para testes
            var options = new DbContextOptionsBuilder<CadastroUsuariosDbContext>()
               .UseMySql("Server=localhost;Database=usuarios;User=root;Password='';", new MySqlServerVersion(new Version(8, 0, 0))).Options;
            _context = new CadastroUsuariosDbContext(options);

            // Para limpar a base de dados antes de cada execução de teste adicione:
            // _context.Database.EnsureCreated();
        }
        [Fact]
        public void AddEnderecoTest()
        {

                // Arrange
                var newUser = new EnderecoModels
                {
                    Id = "1878",
                    UsuarioId = "1878",
                    Logradouro = "Sampaio Vidal",
                    Numero = "48",
                    Complemento = "Casa", Bairro = "Centro", Cidade = "Alvinlândia", CEP = "154644878", Estado = "SP"
                                              };
             
                // Act
                _context.Enderecos.Add(newUser);
            _context.SaveChanges();
            // Assert
            //var savedProd = _context.Produtos.FirstOrDefault(u => u.Descricao == "Produto Teste Integracao");
           // Assert.NotNull(savedProd);
          //  Assert.Equal(876, savedProd.Id);
        }
        [Fact]
     

        public void Dispose()
        {
            // Limpar o banco de dados após os testes
            //_context.Database.EnsureDeleted();
            // _context.Dispose();
        }
    }

}

}

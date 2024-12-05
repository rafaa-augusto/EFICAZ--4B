using Core.Models;
using Infrastructure.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Usuarios_CadastroTest
{
    public class CadastroUsuarioIntegrationTest : IDisposable
    {
        public readonly CadastroUsuariosDbContext _context;
        public CadastroUsuarioIntegrationTest()
        {
            // Configurar o contexto para usar o banco de dados MySQL real para testes
            var options = new DbContextOptionsBuilder<CadastroUsuariosDbContext>()
               .UseMySql("Server=localhost;Database=usuarios;User=root;Password='';", new MySqlServerVersion(new Version(8, 0, 0))).Options;
            _context = new CadastroUsuariosDbContext(options);

            // Para limpar a base de dados antes de cada execução de teste adicione:
            // _context.Database.EnsureCreated();
        }
        [Fact]
        public void AddUsuarioTest()
        {

            // Arrange
            var newUser = new UsuarioModels
            {
                Id = "1878",
                Nome = "Usuario Teste2",
                Sobrenome = "Sobrenome",
                CPF="867868686",Email="ddddd@asaaa.com",Genero="AA", 
                                              Nacionalidade="BRA", Role="Adm", Senha="123",Telefone="6745644" };
            // Act
            _context.Usuarios.Add(newUser);
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

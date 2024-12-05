describe('Teste de Registro no Vue.js', () => {
    it('Deve completar o processo de registro com sucesso', () => {
      // Acessa a aplicação
      cy.visit('http://localhost:5173/register'); // Atualize para o URL correto
  
      // Passo 1: Preenche os campos iniciais
      cy.get('input[name="nomeCompleto"]').type('João da Silva');
      cy.get('input[name="email"]').type('joao.silva@example.com');
      cy.get('input[name="cel"]').type('11987654321');
      cy.get('input[name="userName"]').type('joaosilva');
      cy.contains('Próximo').click();
  
      // Passo 2: Preenche o endereço
      cy.get('input[name="cep"]').type('01001000').blur(); // Simula a saída do campo
      cy.get('input[name="numero"]').type('123');
      cy.contains('Próximo').click();
  
      // Passo 3: Preenche os dados restantes
      cy.get('input[name="password"]').type('senhaSegura123');
      cy.get('input[name="confirmPassword"]').type('senhaSegura123');
      cy.get('input[name="date"]').type('1990-01-01');
      cy.get('input[name="cpf"]').type('12345678900');
      cy.get('input[name="url"]').type('https://example.com/foto.jpg');
      cy.contains('Próximo').click();
  
      // Passo 4: Verifica o resumo e finaliza
      cy.contains('Cadastrar').click();
  
      // Verifica redirecionamento para login
      cy.url().should('include', '/login');
    });
  });
  
<template>
  <div class="edit-form">
    <div class="header">
      <h1>Editar <br> Informações</h1>
    </div>
    <br>
    <form>
      <div class="form-group">
        <label for="nomeCompleto">Nome completo:</label>
        <input
          type="text"
          id="nomeCompleto"
          class="form-control"
          v-model="formData.nomeCompleto"
        />
      </div>
      <div class="form-group">
        <label for="email">Email:</label>
        <input
          type="email"
          id="email"
          class="form-control"
          v-model="formData.email"
        />
      </div>
      <div class="form-group">
        <label for="telefone">Telefone:</label>
        <input
          type="text"
          id="telefone"
          class="form-control"
          v-model="formData.telefone"
        />
      </div>
      <div class="form-group">
        <label for="nomeUsuario">Nome de usuário:</label>
        <input
          type="text"
          id="nomeUsuario"
          class="form-control"
          v-model="formData.nomeUsuario"
        />
      </div>
      <div class="form-group">
        <label for="dataNascimento">Data de nascimento:</label>
        <input
          type="date"
          id="dataNascimento"
          class="form-control"
          v-model="formData.dataNascimento"
        />
      </div>
      <div class="form-group">
        <label for="cpf">CPF:</label>
        <input
          type="text"
          id="cpf"
          class="form-control"
          v-model="formData.cpf"
        />
      </div>

  
      <div v-for="(endereco, index) in formData.endereco" :key="index" class="address-item">
        <div class="form-group">
          <label :for="'rua-' + index">Rua:</label>
          <input
            type="text"
            :id="'rua-' + index"
            class="form-control"
            v-model="endereco.rua"
          />
        </div>
        <div class="form-group">
          <label :for="'numero-' + index">Número:</label>
          <input
            type="text"
            :id="'numero-' + index"
            class="form-control"
            v-model="endereco.numero"
          />
        </div>
        <div class="form-group">
          <label :for="'complemento-' + index">Complemento:</label>
          <input
            type="text"
            :id="'complemento-' + index"
            class="form-control"
            v-model="endereco.complemento"
          />
        </div>
        <div class="form-group">
          <label :for="'cidade-' + index">Cidade:</label>
          <input
            type="text"
            :id="'cidade-' + index"
            class="form-control"
            v-model="endereco.cidade"
          />
        </div>
        <div class="form-group">
          <label :for="'estado-' + index">Estado:</label>
          <input
            type="text"
            :id="'estado-' + index"
            class="form-control"
            v-model="endereco.estado"
          />
        </div>
        <div class="form-group">
          <label :for="'cep-' + index">CEP:</label>
          <input
            type="text"
            :id="'cep-' + index"
            class="form-control"
            v-model="endereco.cep"
          />
        </div>
      </div>

     
      <div class="mt-4">
        <button type="button" class="btn" @click="saveChanges">
          Salvar
        </button>
      </div>
    </form>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
      return {
          formData: {
              nomeCompleto: "",
              email: "",
              telefone: "",
              nomeUsuario: "",
              dataNascimento: "",
              cpf: "",
              endereco: [
                  {
                      rua: "",
                      numero: "",
                      complemento: "",
                      cidade: "",
                      estado: "",
                      cep: "",
                  },
              ],
          },
      };
  },
  methods: {
      async getUserData() {
          const token = localStorage.getItem('authToken');
          const userId = localStorage.getItem('userId');

          if (token && userId) {
              try {
                  const response = await axios.get(`http://localhost:5081/api/Usuario/${userId}`, {
                      headers: {
                          Authorization: `Bearer ${token}`,
                      },
                  });

                  const userData = response.data;
                  this.formData.nomeCompleto = `${userData.nome} ${userData.sobrenome}`;
                  this.formData.email = userData.email;
                  this.formData.telefone = userData.telefone;
                  this.formData.nomeUsuario = userData.nome;  
                  this.formData.dataNascimento = new Date(userData.dataNascimento).toISOString().split('T')[0];
                  this.formData.cpf = userData.cpf;
                  if (userData.enderecos && userData.enderecos.length > 0) {
                      this.formData.endereco = userData.enderecos.map(endereco => ({
                          rua: endereco.logradouro,
                          numero: endereco.numero,
                          complemento: endereco.complemento,
                          cidade: endereco.cidade,
                          estado: endereco.estado,
                          cep: endereco.cep,
                      }));
                  }
              } catch (error) {
                  console.error("Erro ao buscar dados do usuário:", error);
              }
          }
      },
      saveChanges() {
          alert("Alterações salvas com sucesso!");
          this.$router.push({ name: "profile" });
      },
  },
  mounted() {
      this.getUserData();
  },
};
</script>

<style scoped>
.edit-form {
  font-family: Arial, sans-serif;
  padding: 20px;
  max-width: 600px;
  margin: 0 auto;
  text-align: left !important;
}
.btn:hover {
  filter: invert(1);
}

.header {
  text-align: center;
  margin-bottom: 20px;
}

.form-group {
  margin-bottom: 15px;
}

label {
  margin-bottom: 15px;
}

.form-control {
  width: 100%;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 5px;
}

input {
  padding: 14px 28px !important;
  border-radius: 30px !important;
  border: none;
  background: rgb(239, 239, 239) !important;
}

.btn {
  padding: 14px 28px;
  border-radius: 30px;
  transition: 400ms;
  background: #fff;
  color: black;
  font-weight: bold;
  border: 1px solid black;
  cursor: pointer;
}

h1 {
  font-size: 40px !important;
}
</style>
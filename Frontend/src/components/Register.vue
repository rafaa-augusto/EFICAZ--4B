<script>
import axios from "axios";

export default {
  data() {
    return {
      currentStep: 0,
      confirmPassword: "",
      successMessage: "",
      errorMessage: "",
      formData: {
        nomeCompleto: "",
        email: "",
        telefone: "",
        nomeDeUsuario: "",
        senha: "",
        dataNascimento: "",
        cpf: "",
        ativo: true,
        fotoPerfil: null, // Novo campo para armazenar o arquivo da foto
        enderecos: [
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
  computed: {
    filteredFormData() {
      const { senha, ativo, fotoPerfil, enderecos, ...filtered } = this.formData;

      if (enderecos && enderecos.length > 0) {
        filtered.enderecos = `${enderecos[0].rua}, ${enderecos[0].numero}, ${
          enderecos[0].complemento ? enderecos[0].complemento + ", " : ""
        }${enderecos[0].cidade} - ${enderecos[0].estado}, CEP: ${enderecos[0].cep}`;
      } else {
        filtered.enderecos = "Endereço não informado";
      }

      return filtered;
    },
  },
  methods: {
    async buscarEnderecoPorCEP() {
      const cep = this.formData.enderecos[0].cep.replace(/\D/g, "");

      if (cep.length === 8) {
        try {
          const response = await axios.get(`https://viacep.com.br/ws/${cep}/json/`);

          if (response.data.erro) {
            alert("CEP não encontrado.");
          } else {
            this.formData.enderecos[0].rua = response.data.logradouro || "";
            this.formData.enderecos[0].complemento = response.data.complemento || "";
            this.formData.enderecos[0].cidade = response.data.localidade || "";
            this.formData.enderecos[0].estado = response.data.uf || "";
          }
        } catch (error) {
          console.error("Erro ao buscar endereço pelo CEP:", error);
          alert("Não foi possível buscar o endereço. Verifique o CEP e tente novamente.");
        }
      } else {
        alert("Por favor, insira um CEP válido.");
      }
    },
    async registerUser() {
      try {
        const payload = {
          nome: this.formData.nomeCompleto.split(" ")[0] || "",
          sobrenome: this.formData.nomeCompleto.split(" ").slice(1).join(" ") || "",
          cpf: this.formData.cpf,
          telefone: this.formData.telefone,
          genero: "",
          dataNascimento: this.formData.dataNascimento,
          nacionalidade: "",
          email: this.formData.email,
          senha: this.formData.senha,
          enderecos: [
            {
              idEndereco: "",
              logradouro: this.formData.enderecos[0].rua,
              numero: this.formData.enderecos[0].numero,
              complemento: this.formData.enderecos[0].complemento,
              bairro: "",
              cidade: this.formData.enderecos[0].cidade,
              estado: this.formData.enderecos[0].estado,
              cep: this.formData.enderecos[0].cep,
            },
          ],
        };

        const response = await axios.post("http://localhost:5081/api/Usuario", payload, {
          headers: {
            "Content-Type": "application/json",
          },
        });

        if (response.status === 201) {
          const formData = new FormData();
          if (this.formData.fotoPerfil) {
            formData.append("file", this.formData.fotoPerfil);
          }

          const responseImage = await axios.post(`http://localhost:5081/api/Usuario/${response.data.id}/UploadImage`, formData, {
            headers: {
              "Content-Type": "multipart/form-data",
            },

          });

          console.log(responseImage);
          
          if (responseImage.status === 201) {
            this.successMessage = "Usuário cadastrado com sucesso!";
            this.errorMessage = "";
            setTimeout(() => {
              this.$router.push({ name: "login" });
            }, 2000);
          } else {
            this.successMessage = "";
            this.errorMessage = "Não foi possível fazer o upload da imagem. Tente novamente.";
          }
        } else {
          this.successMessage = "";
          this.errorMessage = "Não foi possível cadastrar o usuário. Tente novamente.";
        }
      } catch (error) {
        this.successMessage = "";
        this.errorMessage = "Erro ao cadastrar usuário. Verifique seus dados.";
        console.error("Erro ao cadastrar usuário:", error);
      }
    },
    nextStep() {
      if (this.currentStep === 2 && this.formData.senha !== this.confirmPassword) {
        alert("As senhas não coincidem!");
        return;
      }

      if (this.currentStep < 3) {
        this.currentStep++;
      } else {
        this.registerUser();
      }
    },
    prevStep() {
      if (this.currentStep > 0) {
        this.currentStep--;
      }
    },
    formatKey(key) {
      const keyMap = {
        nomeCompleto: "Nome completo",
        email: "Email",
        telefone: "Telefone",
        nomeDeUsuario: "Nome de usuário",
        senha: "Senha",
        dataNascimento: "Data de nascimento",
        cpf: "CPF",
        fotoPerfil: "Foto de Perfil",
        ativo: "Ativo",
        enderecos: "Endereço",
      };
      return keyMap[key] || key;
    },
  },
};
</script>

<template>
  <!-- Círculos de fundo -->
  <div class="circle small"></div>
  <div class="circle medium"></div>
  <div class="circle extra-large"></div>
  <div class="circle final"></div>
  <div class="circle additional1"></div>
  <div class="circle additional2"></div>
  <div class="circle additional3"></div>

  <div class="image-container">
    <img src="https://i.ibb.co/CJyvtyM/image_url-Photoroom.png" alt="logo" class="logo" />
  </div>
  <div class="container">
    <div class="register">
      <div class="text-box">
        <h1>CRIAR CONTA</h1>
                  <!-- Mensagens de sucesso ou erro -->
          <div class="messages">
            <p v-if="successMessage" class="success">{{ successMessage }}</p>
            <p v-if="errorMessage" class="error">{{ errorMessage }}</p>
          </div>
        <p>Preencha os dados abaixo para se cadastrar</p>
      </div>
      <div class="form">
        <form>
          <!-- Passo 1 -->
          <div class="form-step" v-show="currentStep === 0">
            <input name="nomeCompleto" type="text" class="form-control" v-model="formData.nomeCompleto" placeholder="Nome completo" required />
            <input name="email" type="email" class="form-control" v-model="formData.email" placeholder="Email" required />
            <input name="cel" type="text" class="form-control" v-model="formData.telefone" placeholder="Telefone" required />
            <input name="userName" type="text" class="form-control" v-model="formData.nomeDeUsuario" placeholder="Nome de usuário" required />
          </div>

          <!-- Passo 2 -->
          <div class="form-step" v-show="currentStep === 1">
            <input
              type="text"
              name="cep"
              class="form-control"
              v-model="formData.enderecos[0].cep"
              @blur="buscarEnderecoPorCEP"
              placeholder="CEP"
              required
            />
            <input type="text" name="rua" class="form-control" v-model="formData.enderecos[0].rua" placeholder="Rua" required />
            <input type="text" name="numero" class="form-control" v-model="formData.enderecos[0].numero" placeholder="Número" required />
            <input type="text" name="comp" class="form-control" v-model="formData.enderecos[0].complemento" placeholder="Complemento" />
            <input type="text" name="cidade" class="form-control" v-model="formData.enderecos[0].cidade" placeholder="Cidade" required />
            <input type="text" name="estado" class="form-control" v-model="formData.enderecos[0].estado" placeholder="Estado" required />
          </div>

          <!-- Passo 3 -->
          <div class="form-step" v-show="currentStep === 2">
            <input type="password" name="password" class="form-control" v-model="formData.senha" placeholder="Senha" required />
            <input type="password" name="confirmPassword" class="form-control" v-model="confirmPassword" placeholder="Confirme sua senha" required />
            <input type="date" name="date" class="form-control" v-model="formData.dataNascimento" placeholder="Data de nascimento" required />
            <input type="text" name="cpf" class="form-control" v-model="formData.cpf" placeholder="CPF" required />
            <input type="file" name="fotoPerfil" class="form-control" @change="event => formData.fotoPerfil = event.target.files[0]" required />
          </div>

          <!-- Passo 4 -->
          <div class="form-step" v-show="currentStep === 3">
            <p>Revise suas informações antes de finalizar o cadastro.</p>
            <br />
            <div v-for="(value, key) in filteredFormData" :key="key" class="review-item">
              <strong>{{ formatKey(key) }}:</strong>
              {{ value }}
            </div>
          </div>

          <!-- Navegação -->
          <div class="navigation">
            <button type="button" @click="prevStep" class="btn" v-if="currentStep > 0">Anterior</button>
            <button id="proximo" type="button" @click="nextStep" class="btn">
              {{ currentStep === 3 ? "Cadastrar" : "Próximo" }}
            </button>
          </div>

          <div id="step-indicator" class="step-indicator">
            {{ currentStep + 1 }}/4
          </div>

          <div>
            Já tem uma conta? <a href="/login" class="text-primary">Entrar agora</a>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<style scoped>
/* Círculos */
.circle {
  border-radius: 50%;
  position: absolute;
  opacity: 0.9;
}

.circle.small {
  width: 200px;
  height: 200px;
  margin-bottom: 600px;
  background: #d9d9d9;
  z-index: 3;
}

.circle.medium {
  width: 450px;
  height: 450px;
  background: linear-gradient(207deg, #683636 30.01%, #b8b8b8 68.84%);
  left: 300px;
  margin-top: 150px;
  z-index: 2;
}

.circle.extra-large {
  width: 890px;
  height: 890px;
  background: #838282;
  left: -320px;
  z-index: 1;
  margin-bottom: 150px;
}

.circle.final {
  width: 450px;
  height: 450px;
  background: linear-gradient(194deg, #3a3a3a 46.22%, #5c2323 118.41%);
  right: 50px;
  z-index: 7;
}

.circle.additional1 {
  width: 300px;
  height: 300px;
  top: 65%;
  right: 20%;
  background: #616161;
  z-index: 1;
}

.circle.additional2 {
  width: 400px;
  height: 400px;
  top: 20%;
  left: 50%;
  background: #634949;
  z-index: 1;
}

.circle.additional3 {
  width: 250px;
  height: 250px;
  bottom: 30%;
  left: 10%;
  background: linear-gradient(207deg, #683636 30.01%, #b8b8b8 68.84%);
  z-index: 1;
}

/* Contêiner principal */
.container {
  display: flex;
  width: 100%;
  height: 85vh;
  background-color: #bbb9b9a6;
  border-radius: 50px;
  border: 2px solid black;
  position: relative;
  z-index: 100;
}

/* Container da Imagem */
.image-container {
  flex: 1;
  display: flex;
  align-items: flex-start;
  justify-content: flex-start;
  background-color: #ffffff00;
  padding: 20px;
  position: relative;
  z-index: 101;
}

.logo {
  width: 400px;
  height: auto;
  z-index: 102;
}

/* Container do Registro */
.register {
  flex: 1;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  padding: 0 2rem;
  gap: 2rem;
}

.text-box {
  flex: 1;
}

.form {
  flex: 1;
}

h1 {
  font-size: 60px !important;
  font-weight: 900;
  text-transform: uppercase;
}

p {
  font-size: 20px;
  max-width: 13rem;
  color: gray;
}

a {
  text-decoration: none;
  color: rgb(16, 84, 255);
  text-transform: lowercase;
}

form {
  max-width: 400px;
  display: flex;
  flex-direction: column;
  gap: 20px;
}

input {
  padding: 14px 28px;
  border-radius: 30px;
  border: none;
  background: rgb(239, 239, 239);
  margin: 10px;
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

.btn:hover {
  filter: invert(1);
}

.review-item {
  margin-bottom: 10px;
}

/* Mensagens de sucesso e erro */
.success {
  color: green;
    font-weight: bold;
    background-color: rgba(0, 255, 0, 0.2);
    border-radius: 1rem;
    padding: 1rem;
    margin-bottom: 0px;
    text-align: center;
}

.error {
  color: red;
  font-weight: bold;
  margin-bottom: 10px;
}

/* Responsividade */
@media screen and (max-width: 844px) {
  .container {
    flex-direction: column;
  }

  .register {
    flex-direction: column;
  }

  .image-container,
  .register {
    flex: none;
    width: 100%;
    height: auto;
  }

  .logo {
    width: 300px;
  }

  .text-box {
    text-align: center;
  }
}
</style>
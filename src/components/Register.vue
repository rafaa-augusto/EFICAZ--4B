
<script>
import axios from "axios";

export default {
  data() {
    return {
      currentStep: 0,
      confirmPassword: "",
      formData: {
        nomeCompleto: "",
        email: "",
        telefone: "",
        nomeDeUsuario: "",
        senha: "",
        dataNascimento: "",
        cpf: "",
        ativo: true,
        url_foto_perfil: "",
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
      const { senha, ativo, url_foto_perfil, enderecos, ...filtered } = this.formData;

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
      const cep = this.formData.enderecos[0].cep.replace(/\D/g, ""); // Remove caracteres não numéricos

      // Verifica se o CEP tem 8 dígitos
      if (cep.length === 8) {
        try {
          const response = await axios.get(`https://viacep.com.br/ws/${cep}/json/`);

          if (response.data.erro) {
            alert("CEP não encontrado.");
          } else {
            // Preenche os campos de endereço com os dados do ViaCEP
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
          usuario: {
            nomeCompleto: this.formData.nomeCompleto,
            email: this.formData.email,
            senha: this.formData.senha,
            cpf: this.formData.cpf,
            telefone: this.formData.telefone,
            nomeDeUsuario: this.formData.nomeDeUsuario,
            url_foto_perfil: this.formData.url_foto_perfil,
          },
          endereco: {
            rua: this.formData.enderecos[0].rua,
            numero: this.formData.enderecos[0].numero,
            complemento: this.formData.enderecos[0].complemento,
            cidade: this.formData.enderecos[0].cidade,
            estado: this.formData.enderecos[0].estado,
            cep: this.formData.enderecos[0].cep,
          },
        };

        console.log("Dados enviados:", JSON.stringify(payload));

        const response = await axios.post("https://localhost:7119/api/Usuario", payload, {
          headers: {
            "Content-Type": "application/json",
          },
        });

        this.$router.push({ name: "login" });
        console.log("Resposta do backend:", response.data);
      } catch (error) {
        console.error("Erro ao cadastrar usuário:", error);
        alert("Ocorreu um erro ao tentar cadastrar o usuário.");
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
        url_foto_perfil: "URL da Foto de Perfil",
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
            <input type="text" name="url" class="form-control" v-model="formData.url_foto_perfil" placeholder="URL foto de perfil" required />
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
  background: #D9D9D9;
  z-index: 3;
}

.circle.medium {
  width: 450px;
  height: 450px;
  background: linear-gradient(207deg, #683636 30.01%, #B8B8B8 68.84%);
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
  background: linear-gradient(194deg, #3A3A3A 46.22%, #5c2323 118.41%);
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
  background: linear-gradient(207deg, #683636 30.01%, #B8B8B8 68.84%);
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
  z-index: 100; /* Superior aos círculos */
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
  z-index: 101; /* Superior aos círculos */
}

.logo {
  width: 400px;
  height: auto;
  z-index: 102; /* Garantir que fique acima de tudo */
}


/* Container do Registro */
.register {
    flex: 1;
    display: flex; /* Alinha lado a lado */
    flex-direction: row; /* Garantir alinhamento horizontal */
    justify-content: space-between; /* Espaça os elementos */
    align-items: center; /* Centraliza verticalmente */
    padding: 0 2rem;
    gap: 2rem; /* Espaçamento entre os elementos */
}

.text-box {
    flex: 1; /* Garante largura proporcional */
}

.form {
    flex: 1; /* Garante largura proporcional */
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

/* Responsividade */
@media screen and (max-width: 844px) {
    .container {
        flex-direction: column;
    }

    .register {
        flex-direction: column; /* Alinha os elementos em coluna */
    }

    .image-container, .register {
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
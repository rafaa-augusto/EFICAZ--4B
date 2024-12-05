
<script>
import { RouterLink } from 'vue-router';
import axios from 'axios';
// import { errorMessages } from 'vue/compiler-sfc';


export default {
    data() {
        return {
            email: '',
            password: '',
            errorMessage: '',
            showLogo: true
        };
    },
    methods: {
        async submitLogin() {
            try {
                const response = await axios.post('https://localhost:7119/api/Auth/signIn', {
                    email: this.email,
                    password: this.password
                });

                console.log("Resposta completa da API de Login:", response.data);

                const token = response.data;      
                
                if (token) {
                    localStorage.setItem('authToken', token.token);
                    console.log('Login realizado com sucesso!');
                    this.$router.push('/'); 
                } else {
                    console.error('Token não retornado pela API de login.');
                }
            } catch (error) {
                // console.error('Erro no login:', error);
                console.log(error.response.data);
                // Define a mensagem de erro com base na resposta da API ou no erro local
                this.errorMessage = error.response?.data?.message || 'Ocorreu um erro ao realizar login. Tente novamente.';
            }
        }

    },
    mounted() {
        setTimeout(() => {
            this.showLogo = false;
        }, 100);
    },
    components: {
        RouterLink
    }
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
    <div class="container-geral">


    <div class="image-container">
        <img src="https://i.ibb.co/CJyvtyM/image_url-Photoroom.png" alt="logo" class="logo" />
    </div>
    <div class="container">
        <!-- Container do Login -->
        <div class="login">
            <div class="text-box">
                <h1>BEM VINDO de volta</h1>
                <p>Acesse sua conta agora mesmo</p>
            </div>
            <div class="form">
                <form @submit.prevent="submitLogin">
                    <!-- Mensagem de erro -->
                    <div v-if="errorMessage" class="error-message">
                        {{ errorMessage }}
                    </div>

                    <input type="email" name="email" class="form-control" v-model="email" placeholder="Email" required />
                    <input type="password" name="password" class="form-control" v-model="password" placeholder="Senha" required />
                    <a href="#" class="forgot-password d-block mb-3">Esqueci minha senha</a>
                    <button type="submit" id="entrar" class="btn">Entrar</button>
                    <div>
                        Não tem uma conta? <a href="/register" class="text-primary font-weight-bold">Cadastre-se</a>
                    </div>
                </form>
            </div>
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

.container-geral{
    background-color: #f4f4f4;
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
    background-color: #f4f4f400;
    padding: 20px;
    position: relative;
    z-index: 120;
}

.logo {
    width: 400px;
    height: auto;
}

/* Container do Login */
.login {
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
    font-size: 70px !important;
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

.error-message {
    color: red;
    font-weight: bold;
    background-color: rgba(255, 0, 0, 0.3);
    border-radius: 1rem;
    padding: 1rem;
    margin-bottom: 15px;
    text-align: center;
}

/* Responsividade */
@media screen and (max-width: 844px) {
    .container {
        flex-direction: column;
    }

    .login {
        flex-direction: column; /* Alinha os elementos em coluna */
    }

    .image-container, .login {
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
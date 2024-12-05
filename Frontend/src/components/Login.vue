<template>
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
            <div class="login">
                <div class="text-box">
                    <h1>BEM VINDO de volta</h1>
                    <p>Acesse sua conta agora mesmo</p>
                </div>
                <div class="form">
                    <form @submit.prevent="submitLogin">
                        <div v-if="errorMessage" class="error-message">
                            {{ errorMessage }}
                        </div>
                        
                        <div v-if="successMessage" class="success-message">
                            {{ successMessage }}
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

<script>
import { RouterLink } from 'vue-router';
import axios from 'axios';

export default {
    data() {
        return {
            email: '',
            password: '',
            errorMessage: '',
            successMessage: '', 
            showLogo: true
        };
    },
    methods: {
        async submitLogin() {
            try {
                const response = await axios.post('http://localhost:5081/api/Auth/signIn', {
                    email: this.email,
                    password: this.password
                });
                const token = response.data.token;
                
                if (token) {
                    const userId = response.data.userId; 
                    localStorage.setItem('authToken', token);
                    localStorage.setItem('userId', userId);
                    console.log('Login realizado com sucesso!');
                    this.successMessage = 'Login realizado com sucesso!'; 
                    this.errorMessage = ''; 

                    setTimeout(() => {
                        this.$router.push('/');
                    }, 2000);
                } else {
                    console.error('Token não retornado pela API de login.');
                }
            } catch (error) {
                console.log(error.response);
                this.errorMessage = error.response?.data || 'Ocorreu um erro ao realizar login. Tente novamente.';
                this.successMessage = ''; 
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

<style scoped>
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

.login {
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

.success-message {
    color: green;
    font-weight: bold;
    background-color: rgba(0, 255, 0, 0.2);
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
        flex-direction: column;
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

<script>
import api from '@/api';

export default {
    data() {
        return {
            userName: '',
            userEmail: '',
            userProfilePic: '',
            errorMessage: '',
            placeholderImage: 'https://via.placeholder.com/120', // Imagem padrão
        };
    },
    methods: {
        async fetchUserData() {
            try {
                const response = await api.get('/Usuario/me');
                const userData = response.data;
                this.userName = userData.nomeDeUsuario || 'Usuário';
                this.userEmail = userData.email || 'Email não disponível';
                this.userProfilePic = userData.url_foto_perfil || this.placeholderImage;
            } catch (error) {
                console.error('Erro ao buscar dados do usuário:', error);
                if (error.response && error.response.status === 401) {
                    this.errorMessage = 'Usuário não autenticado. Por favor, faça login novamente.';
                    this.logout();
                } else {
                    this.errorMessage = 'Erro ao buscar dados do usuário. Por favor, tente novamente mais tarde.';
                }
            }
        },
        editProfile() {
            this.$router.push('/edit-profile');
        },
        logout() {
            localStorage.removeItem('authToken');
            this.$router.push('/login');
        },
        goBack() {
            this.$router.go(-1);
        },
        goHome() {
            this.$router.push('/');
        },
    },
    mounted() {
        this.fetchUserData();
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
    <div class="container-geral"></div>


    <div class="image-container">
        <img src="https://i.ibb.co/CJyvtyM/image_url-Photoroom.png" alt="logo" class="logo" />
    </div>

    <div class="profile-page">
        <header class="header">
            <div class="container">
                <h1>Meu Perfil</h1>
            </div>
        </header>
        <main class="profile-container">
            <div class="user-info">
                <img :src="userProfilePic || placeholderImage" alt="Foto do Perfil" class="profile-pic" />
                <h2>{{ userName }}</h2>
                <p>{{ userEmail }}</p>
            </div>
            <div class="profile-actions">
                <div class="action-buttons">
                    <button @click="editProfile" class="btn primary-btn">Editar Perfil</button>
                    <button @click="logout" class="btn secondary-btn">Sair da Conta</button>
                </div>
                <div class="navigation-buttons">
                    <button @click="goBack" class="btn secondary-btn">Voltar</button>
                    <button @click="goHome" class="btn secondary-btn">Home</button>
                </div>
            </div>
        </main>
        <p v-if="errorMessage" class="error-message">{{ errorMessage }}</p>
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


.profile-page {
    font-family: 'Arial', sans-serif;
    color: #333;
    text-align: center;
    margin: 0 auto;
    max-width: 800px; /* Diminuído */
    padding: 20px;
    background: #fff; /* Fundo branco */
    border-radius: 20px; /* Bordas arredondadas */
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
    position: relative; /* Necessário para o z-index funcionar */
    z-index: 100; /* Superior aos círculos */
}


.header h1 {
    font-size: 2rem;
    font-weight: bold;
    margin-bottom: 20px;
    color: #000;
}

.profile-container {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: 20px;
}

.user-info {
    display: flex;
    flex-direction: column;
    align-items: center;
}

.profile-pic {
    width: 120px;
    height: 120px;
    border-radius: 50%;
    object-fit: cover;
    border: 2px solid #000;
    background: #eee;
}

h2 {
    font-size: 1.5rem;
    margin: 10px 0;
}

p {
    font-size: 1rem;
    color: #666;
}

.profile-actions {
    display: flex;
    flex-direction: column;
    gap: 20px;
    width: 100%;
}

.action-buttons,
.navigation-buttons {
    display: flex;
    justify-content: center;
    gap: 15px; /* Separação entre os botões */
}

.btn {
    padding: 10px 15px;
    border-radius: 30px;
    font-size: 1rem;
    cursor: pointer;
    text-transform: uppercase;
    transition: 400ms;
}

.btn:hover {
    filter: invert(1);
}

.primary-btn {
    background-color: #000;
    color: #fff;
    border: none;
}

.primary-btn:hover {
    background-color: #333;
}

.secondary-btn {
    background-color: #fff;
    color: #000;
    border: 1px solid #000;
}

.secondary-btn:hover {
    background-color: #f0f0f0;
}

.error-message {
    color: red;
    font-size: 1rem;
    margin-top: 20px;
}
</style>

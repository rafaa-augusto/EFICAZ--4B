<script>
export default {
    data() {
        return {
            successMessage: ''
        };
    },
    methods: {
        goBack() {
            this.$router.go(-1);
        }
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
    <div class="container-geral"></div>


    <div class="image-container">
        <img src="https://i.ibb.co/CJyvtyM/image_url-Photoroom.png" alt="logo" class="logo" />
    </div>

    <div class="profile-page">
        <main class="profile-container">
            <h1>Editar Perfil</h1>
            <form class="profile-form">
                <!-- Inputs lado a lado -->
                <div class="form-row">
                    <input type="text" class="form-control" placeholder="Primeiro Nome" />
                    <input type="text" class="form-control" placeholder="Sobrenome" />
                </div>
                <!-- Inputs lado a lado -->
                <div class="form-row">
                    <input type="email" class="form-control" placeholder="Email" />
                    <input type="tel" class="form-control" placeholder="Telefone" />
                </div>
                <!-- Input único -->
                <input type="text" class="form-control full-width" placeholder="Endereço Completo" />
                <!-- Botão de salvar -->
                <button type="button" class="btn save-btn">Salvar</button>
            </form>
            <p v-if="successMessage" class="success-message">{{ successMessage }}</p>
            <button @click="goBack" class="btn secondary-btn">Voltar</button>
        </main>
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



/* Página principal */
.profile-page {
    display: flex;
    justify-content: center;
    align-items: center;
    min-height: 100vh;
    background-color: #bbb9b956;
}

/* Container principal */
.profile-container {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    width: 90%;
    max-width: 600px; /* Menor largura */
    background-color: #fff;
    border-radius: 40px;
    border: 1px solid black;
    padding: 30px; /* Menor padding */
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    position: relative; /* Necessário para o z-index funcionar */
    z-index: 100; /* Superior aos círculos */
}


/* Título */
.profile-container h1 {
    font-size: 1.8rem;
    font-weight: bold;
    color: #333;
    margin-bottom: 20px;
}

/* Formulário */
.profile-form {
    width: 100%;
    display: flex;
    flex-direction: column;
    gap: 20px; /* Espaçamento entre os campos */
}

/* Linha de inputs lado a lado */
.form-row {
    display: flex;
    gap: 10px; /* Espaçamento entre inputs */
}

/* Inputs */
.form-control {
    flex: 1;
    padding: 10px;
    font-size: 1rem;
    border: 1px solid #ccc;
    border-radius: 5px;
}

.form-control.full-width {
    width: 100%;
}

/* Botão de salvar */
.save-btn {
    background-color: #007bff;
    color: #fff;
    padding: 10px 20px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    font-size: 1rem;
}

.save-btn:hover {
    background-color: #0056b3;
}

/* Mensagem de sucesso */
.success-message {
    color: green;
    font-size: 1rem;
    margin-top: 10px;
}

/* Botão de voltar */
.secondary-btn {
    background-color: #fff;
    color: #000;
    border: 1px solid #000;
    padding: 10px 20px;
    border-radius: 5px;
    font-size: 1rem;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

.secondary-btn:hover {
    background-color: #f0f0f0;
}
</style>

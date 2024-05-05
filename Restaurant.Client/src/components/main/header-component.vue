<script setup>
import {ref, onMounted} from 'vue';
import axios from 'axios';

const user = ref(null);

async function fetchUserData() {
  try {
    const response = await axios.post('http://localhost:5178/api/Client/CurrentUser', null, {withCredentials: true});
    user.value = response.data;
    console.log(response.data);
  } catch (error) {
    console.error('Error fetching user data:', error);
  }
}

async function logout() {
  try {
    const response = await axios.post('http://localhost:5178/api/Client/Logout', null, {withCredentials: true});
    console.log(response.data);
    user.value = null;
  } catch (error) {
    console.error('Error logging out:', error);
  }
}

onMounted(() => {
  fetchUserData();
});

let showPopUp = ref(false);

let showSmsCode = ref(false);

let phone = ref('');

async function sendSms() {

  try {
    const response = await axios.post('http://localhost:5178/api/PhoneAuth/Generate', {
      phone: phone.value
    }, {withCredentials: true});
    showSmsCode.value = true;
    console.log(response.data);
  } catch (error) {
    console.error('Error logging out:', error);
  }
}

let code = ref('');

async function verifySms() {

  try {
    const response = await axios.post('http://localhost:5178/api/PhoneAuth/Verify', {
      phone: phone.value,
      code: code.value
    }, {withCredentials: true});
    console.log(response.data);
    if (response.data.success === true) {
      showPopUp.value = false;
      showSmsCode.value = false;

      await fetchUserData()
    }
  } catch (error) {
    console.error('Error logging out:', error);
  }

}

function closePopup() {
  showPopUp.value = false;
  showSmsCode.value = false;
}
</script>

<template>
  <header class="wrapper">
    <div class="popup zcool-font" v-if="showPopUp" :class="{'no-active': showPopUp === false}">
      <img alt="" src="/png/close.png" class="close" @click="closePopup">
      <div v-if="!showSmsCode">
        <input type="text" placeholder="укажите ваш номер телефона" v-model="phone">
        <button @click="sendSms">подтвердить</button>
      </div>
      <div v-if="showSmsCode">
        <input type="text" placeholder="введите код" v-model="code">
        <button @click="verifySms">войти</button>
      </div>
    </div>

    <router-link to="main">
      <img class="logo" src="/png/logo.png" alt="logo">
    </router-link>
    <button class="login" v-if="user == null" @click="showPopUp = true">Войти</button>
    <div v-if="user != null" class="login-wrapper">
      <div>
        <div class="user-name">{{ user.data.name }}</div>
        <div class="user-name">{{ user.data.phone }}</div>
      </div>
      <button class="login logOut" @click="logout">Выйти</button>
    </div>
  </header>
</template>

<style scoped>
header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 20px;

}

.logo {
  width: 380px;
  height: 70px;
}

.login {
  width: 160px;
  height: 40px;
}

.user-name {
  margin-right: 36px;
  font-size: 20px;
}

.login-wrapper {
  display: flex;
  align-items: center;
}


.popup {
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  background: #fff;
  color: #000;
  z-index: 999990;
  padding: 25px;
  width: 400px;
  height: 150px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.5);
  transition: 1s;
}

.no-active {
  width: 0;
  height: 0;
}

.popup input {
  width: 100%;
  height: 40px;
  outline: none;
  border: 2px solid #363C4480;
  color: #363c4480;
  margin: 10px 0;
  padding: 0 20px;
  font-size: 20px;
}

.popup button {
  color: #212121;
  background-color: transparent;
  border: 2px solid #212121;
  width: 150px;
  height: 40px;
  position: absolute;
  left: 74.5%;
  top: 60%;
  transform: translate(-50%, 0);
}

.popup button:hover {
  border: 2px solid #fff;
  color: #fff;
  background: #000;
}

.close {
  width: 25px;
  height: 25px;
  position: absolute;
  top: 2%;
  right: 2%;
  cursor: pointer;
}

</style>
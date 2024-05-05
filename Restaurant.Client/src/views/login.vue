<script setup>
import {ref} from 'vue'
import axios from 'axios';

const login = ref('');
const password = ref('');
const errorMessage = ref('');

async function loginUser() {
  try {
    const response = await axios.post('http://localhost:5178/api/Admin/Login', {
      login: login.value,
      password: password.value
    }, {withCredentials: true});

    if (response.data.success) {
      window.location.href = '/admin';
    } else {
      errorMessage.value = 'Invalid login or password. Please try again.';
    }
  } catch (error) {
    console.error('Error logging in:', error);
    errorMessage.value = 'An error occurred while logging in. Please try again later.';
  }
}
</script>

<template>
  <div class="wrapper container zcool-font">
    <div class="admin-form">
      <h1>Admin panel</h1>
      <div class="input-field">
        <label for="login">Login</label>
        <input type="text" id="login" placeholder="Enter your login" v-model="login">
      </div>
      <div class="input-field">
        <label for="password">Password</label>
        <input type="password" id="password" placeholder="Enter your password" v-model="password">
      </div>
      <button @click="loginUser">Login</button>
    </div>
  </div>
</template>

<style scoped>
.wrapper {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
}

.admin-form {
  width: 300px;
  padding: 20px;
  background-color: rgba(255, 255, 255, 0.8);
  border-radius: 8px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  text-align: center;
}

.admin-form h1 {
  font-size: 24px;
  margin-bottom: 20px;
  color: #000;
}

.input-field {
  margin-bottom: 15px;
}

.input-field label {
  font-size: 14px;
  margin-bottom: 5px;
  display: block;
  color: #000;
}

.input-field input {
  width: 100%;
  padding: 8px;
  border: 1px solid #ccc;
  border-radius: 4px;
  box-sizing: border-box;
  background-color: rgba(255, 255, 255, 0.2);
  color: #000;
}

button {
  width: 100%;
  padding: 10px;
  background-color: #007bff;
  color: #fff;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

button:hover {
  background-color: #0056b3;
}
</style>
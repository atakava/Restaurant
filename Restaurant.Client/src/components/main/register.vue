<script setup>
import {ref} from 'vue';
import axios from 'axios';

const name = ref('');
const phone = ref('');
const data = ref(null);
const error = ref(null);

const fetchData = async () => {
  try {
    const client = {name: name.value, phone: phone.value}; // Создаем объект с актуальными данными
    const response = await axios.post(
        "http://localhost:5178/api/Client/Create",
        client,
        {withCredentials: true}
    );
    data.value = response.data;
    error.value = null;
  } catch (err) {
    console.error(err);
    error.value = 'Ошибка при получении данных';
  }
};
</script>

<template>
  <div class="wrapper">
    <h3>Зарегистрироваться</h3>
    <div class="content-register">
      <input placeholder="Введите имя" v-model="name">
      <input placeholder="Введите номер телефона" v-model="phone">
      <button @click="fetchData">Зарегистрироваться</button>
    </div>
  </div>
</template>

<style scoped>
h3 {
  text-align: center;
  margin: 45px 0;
  font-size: 36px;
}

.content-register {
  background: #fff;
  height: 200px;
  padding: 30px;
  position: relative;
}

.content-register input {
  width: 100%;
  height: 40px;
  outline: none;
  border: 2px solid #363C4480;
  color: #363c4480;
  margin: 10px 0;
  padding: 0 20px;
  font-size: 20px;
}

button {
  color: #212121;
  background-color: transparent;
  border: 2px solid #212121;
  width: 280px;
  height: 40px;
  position: absolute;
  right: 2.5%;
  bottom: 5%;
}

button:active {
  transform: translateY(5px);
}
</style>
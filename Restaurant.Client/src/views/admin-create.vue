<script setup>
import {ref} from 'vue';
import axios from 'axios';
import NavLine from "@/components/admin/nav-line.vue";

const formData = ref({
  title: '',
  description: '',
  image: null,
  price: null,
  catalogId: null
});

const handleImageChange = (event) => {
  formData.value.image = event.target.files[0];
};

const createProduct = async () => {
  try {
    const formDataToSend = new FormData();
    formDataToSend.append('title', formData.value.title);
    formDataToSend.append('description', formData.value.description);
    formDataToSend.append('img', formData.value.image);
    formDataToSend.append('price', formData.value.price);
    formDataToSend.append('catalogId', formData.value.catalogId);

    const response = await axios.post('http://localhost:5178/api/Product/Create', formDataToSend, {
      withCredentials: true,
      headers: {
        'Content-Type': 'multipart/form-data'
      }
    });

    if (response.data.success) {
      console.log('Продукт успешно создан');
      window.location.href = '/admin';
    } else {
      console.error('Ошибка при создании продукта:', response.data.error);
    }
  } catch (error) {
    console.error('Ошибка при выполнении запроса:', error);
  }
};
</script>

<template>
  <div class="content zcool-font">
    <nav-line/>
    <div class="create-product">
      <h1>Создание продукта</h1>
      <form @submit.prevent="createProduct">
        <div class="form-group">
          <label for="title">Название</label>
          <input type="text" id="title" v-model="formData.title" required>
        </div>
        <div class="form-group">
          <label for="description">Описание</label>
          <textarea id="description" v-model="formData.description" required></textarea>
        </div>
        <div class="form-group">
          <label for="image">Изображение</label>
          <input type="file" id="image" @change="handleImageChange" required>
        </div>
        <div class="form-group">
          <label for="price">Цена</label>
          <input type="number" id="price" v-model.number="formData.price" required>
        </div>
        <div class="form-group">
          <label for="catalogId">ID каталога</label>
          <input type="number" id="catalogId" v-model.number="formData.catalogId" required>
        </div>
        <button type="submit">Создать продукт</button>
      </form>
    </div>
  </div>
</template>

<style scoped>
.content {
  width: 100%;
  display: flex;
  background: #fff;
}

.create-product {
  width: 100%;
  margin: 0 auto;
  padding: 20px;
  background-color: #fff;
  border-radius: 8px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
}

.create-product h1 {
  font-size: 24px;
  margin-bottom: 20px;
  text-align: center;
}

.form-group {
  margin-bottom: 20px;
}

label {
  display: block;
  margin-bottom: 5px;
}

input[type="text"],
input[type="number"],
textarea {
  width: 100%;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 4px;
}

textarea {
  height: 270px;
}

input[type="file"] {
  display: none;
}

input[type="file"] + label {
  display: inline-block;
  padding: 10px 15px;
  background-color: #007bff;
  color: #fff;
  border-radius: 4px;
  cursor: pointer;
}

button[type="submit"] {
  display: block;
  width: 100%;
  padding: 10px;
  background-color: #4CAF50;
  color: #fff;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

button[type="submit"]:hover {
  background-color: #45a049;
}
</style>
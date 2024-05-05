<template>
  <div class="content zcool-font">
    <nav-line/>
    <div class="create-category">
      <h1>Создание категории</h1>
      <form @submit.prevent="createCategory">
        <div class="form-group">
          <label for="name">Название категории</label>
          <input type="text" id="name" v-model="formData.name" required>
        </div>
        <button type="submit">Создать категорию</button>
      </form>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import axios from 'axios';
import NavLine from "@/components/admin/nav-line.vue";

const formData = ref({
  name: ''
});

const createCategory = async () => {
  try {
    const response = await axios.post('http://localhost:5178/api/Category/Create', formData.value);
    if (response.data.success) {
      console.log('Категория успешно создана');
      window.location.href = '/category-admin'; 
    } else {
      console.error('Ошибка при создании категории:', response.data.error);
    }
  } catch (error) {
    console.error('Ошибка при выполнении запроса:', error);
  }
};
</script>

<style scoped>
.content {
  width: 100%;
  display: flex;
  background: #fff;
}

.create-category {
  width: 100%;
  margin: 0 auto;
  padding: 20px;
  background-color: #fff;
  border-radius: 8px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
}

.create-category h1 {
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

input[type="text"] {
  width: 100%;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 4px;
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

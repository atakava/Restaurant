<script setup>
import {ref, onMounted} from 'vue';
import axios from 'axios';
import NavLine from "@/components/admin/nav-line.vue";

const products = ref([]);

const fetchProducts = async () => {
  try {
    const response = await axios.post('http://localhost:5178/api/Product/Select', null, {withCredentials: true});
    if (response.data.success) {
      products.value = response.data.data;
    } else {
      console.error('Ошибка при получении данных о продуктах:', response.data.error);
    }
  } catch (error) {
    console.error('Ошибка при выполнении запроса:', error);
  }
};

const deleteProduct = async (productId) => {
  try {
    const response = await axios.post('http://localhost:5178/api/Product/Delete', {
      id: productId
    }, {withCredentials: true});
    if (response.data.success) {
      products.value = products.value.filter(product => product.id !== productId);
    } else {
      console.error('Ошибка при удалении продукта:', response.data.error);
    }
    await fetchProducts();

  } catch (error) {
    console.error('Ошибка при выполнении запроса:', error);
  }
};

onMounted(fetchProducts);
</script>

<template>
  <div class="content zcool-font">
    <nav-line/>
    <div class="admin">
      <h1>Продукты</h1>
      <div class="product-table">
        <table>
          <thead>
          <tr>
            <th>ID</th>
            <th>Название</th>
            <th>Описание</th>
            <th>Цена</th>
            <th>Картинка</th>
            <th>Удалить</th>
          </tr>
          </thead>
          <tbody>
          <tr v-for="product in products" :key="product.id">
            <td>{{ product.id }}</td>
            <td>{{ product.title }}</td>
            <td>{{ product.description }}</td>
            <td>{{ product.price }}</td>
            <td><img :src="'http://' + product.img" :alt="product.title"></td>
            <td>
              <button class="delete-button" @click="deleteProduct(product.id)">Удалить</button>
            </td>
          </tr>
          </tbody>
        </table>
      </div>
    </div>
    <div class="add-product-button">
      <router-link to="product-create">
        <img src="/svg/plus.svg" alt="plus-btn">
      </router-link>
    </div>
  </div>
</template>

<style scoped>
.admin {
  background: #fff;
  width: 100%;
}

h1 {
  text-align: center;
  margin-top: 30px;
  color: #333;
}

.content {
  width: 100%;
  display: flex;
}

.add-product-button {
  position: fixed;
  bottom: 30px;
  left: 18%;
  width: 60px;
  height: 60px;
  background-color: #4CAF50;
  color: white;
  font-size: 32px;
  border-radius: 50%;
  border: none;
  cursor: pointer;
  box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
  display: flex;
  justify-content: center;
  align-items: center;
}

.add-product-button:focus {
  outline: none;
}

.add-product-button:hover {
  background-color: #45a049;
}

.add-product-button img {
  width: 35px;
  height: 35px;
}
</style>
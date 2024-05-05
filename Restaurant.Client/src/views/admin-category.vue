<script setup>
import {ref, onMounted} from 'vue';
import axios from 'axios';
import NavLine from "@/components/admin/nav-line.vue";

const catalogs = ref([]);
const newCategoryName = ref('');

const fetchCatalogs = async () => {
  try {
    const response = await axios.post('http://localhost:5178/api/Category/Select', null, {withCredentials: true});
    if (response.data.success) {
      catalogs.value = response.data.data;
    } else {
      console.error('Ошибка при получении каталогов:', response.data.error);
    }
  } catch (error) {
    console.error('Ошибка при выполнении запроса:', error);
  }
};

const deleteCategory = async (categoryId) => {
  try {
    const response = await axios.post('http://localhost:5178/api/Category/Delete', {id: categoryId}, {withCredentials: true});
    if (response.data.success) {
      await fetchCatalogs();
    } else {
      console.error('Ошибка при удалении категории:', response.data.error);
    }
  } catch (error) {
    console.error('Ошибка при выполнении запроса:', error);
  }
};

onMounted(fetchCatalogs);
</script>

<template>
  <div class="content zcool-font">
    <nav-line/>
    <div class="">
      <div class="catalogs">
        <h1>Каталоги</h1>
        <div class="catalog-table">
          <table>
            <thead>
            <tr>
              <th>ID</th>
              <th>Название категории</th>
              <th>Действие</th>
            </tr>
            </thead>
            <tbody>
            <tr v-for="(catalog, index) in catalogs" :key="index">
              <td>{{ catalog.id }}</td>
              <td>
                {{ catalog.name }}
              </td>
              <td>
                <button class="delete-button" @click="deleteCategory(catalog.id)">Удалить</button>
              </td>
            </tr>
            </tbody>
          </table>
        </div>
      </div>
      <div v-for="(catalog, index) in catalogs" :key="index">
        <h2>{{ catalog.name }}</h2>
        <div class="product-table">
          <table>
            <thead>
            <tr>
              <th>Название продукта</th>
              <th>Описание</th>
              <th>Цена</th>
              <th>Изображение</th>
            </tr>
            </thead>
            <tbody>
            <tr v-for="(product, i) in catalog.products" :key="i">
              <td>{{ product.title }}</td>
              <td>{{ product.description }}</td>
              <td>{{ product.price }}</td>
              <td><img :src="'http://' + product.img" :alt="product.title"/></td>
            </tr>
            </tbody>
          </table>
        </div>
      </div>

      <div class="add-product-button">
        <router-link to="create-category">
          <img src="/svg/plus.svg" alt="plus-btn">
        </router-link>
      </div>
    </div>
  </div>

</template>

<style scoped>
.content {
  width: 100%;
  display: flex;
  justify-content: space-between;
  background: #fff;
}

.catalogs {
  margin-bottom: 20px;
}

.catalog-table table {
  width: 100%;
  border-collapse: collapse;
}

.catalog-table th,
.catalog-table td {
  padding: 10px;
  border: 1px solid #ddd;
}

.catalog-table th {
  background-color: #f2f2f2;
}

.catalog-table tr:hover {
  background-color: #f2f2f2;
}

.product-table {
  margin-top: 10px;
}

.product-table table {
  width: 100%;
  border-collapse: collapse;
}

.product-table th,
.product-table td {
  padding: 10px;
  border: 1px solid #ddd;
}

.product-table th {
  background-color: #f2f2f2;
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

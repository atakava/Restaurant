<script setup>
import {ref, onMounted} from "vue"
import axios from 'axios';
import HeaderComponent from "@/components/main/header-component.vue";
import FooterComponent from "@/components/main/footer-component.vue";

const categories = ref([]);

async function fetchDataProduct() {
  axios.post('http://localhost:5178/api/Category/Select', null, {withCredentials: true})
      .then(response => {
        console.log(response.data);
        categories.value = response.data.data;
      })
      .catch(error => {
        console.error('Error fetching categories:', error);
      });
}

onMounted(() => {
  fetchDataProduct();
});
</script>

<template>
  <header-component/>
  <div class="category-line zcool-font">
    <div class="wrapper content">
      <div class="category-name" v-for="item in categories">{{ item.name }}</div>
    </div>
  </div>

  <div class="product-container zcool-font wrapper" v-for="item in categories">
    <h1 class="product-category">{{item.name}}</h1>
    <div class="product-box">
      <div class="product-card" v-for="product in item.products">
        <img :src="'http://' + product.img" alt="">
        <div class="product-content">
          <div class="title-product">{{product.title}}</div>
          <div class="desc">{{product.description}}</div>
          <div class="price">{{ product.price}} ₽</div>
        </div>
      </div>
    </div>
  </div>
  <footer-component/>
</template>

<style scoped>
.category-line {
  color: #fff;
  margin: 35px 0;
  background: #3C3127;
  padding: 15px 0;
  font-size: 20px;
}

.content {
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.category-name {
  cursor: pointer;
}

.product-container {
  color: #fff;
}

.product-category {
  font-size: 32px;
  margin: 20px 0;
}

.product-box {
  display: flex;
  justify-content: space-between;
  align-items: center;
  flex-wrap: wrap;
}

.product-card {
  background: #fff;
  color: #000;
  max-width: 332px;
  flex-basis: calc(33.33% - 10px);
  margin: 25px 0;
  box-sizing: border-box;
}

.product-card img {
  width: 100%;
  height: 331px;
  background: #5b6479;
}

.product-card .title-product {
  font-size: 20px;
  margin-bottom: 10px;
  font-weight: 600;
}

.product-card .desc {
  font-size: 16px;
  margin-bottom: 10px;
}

.product-card .price {
  font-size: 24px;
}

.product-content {
  padding: 15px;
}
</style>
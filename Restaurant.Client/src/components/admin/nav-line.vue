<script setup>

</script>

<template>
  <div class="line zcool-font">
    <div>
      <div class="logo">Admin Panel</div>
      <router-link to="category-admin" class="btn-text">
        <div class="nav-item">Каталог</div>
      </router-link>
      <router-link to="admin" class="btn-text">
        <div class="nav-item">Товары</div>
      </router-link>
      <router-link to="users" class="btn-text">
        <div class="nav-item">Пользователи</div>
      </router-link>
      <router-link to="tables" class="btn-text">
        <div class="nav-item">Столы</div>
      </router-link>
    </div>
  </div>
</template>

<style scoped>

.logo {
  text-align: center;
  font-size: 24px;
  margin: 35px 0;
  cursor: pointer;
}

.line {
  position: sticky;
  top: 0;
  left: 0;
  width:250px;
  height: 100vh;
  background-color: #333;
  color: #fff; 
}

.nav-item {
  padding: 10px 10px; 
  cursor: pointer;
}

.nav-item:hover {
  background-color: #555;
}

.nav-item:active {
  background-color: #888; 
  font-weight: bold; 
}
</style>
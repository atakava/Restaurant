<template>
  <div class="content zcool-font">
    <nav-line/>
    <div class="users">
      <h1>Пользователи</h1>
      <table>
        <thead>
        <tr>
          <th>ID</th>
          <th>Имя</th>
          <th>Телефон</th>
          <th>Действие</th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="user in users" :key="user.id">
          <td>{{ user.id }}</td>
          <td>{{ user.name }}</td>
          <td>{{ user.phone }}</td>
          <td><button @click="deleteUser(user.id)">Удалить</button></td>
        </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import axios from 'axios';
import NavLine from "@/components/admin/nav-line.vue";

const users = ref([]);

const fetchUsers = async () => {
  try {
    const response = await axios.post('http://localhost:5178/api/Client/Select');
    if (response.data.success) {
      users.value = response.data.data;
    } else {
      console.error('Ошибка при получении пользователей:', response.data.error);
    }
  } catch (error) {
    console.error('Ошибка при выполнении запроса:', error);
  }
};

const deleteUser = async (userId) => {
  try {
    const response = await axios.post('http://localhost:5178/api/Client/Delete', { id: userId });
    if (response.data.success) {
      console.log('Пользователь успешно удален');
      // Обновляем список пользователей после удаления
      fetchUsers();
    } else {
      console.error('Ошибка при удалении пользователя:', response.data.error);
    }
  } catch (error) {
    console.error('Ошибка при выполнении запроса:', error);
  }
};

onMounted(fetchUsers);
</script>

<style scoped>
.content {
  width: 100%;
  display: flex;
  background: #fff;
}

.users {
  width: 100%;
  margin: 0 auto;
  padding: 20px;
}

h1 {
  font-size: 24px;
  margin-bottom: 20px;
}

table {
  width: 100%;
  border-collapse: collapse;
}

th, td {
  padding: 8px;
  border-bottom: 1px solid #ddd;
  text-align: left;
}

th {
  background-color: #f2f2f2;
}

button {
  padding: 8px 12px;
  background-color: #f44336;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

button:hover {
  background-color: #c62828;
}
</style>

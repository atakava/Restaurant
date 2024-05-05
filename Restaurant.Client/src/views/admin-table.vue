<template>
  <div>
    <div class="content zcool-font">
      <nav-line/>
      <div>
        <h2>Таблица столов</h2>
        <table>
          <thead>
          <tr>
            <th>Номер стола</th>
            <th>Количество мест</th>
            <th>Цена</th>
            <th>Занятые времена</th>
          </tr>
          </thead>
          <tbody>
          <tr v-for="table in tables" :key="table.id">
            <td>{{ table.number }}</td>
            <td>{{ table.numberOfSeats }}</td>
            <td>{{ table.price }}</td>
            <td>
              <ul>
                <li v-for="reserved in table.reserveds" :key="reserved.id" v-if="table.reserveds.length > 0">
                  {{ formatDateTime(reserved.start) }} - {{ formatDateTime(reserved.end) }}
                </li>
                <div v-else>Свободен</div>
              </ul>
            </td>
          </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>

<script setup>
import {ref, onMounted} from 'vue';
import axios from 'axios';
import NavLine from "@/components/admin/nav-line.vue";

const tables = ref([]);

onMounted(async () => {
  try {
    const response = await axios.post('http://localhost:5178/api/Table/Select');
    tables.value = response.data.data;
  } catch (error) {
    console.error('Error fetching tables data:', error);
  }
});

function formatDateTime(dateTime) {
  const options = {year: 'numeric', month: '2-digit', day: '2-digit', hour: '2-digit', minute: '2-digit'};
  return new Date(dateTime).toLocaleString('ru-RU', options);
}
</script>

<style scoped>
.content {
  background-color: #fff;
  color: #333;
  display: flex;
}

h2 {
  font-size: 24px;
  margin-bottom: 20px;
  text-align: center;
}

table {
  width: 100%;
  border-collapse: collapse;
  margin: 20px 180px;
}

table th,
table td {
  padding: 10px;
  border-bottom: 1px solid #ccc;
  text-align: left;
}

table th {
  background-color: #f2f2f2;
  font-weight: bold;
}

ul {
  margin: 0;
  padding: 0;
  list-style-type: none;
}

li {
  margin-bottom: 5px;
}

</style>
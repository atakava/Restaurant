<script setup>
import { ref, onMounted } from 'vue';
import TableTwo from "@/components/main/TableTwo.vue";
import TableFour from "@/components/main/TableFour.vue";
import Sofa from "@/components/main/sofa.vue";
import axios from 'axios';

const isShow = ref(false);
const table = ref(null);

const user = ref(null);

async function fetchUserData() {
  try {
    const response = await axios.post('http://localhost:5178/api/Client/CurrentUser', null, {withCredentials: true});
    user.value = response.data.data;
    console.log(user);
  } catch (error) {
    console.error('Error fetching user data:', error);
  }
}

function openModal(tableNumber) {
  axios.post('http://localhost:5178/api/Table/GetByNumber', {
    number: tableNumber
  })
      .then(response => {
        console.log(response.data);
        table.value = response.data.data;
        isShow.value = true;
      })
      .catch(error => {
        console.error('Error fetching table information:', error);
      });
}

const reservationDate = ref('');
const reservationTime = ref('');

async function submitReservation() {
  const reservationData = {
    clientId: user.value.id,
    tableId: table.value.id,
    tableNumber: table.value.number,
    start: new Date(`${reservationDate.value}T${reservationTime.value}`),
    end: new Date(`${reservationDate.value}T${reservationTime.value}`),
    totalPrice: 110 
  };

  try {
    const response = await axios.post('http://localhost:5178/api/ReservationTable/Create', reservationData);
    console.log(response.data);
  } catch (error) {
    console.error('Error submitting reservation:', error);
  }

  openModal(reservationData.tableNumber);
}

function formatReservationDate(dateString) {
  const date = new Date(dateString);
  const day = date.getDate();
  const month = date.toLocaleString('default', { month: 'short' });
  const hours = date.getHours();
  const minutes = date.getMinutes();
  return `Столик зарезервирован на ${day} ${month} в ${hours}:${minutes}`;
}

onMounted(() => {
  fetchUserData();
});
</script>

<template>
  <div class="wrapper container">
    <h1>Забронировать столик</h1>
    <div class="popup" v-if="table != null" :class="{'no-active': isShow === false}">
      <div class="header">
        <p>Бронирование в Bento Box</p>
        <div class="close" @click="isShow = false"><img src="/png/close.png" alt=""></div>
      </div>
      <div class="body">
        <div>Стол номер: {{ table.number }}</div>
        <div class="input-block">
          <label for="date">Дата:</label>
          <input type="date" id="date" v-model="reservationDate">
        </div>
        <div class="input-block">
          <label for="time">Время:</label>
          <input type="time" id="time" v-model="reservationTime">
        </div>
        <div class="resrveds" >
          <div v-for="item in table.reserveds">{{formatReservationDate(item.start)}}</div>
        </div>
        <button @click="submitReservation">Забронировать</button>

      </div>
    </div>
    <div class="center">
      <div class="scheme">
        <div>
          <TableTwo :table="1" @click="openModal(1)"/>
          <TableTwo :table="2" @click="openModal(2)"/>
          <TableTwo :table="3" @click="openModal(3)"/>
          <TableTwo :table="4" @click="openModal(4)"/>
          <TableTwo :table="5" @click="openModal(5)"/>
          <TableTwo :table="6" @click="openModal(6)"/>
          <TableTwo :table="7" @click="openModal(7)"/>
          <TableTwo :table="8" @click="openModal(8)"/>
        </div>
        <div>
          <table-four :table="9" @click="openModal(9)"></table-four>
          <table-four :table="10" @click="openModal(10)"></table-four>
          <table-four :table="11" @click="openModal(11)"></table-four>
          <table-four :table="12" @click="openModal(12)"></table-four>
          <table-four :table="13" @click="openModal(13)"></table-four>
        </div>
        <div>
          <sofa :table="14" @click="openModal(13)"></sofa>
          <sofa :table="15" @click="openModal(15)"></sofa>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>


.header {
  font-size: 32px;
  height: 106px;
  padding: 18px 34px;
  color: #fff;
  background: #262D2F;
  position: relative;
}

.close {
  position: absolute;
  top: 3%;
  right: 3%;
  cursor: pointer;
}

.input-block {
  margin: 15px 0;
  display: flex;
  flex-direction: column;
}

.close img {
  width: 30px;
  height: 30px;
}

.header p {
  width: 250px;
}

.body {
  color: #121212;
  padding: 20px;
}

.popup {
  transition: .3s;
  position: fixed;
  width: 443px;
  height: 730px;
  background: #fff;
  top: 50%;
  right: 0;
  transform: translateY(-50%);
  z-index: 2;
}

.no-active {
  width: 0;
  opacity: 0;
}

.body label {
  margin-bottom: 10px;
}

.body input {
  padding: 8px;
  border: 1px solid #ccc;
  border-radius: 5px;
  margin-bottom: 15px;
}

.body button {
  color: #212121;
  background-color: transparent;
  border: 2px solid #212121;
  width: 280px;
  height: 40px;
  position: absolute;
  right: 2.5%;
  bottom: 5%;
}

.wrapper h1 {
  text-align: center;
  margin: 45px 0;
}

.center {
  display: flex;
  justify-content: center;
}

.scheme {
  position: relative;
  background-image: url('/png/room.png');
  width: 866px;
  height: 640px;
}

.container {
  position: relative;
}

.resrveds {
  width: 100%;
  background: #363C4480;
  height: 270px;
}

.resrveds div {
  background: #BD4B4B;
  color: #fff;
  padding: 10px;
}
</style>
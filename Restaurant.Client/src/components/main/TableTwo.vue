<script setup>

defineProps({
  table: Number
})

let isShow = false;
</script>

<template>
  <div class="table" @click="isShow = !isShow">
    <div>{{ table }}</div>
  </div>
</template>

<style scoped>
.table {
  color: #fff;
  position: relative;
  width: 42px;
  height: 72px;
  display: inline-block;
  cursor: pointer;
}

.table div {
  margin: 26px 0;
  text-align: center;
}

.table:nth-child(1) {
  background-image: url('/svg/1.svg');
  top: 50px;
  left: 380px;
}

.table:nth-child(2) {
  background-image: url('/svg/1.svg');
  top: 50px;
  left: 410px;
}

.table:nth-child(3) {
  background-image: url('/svg/1.svg');
  top: 150px;
  left: 296px;
}

.table:nth-child(4) {
  background-image: url('/svg/1.svg');
  top: 150px;
  left: 326px;
}

.table:nth-child(5) {
  background-image: url('/svg/1.svg');
  top: 250px;
  left: 211px;
}


.table:nth-child(6) {
  background-image: url('/svg/1.svg');
  top: 350px;
  left: 168px;
}

.table:nth-child(7) {
  background-image: url('/svg/1.svg');
  top: 250px;
  left: 200px;
}

.table:nth-child(8) {
  background-image: url('/svg/1.svg');
  top: 350px;
  left: 158px;
}
</style>
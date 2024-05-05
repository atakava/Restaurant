<script setup>

defineProps({
  table: Number
})

</script>

<template>
  <div class="tableTwo">
    <div>{{ table }}</div>
  </div>
</template>

<style scoped>
.tableTwo {
  color: #fff;
  position: relative;
  width: 76px;
  height: 72px;
  display: inline-block;
  cursor: pointer;
}

.tableTwo div {
  margin: 26px 0;
  text-align: center;
}

.tableTwo:nth-child(1) {
  background-image: url('/svg/2.svg');
  top: 65px;
  left: 529px;
}

.tableTwo:nth-child(2) {
  background-image: url('/svg/2.svg');
  top: -26px;
  left: 535px;
}

.tableTwo:nth-child(3) {
  background-image: url('/svg/2.svg');
  top: 150px;
  left: 460px;
}

.tableTwo:nth-child(4) {
  background-image: url('/svg/2.svg');
  top: 255px;
  left: 300px;
}

.tableTwo:nth-child(5) {
  background-image: url('/svg/2.svg');
  top: 340px;
  left: 310px;
}
</style>
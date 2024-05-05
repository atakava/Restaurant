<script setup>

defineProps({
  table: Number
})

</script>

<template>
  <div class="sofa">
    <div>{{ table }}</div>
  </div>
</template>

<style scoped>
.sofa {
  color: #fff;
  position: relative;
  width: 78px;
  height: 156px;
  display: inline-block;
  cursor: pointer;
}

.sofa div {
  margin: 76px 16px;
}

.sofa:nth-child(1) {
  background-image: url('/svg/3.svg');
  top: -100px;
  left: 750px;
}

.sofa:nth-child(2) {
  background-image: url('/svg/3.svg');
  top: 180px;
  left: 670px;
}

</style>
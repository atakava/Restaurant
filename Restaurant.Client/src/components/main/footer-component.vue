<script setup>

</script>

<template>
  <div class="footer wrapper">
    <img class="tg" src="/svg/tg.png" alt="tg">
    <div class="callback">
      <h1>Позвонить нам</h1>
      <p>+7(999)999-99-99</p>
    </div>

    <div class="garfic">
      <h1>График работы</h1>
      <p>ВС-ЧТ - 12:00-00:00</p>
      <p>ПТ-СБ - 12:00-05:00</p>
    </div>
    <div class="address">
      <h1>Адрес</h1>
      <p>Москва, Лесная,</p>
      <p>20 стр. 5</p>
    </div>
  </div>
</template>

<style scoped>
.footer {
  margin-top: 45px;
  margin-bottom: 45px;
  display: flex;
  justify-content: space-between;
  text-align: center;
}

p  {
  margin: 15px 0;
}

img {
  width: 75px;
  height: 75px;
}
</style>
<script setup>

import HeaderComponent from "@/components/main/header-component.vue";
</script>

<template>
  <div class="container">
    <header-component class="header"/>
    <div class="overlay"/>
    <div class="text zcool-font">Добро пожаловать в наш уютный уголок Японии! Насыщенные вкусом суши, свежие роллы и изысканные
      блюда ждут вас здесь. Ресторан японской кухни Bento Box - это место, где каждый кусочек представляет собой
      произведение искусства.
    </div>
<!--    <button class="reserved">Забронировать</button>-->
  </div>
</template>

<style scoped>
.container {
  height: 100vh;
  width: 100%;
  background-image: url("/png/bg-intro.png");
}

.header {
  position: relative;
  z-index: 1;
}

.overlay {
  z-index: 0;
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100vh;
  background: rgba(0, 0, 0, 40%);
}

.text {
  position: absolute;
  top: 38%;
  right: 12%;
  z-index: 0;
  font-size: 24px;
  color: #fff;
  width: 517px;
  height: 217px;
  text-align: end;
  line-height: 40px;
  text-shadow: 3px 3px 2px #000;
}

.reserved {
  position: absolute;
  top: 85%;
  left: 50%;
  transform: translateX(-50%);
  z-index: 1;
  width: 258px;
  height: 60px;
}
</style>
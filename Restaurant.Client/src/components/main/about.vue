<script setup>

</script>

<template>
  <div class="content wrapper zcool-font">
    <div class="img-container">
      <img src="/png/1.png" alt="img">
      <img src="/png/3.png" alt="img">
      <img src="/png/2.png" alt="img">
    </div>
    <div class="text-content">
      <h3 class="zcool-font">Ресторан японской кухни Bento Box</h3><br>
      <br>
      <p class="zcool-font">Ресторан Bento Box представляет собой истинное японское кулинарное путешествие, где каждое
        блюдо — настоящее
        произведение искусства. Наше меню наполнено разнообразием изысканных суши, традиционных японских раменов и
        аутентичных бенто боксов, подготовленных с использованием только свежих и высококачественных ингредиентов.</p>
      <br>
      <p class="zcool-font">Кулинары Bento Box — настоящие мастера своего дела, каждое блюдо готовится с большой любовью
        и вниманием к
        деталям. Наш дружелюбный и профессиональный персонал всегда готов принять вас с радушием и предложить лучший
        сервис. Мы стремимся к идеальному сочетанию атмосферы комфорта, отличной кухни и высокого уровня обслуживания,
        чтобы каждый наш гость почувствовал себя как дома и насладился неповторимым вкусом японской кухни.
      </p>
      <br>
      <p class="zcool-font">Посетив ресторан Bento Box, вы окунетесь в атмосферу Японии и останетесь довольны как
        качеством блюд, так и
        вниманием персонала к каждой детали вашего визита.</p>
      <router-link to="menu" class="btn-text">
        <button class="menu-nav">
          В меню
        </button>
      </router-link>
    </div>
  </div>
</template>

<style scoped>

.content {
  position: relative;
  color: #fff;
  display: flex;
  justify-content: space-between;
  padding-top: 45px;
}

.img-container {
  display: flex;
  flex-direction: column;
}

.img-container img {
  width: 400px;
  height: 205px;
  margin-bottom: 25px;
}

.text-content {
  padding-left: 50px;
  font-size: 20px;
}

.text-content h3 {
  font-size: 32px;
}

.text-content p {
  text-align: justify;
}

.menu-nav {
  position: absolute;
  top: 85%;
  right: 0;
  width: 270px;
  height: 55px;
}

</style>
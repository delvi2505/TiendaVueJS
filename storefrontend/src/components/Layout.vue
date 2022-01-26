<template>
  <div class="LayoutDefault">
    <my-nav-bar/>
    <main class="LayoutDefault__main">
      <car/>
      <slot/>
    </main>
    <footer class="LayoutDefault__footer">
      &copy; 2022 Bienvenido Hernaández
    </footer>
  </div>
</template>
<script>
import Car from './Car.vue'
import MyNavBar from './NavBar.vue'
import Charging from './Charging.vue'
import Error from './Error.vue'

export default {
  name: 'layout',
  components: {Car, MyNavBar, Charging, Error}
}
</script>

<template>
  <div v-if="showComponent">
    <nav class="navbar" role="navigation" aria-label="main navigation">
      <div class="navbar-brand">
        <a class="navbar-item" href="https://bulma.io">
          <img src="https://bulma.io/images/bulma-logo.png" width="112" height="28">
        </a>

        <a role="button" class="navbar-burger" aria-label="menu" aria-expanded="false" data-target="navbarBasicExample">
          <span aria-hidden="true"></span>
          <span aria-hidden="true"></span>
          <span aria-hidden="true"></span>
        </a>
      </div>

      <div id="navbarBasicExample" class="navbar-menu">
        <div class="navbar-start">
          <strong class="navbar-item">
            {{customer.nombre +' '+ customer.apellido}}
          </strong>

          <router-link to="/Products" class="navbar-item">
            Articulos
          </router-link>
        </div>

        <div class="navbar-end">
          <div class="navbar-item">
            <div class="buttons">
              <a class="button is-primary" v-on:click="logout()">
                <strong>Salir</strong>
              </a>
            </div>
          </div>
        </div>
      </div>
    </nav>
  </div>
</template>

<script>
document.addEventListener('DOMContentLoaded', () => {
  // Get all "navbar-burger" elements
  const $navbarBurgers = Array.prototype.slice.call(document.querySelectorAll('.navbar-burger'), 0)

  // Check if there are any navbar burgers
  if ($navbarBurgers.length > 0) {
    // Add a click event on each of them
    $navbarBurgers.forEach(el => {
      el.addEventListener('click', () => {
        // Get the target from the "data-target" attribute
        const target = el.dataset.target
        const $target = document.getElementById(target)

        // Toggle the "is-active" class on both the "navbar-burger" and the "navbar-menu"
        el.classList.toggle('is-active')
        $target.classList.toggle('is-active')
      })
    })
  }
})

export default {
  name: 'NavBar',
  data () {
    return {
      showComponent: false,
      customer: {}
    }
  },
  mounted () {
    this.showThisComponent()
  },
  methods: {
    showThisComponent () {
      this.loading = true
      if (localStorage.getItem('customer')) {
        this.customer = JSON.parse(localStorage.getItem('customer'))
        this.showComponent = true
      } else {
        this.showComponent = false
      }
    },
    logout () {
      localStorage.removeItem('customer')
      this.$router.push('/')
    }
  }
}

</script>

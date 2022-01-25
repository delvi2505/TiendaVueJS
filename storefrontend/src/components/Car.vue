<template>
  <div v-if="showComponent" class="columns">
    <div v-if="errored">
      <Error/>
    </div>
    <div v-else-if="loading">
      <Charging/>
    </div>
    <div v-else class="column is-4 is-offset-8" style="text-align:center;">
      <router-link v-bind:to="'/ListCar/' + this.car.id">
        <img v-bind:src="urlImage" width="100" height="100"/>
      </router-link>
      <div>
        <span class="tag is-success is-medium is-rounded">{{car.cantProductos}} Art.</span>
        <span class="tag is-primary is-medium is-rounded">RD$ {{car.totalApagar}}</span>
      </div>
    </div>
  </div>
</template>
<script>
import axios from 'axios'
import Error from './Error.vue'
import Charging from './Charging.vue'

export default {
  components: { Error, Charging },
  name: 'Car',
  data () {
    return {
      loading: true,
      errored: false,
      urlImage: require('@/assets/car.png'),
      showComponent: false,
      customer: {},
      car: {}
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
        axios
          .get('http://localhost:5110/GetCar/' + this.customer.id)
          .then(response => {
            this.car = response.data
            this.loading = false
            this.errored = false
          })
          .catch(error => {
            console.log(error)
            this.loading = false
            this.errored = true
          })
        this.showComponent = true
      } else {
        this.showComponent = false
      }
    }
  }
}
</script>

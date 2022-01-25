<template>
  <div class="columns">
    <div class="column" v-if="errored">
      <Error/>
    </div>
    <div class="column" v-else-if="loading">
      <Charging/>
    </div>
    <div v-else class="column" style="text-align: center;">
      <h1 class="title">Tienda en linea Vue.js</h1>
      <form class="form" onsubmit="event.preventDefault();" v-on:submit="redirection()">
        <div class="select is-normal is-rounded">
          <select v-model="customerSelected" required>
            <option disabled value="">Iniciar como:</option>
            <option v-for="customer in customers" :key="customer" :value="customer">{{customer.nombre}}</option>
          </select>
        </div>
        <button type="submit" class="button is-info">Continuar</button>
      </form>
    </div>
  </div>
</template>
<script>
import axios from 'axios'
import Error from './Error.vue'
import Charging from './Charging.vue'

export default {
  components: { Error, Charging },
  name: 'Customer',
  data () {
    return {
      loading: true,
      errored: false,
      customerSelected: {},
      customers: []
    }
  },
  mounted () {
    this.getCustomers()
  },
  methods: {
    getCustomers () {
      this.loading = true
      axios
        .get('http://localhost:5110/GetClientes')
        .then(response => {
          this.customers = response.data
          this.loading = false
          this.errored = false
        })
        .catch(error => {
          console.log(error)
          this.loading = false
          this.errored = true
        })
    },
    redirection () {
      localStorage.removeItem('customer')
      localStorage.setItem('customer', JSON.stringify(this.customerSelected))
      this.$router.push('/Products')
    }
  }
}
</script>

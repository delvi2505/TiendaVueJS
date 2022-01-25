<template>
  <layout>
    <div>
      <div v-if="errored">
        <error/>
      </div>
      <div v-else-if="loading">
        <charging/>
      </div>
      <div v-else>
        <img v-bind:src="product.urlImagen" width="200" height="200">
        <h3 class="subtitle">{{product.nombre}} - <span id="precio">Precio RD$: {{product.precio}}</span> </h3>
        <strong>Cantidad Disponible: {{product.stock}}</strong>
        <p class="mb-4">{{product.descripcion}}</p>
        <button v-on:click="addToCar()" class="button is-success">Agregar al Carrito</button>
      </div>
    </div>
  </layout>
</template>
<script>
import axios from 'axios'
import Layout from './Layout.vue'

export default {
  components: { Layout },
  name: 'Product',
  data () {
    return {
      loading: true,
      errored: false,
      product: {},
      customer: {}
    }
  },
  mounted () {
    this.getProduct(this.$route.params.id)
  },
  methods: {
    getProduct (id) {
      this.loading = true
      axios
        .get('http://localhost:5110/GetProduct/' + id)
        .then(response => {
          this.product = response.data
          this.loading = false
          this.errored = false
        })
        .catch(error => {
          console.log(error)
          this.loading = false
          this.errored = true
        })
    },
    addToCar () {
      this.loading = true
      this.customer = JSON.parse(localStorage.getItem('customer'))
      var sendData = {
        IdCliente: this.customer.id,
        IdProducto: this.product.id
      }
      axios
        .post('http://localhost:5110/AddToCar', sendData)
        .then(response => {
          this.loading = false
          this.errored = false
          if (response.data === 'OK') {
            // alert('Articulo ' + this.product.nombre + ' agregado al carrito')
          } else {
            alert(response.data)
            console.log(response.data)
          }
        })
        .catch(error => {
          console.log(error)
          this.loading = false
          this.errored = true
        })
    }
  }
}
</script>
<style scoped>
#precio{
  color: green;
}
</style>

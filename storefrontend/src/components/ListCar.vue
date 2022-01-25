<template>
  <layout>
    <div id="container">
      <div v-if="errored">
        <error/>
      </div>
      <div v-else-if="loading">
        <charging/>
      </div>
      <div v-else>
        <button v-on:click="purchase()" class="button is-success">Comprar Ahora</button>
        <h1 class="title">Articulos en el Carrito</h1>
        <hr>
        <table class="table is-hoverable is-striped">
          <thead>
            <tr>
              <th></th>
              <th>Nombre</th>
              <th>Precio</th>
              <th></th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="product in products" :key="product.id">
              <td><img v-bind:src="product.UrlImagen" width="100" height="100" /></td>
              <td>{{product.Nombre}}</td>
              <td>{{product.Precio}}</td>
              <td><button v-on:click="goToProduct(product.Id)" class="button is-info">Ver Articulo</button></td>
              <td><button v-on:click="deleteFromCar(product.IdDetalle)" class="button is-danger">Quitar Articulo</button></td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </layout>
</template>
<script>
import axios from 'axios'
import Layout from './Layout.vue'

export default {
  components: { Layout },
  name: 'ListCar',
  data () {
    return {
      loading: true,
      errored: false,
      products: [],
      customer: {}
    }
  },
  mounted () {
    this.customer = JSON.parse(localStorage.getItem('customer'))
    this.getProducts()
  },
  methods: {
    getProducts () {
      this.loading = true
      axios
        .get('http://localhost:5110/GetProductFromCar/' + this.customer.id)
        .then(response => {
          this.products = response.data
          this.loading = false
          this.errored = false
        })
        .catch(error => {
          console.log(error)
          this.loading = false
          this.errored = true
        })
    },
    goToProduct (id) {
      this.$router.push('/Product/' + id)
    },
    deleteFromCar (id) {
      this.loading = true
      axios
        .delete('http://localhost:5110/DeleteFromCar/' + id)
        .then(response => {
          this.loading = false
          this.errored = false
          if (response.data === 'OK') {
            this.getProducts()
          } else {
            alert(response.data)
          }
        })
        .catch(error => {
          console.log(error)
          this.loading = false
          this.errored = true
        })
    },
    purchase () {
      this.loading = true
      axios
        .post('http://localhost:5110/Purchase/' + this.customer.id, this.products)
        .then(response => {
          this.loading = false
          this.errored = false
          if (response.data === 'OK') {
            this.getProducts()
          } else {
            alert(response.data)
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

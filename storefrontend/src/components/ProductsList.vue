<template>
  <layout>
    <div class="container">
      <div v-if="errored">
        <error/>
      </div>
      <div v-else-if="loading">
        <charging/>
      </div>
      <div v-else>
        <h1 class="title">Articulos</h1>
        <hr/>
        <table id="tableProducts" class="table is-hoverable is-striped">
          <thead>
            <tr>
              <th></th>
              <th>Nombre</th>
              <th>Precio</th>
              <th>Disponible</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="product in products" :key="product.id">
              <td><img v-bind:src="product.urlImagen" width="100" height="100" /></td>
              <td>{{product.nombre}}</td>
              <td>{{product.precio}}</td>
              <td>{{product.stock}}</td>
              <td><button v-on:click="goToProduct(product.id)" class="button is-info" >Ver Articulo</button></td>
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
  name: 'CustomerList',
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
        .get('http://localhost:5110/GetProducts')
        .then(response => {
          this.products = response.data
          this.$store.dispatch('updateCarAction')
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
    }
  }
}

</script>

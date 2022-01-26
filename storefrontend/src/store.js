import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    car: {}
  },
  mutations: {
    updateCarMutation (state, value) {
      state.car = value
    }
  },
  actions: {
    updateCarAction (context) {
      if (localStorage.getItem('customer')) {
        this.customer = JSON.parse(localStorage.getItem('customer'))
        axios
          .get('http://localhost:5110/GetCar/' + this.customer.id)
          .then(response => {
            context.commit('updateCarMutation', response.data)
          })
          .catch(error => {
            console.log(error)
          })
      }
    }
  },
  getters: {
    getCar (state) {
      return state.car
    }
  }
})

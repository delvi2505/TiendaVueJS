import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Customer',
      component: () => import('@/components/Customer')
    },
    {
      path: '/Products',
      name: 'Products',
      component: () => import('@/components/ProductsList')
    },
    {
      path: '/Product/:id',
      name: 'Product',
      component: () => import('@/components/Product')
    },
    {
      path: '/ListCar/:id',
      name: 'ListCar',
      component: () => import('@/components/ListCar')
    }
  ]
})

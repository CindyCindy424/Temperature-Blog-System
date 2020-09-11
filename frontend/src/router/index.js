import Vue from 'vue'
import Router from 'vue-router'
import Login from '../components/Login.vue'
import Register from '../components/Register.vue'
import MySpace from '../components/MySpace.vue'
import MainPage from '../components/MainPage.vue'
Vue.use(Router)
const routes = [{
  path: '/',
  name: 'Login',
  component: Login
},
{
  path: '/Login',
  name: 'Login',
  // route level code-splitting
  // this generates a separate chunk (about.[hash].js) for this route
  // which is lazy-loaded when the route is visited.
  // component: () =>
  // import(/* webpackChunkName: "about" */ '../views/About.vue')
  // }
  component: Login
},
{
  path: '/Register',
  name: 'Register',
  component: Register
},
{
  path: '/MySpace',
  name: 'MySpace',
  component: MySpace
},
{
  path: '/MainPage',
  name: 'MainPage',
  component: MainPage
}
]

export default new Router({
  routes
})

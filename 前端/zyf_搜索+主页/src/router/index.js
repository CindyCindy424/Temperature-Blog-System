import Vue from 'vue'
import Router from 'vue-router'
import Login from '../components/Login.vue'
import Register from '../components/Register.vue'
import MySpace from '../components/MySpace.vue'
import SearchResult from '../components/SearchResult.vue'
import MyHome from '../components/MyHome.vue'
import Album from '../components/Album.vue'
import MyArticle from '../components/MyArticle.vue'
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
  path: '/SearchResult',
  name: 'SearchResult',
  component: SearchResult
},
{
  path: '/MyHome',
  name: 'MyHome',
  component: MyHome
},
{
  path: '/Album',
  name: 'Album',
  component: Album
},
{
  path: '/MyArticle',
  name: 'MyArticle',
  component: MyArticle
}
]

export default new Router({
  routes
})

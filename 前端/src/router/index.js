import Vue from 'vue'

import Router from 'vue-router'
import Login from '../components/Login.vue'
import Register from '../components/Register.vue'
import MySpace from '../components/MySpace.vue'
import Album from '../components/Album.vue'
import Photo from '../components/Photo.vue'
import favorite from '@/components/favorite'
//import ViewArticle from '@/components/ViewArticle'
// import ViewPhoto from '@/components/ViewPhoto'
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
  path: '/Album',
  name: 'Album',
  component: Album
},
{
  path: '/Photo',
  name: 'Photo',
  component: Photo
},
{
  path: '/favorite',
  name: 'favorite',
  component: favorite
},
// {
//   path: '/ViewArticle',
//   name: 'ViewArticle',
//   component: ViewArticle
// }
// {
//   path: '/ViewPhoto',
//   name: 'ViewPhoto',
//   component: ViewPhoto
// }
]

export default new Router({
  routes
})

import Vue from 'vue'

import Router from 'vue-router'
import Login from '../components/Login.vue'
import Register from '../components/Register.vue'
import MySpace from '../components/MySpace.vue'
import Album from '../components/Album.vue'
import Photo from '../components/Photo.vue'
import favorite from '@/components/favorite'
import UploadArticle from '../components/UploadArticle.vue'
import MyArticle from '../components/MyArticle.vue'
import ViewArticle from '../components/ViewArticle.vue'
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
{
  path: '/UploadArticle',
  name: 'UploadArticle',
  component: UploadArticle
},
{
  path: '/MyArticle',
  name: 'MyArticle',
  component: MyArticle
},
{
  path: '/ViewArticle',
  name: 'ViewArticle',
  component: ViewArticle
},
]

export default new Router({
  routes
})

import Vue from 'vue'
import Router from 'vue-router'
import Login from '../components/Login.vue'
import Register from '../components/Register.vue'
import MySpace from '../components/MySpace.vue'
import MainPage from '../components/MainPage.vue'
import Album from '../components/Album.vue'
import Photo from '../components/Photo.vue'
import Favourite from '../components/Favourite.vue'
Vue.use(Router)
const routes = [{
  path: '/',
  name: 'Login',
  component: Login
},
{
  path: '/Favourite',
  name: 'Favourite',
  component: Favourite
},
{
  path: '/Photo',
  name: 'Photo',
  component: Photo
},
{
  path: '/Album',
  name: 'Album',
  component: Album
},
{
  path: '/Login',
  name: 'Login',
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

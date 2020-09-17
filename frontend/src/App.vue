<template>
<div id="app">
    <div :class="['navigation']">
        <img src="./assets/images/TempratureLogo.png" :class="{logo:isLogo}">
        <router-link to="/MainPage" :class="['title']">Temperature</router-link>
        <input :class="['search']">
        <el-button icon="el-icon-search" circle style="position:absolute;top:10px;left:40%;color="></el-button>
        <el-menu :default-active="activeIndex" class="el-menu-demo" mode="horizontal" text-color="#FFFFFF"
        @select="handleSelect">
          <el-menu-item index="/favourite" v-if='isLogin' :class="['favourite']" @click="this.pushToFavourite">收藏夹</el-menu-item>
          <el-submenu index="/compose" v-if='isLogin' :class="['compose']">
            <template slot="title">创作</template>
            <el-menu-item index="/compose/text" style="color:black;font-size:20px;height:40px;border-bottom:2px solid grey;">发表文章</el-menu-item>
            <el-menu-item index="/compose/topic" style="color:black;font-size:20px;height:40px;border-bottom:2px solid grey;">发表话题</el-menu-item>
            <el-menu-item index="/compose/picture" style="color:black;font-size:20px;height:40px;" @click="this.pushToPic">图片</el-menu-item>
          </el-submenu>
        </el-menu>
        <router-link  to="/Login"   :class="['login']" v-show="isLogin==0">登录</router-link>
        <router-link to="/Register" :class="['register']" v-show="isLogin==0">注册</router-link>
        <el-avatar v-show="isLogin==1" style="position:absolute;top:10px;left:93%;" :src="this.avator"></el-avatar>
        <router-link :to="{path:'/MySpace',query:{account:this.account}}" v-text="account" :class="['mySpace']" v-show="isLogin"></router-link>
    </div>
    <router-view ref="view"  @turnToRegister="turnToRegister()" @loginSuc="loginSuccess()" @propAccount="changeAccount" @updateToken='updateToken()'></router-view>
</div>
</template>

<script>
export default {
  name: 'app',
  data: function () {
    return {
      currentPage: 'Login',
      isLogo: true,
      isLogin: false,
      isRegister: true,
      account: '',
      activeIndex: '1',
      activeIndex2: '1',
      avator: '',
      token: '',
      userID: ''
    }
  },
  created () {
    this.token = document.cookie.split(';')[0].split('=')[1]
    if (typeof (this.token) === 'undefined' && this.isLogin === true) {
      this.isLogin = false
      this.$router.push({path: '/login'})
      window.alert('登录已过期，请重新登录')
    }
    if (this.isLogin === false) {
      this.$router.push({path: '/login'})
    }
  },
  methods: {
    loginSuccess: function () {
      this.isLogin = true
    },
    getUserID: function () {
      var xhr = new XMLHttpRequest()
      xhr.onreadystatechange = () => {
        var response = xhr.responseText
        var returnModel = JSON.parse(response)
        this.userID = returnModel.userInfo.userId
      }
      var headerToken = document.cookie.split(';')[0].split('=')[1]
      xhr.open('POST', 'http://139.224.255.43:7779/Account/getUserInfoByNickName?nick_name=' + this.account)
      xhr.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      xhr.send()
    },
    changeAccount: function (evtValue) {
      this.account = evtValue
    },
    handleSelect (key, keyPath) {
      console.log(key, keyPath)
    },
    updateToken: function () {
      this.token = this.$refs.view.token
      var exdate = new Date()
      exdate.setTime(exdate.getTime() + 20 * 60 * 1000)
      document.cookie = 'token=' + this.token + ';expires=' + exdate.toUTCString()
    },
    turnToRegister: function () {
      this.currentPage = 'Register'
    },
    pushToPic: function () {
      this.$router.push({path: '/Album', query: {userID: this.userID, currentUserID: this.userID}})
    },
    pushToFavourite: function () {
      this.$router.push({path: '/Favourite', query: {userID: this.userID, currentUserName: this.account}})
    }
  },
  watch: {
    account (oldVal, newVal) {
      var xhr2 = new XMLHttpRequest()
      xhr2.onreadystatechange = () => {
        if (xhr2.readyState === 4 && xhr2.status === 200) {
          var response = xhr2.responseText
          this.returnInfo = JSON.parse(response)
          this.getUserID()
          if (this.returnInfo.flag === 1) {
            this.avator = 'http://139.224.255.43:7779/BlogPics/Avator/' + this.returnInfo.path.split('\\')[2]
          }
        }
      }
      var headerToken = document.cookie.split(';')[0].split('=')[1]
      xhr2.open('POST', 'http://139.224.255.43:7779/Account/getAvatrResource?nick_name=' + this.account)
      xhr2.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      xhr2.send()
    }
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
.navigation {
    position: absolute;
    width: 100%;
    height: 63px;
    left: 0px;
    top: -1px;
    opacity: 1;
    z-index:999;
}
.logo {
    position: absolute;
    width: 38px;
    height: 38px;
    left: 5%;
    top: 10px;
}

.title {
    position: absolute;
    width: 173px;
    height: 33px;
    left: 7%;
    top: 15px;
    margin-top: 0%;
    font-family: Lucida Handwriting;
    font-style: italic;
    font-weight: normal;
    font-size: 24px;
    line-height: 33px;
    color: #FFFFFF;
    text-decoration: none;
}

.search {
    position: absolute;
    width: 339px;
    height: 35px;
    left: 25%;
    top: 12px;
    background: #E8E2E2;
    border-radius: 30px;
}

.message {
    position: absolute;
    width: 72px;
    height: 24px;
    left: 50%;
    top: 0px;
    font-family: Microsoft YaHei;
    font-style: normal;
    font-weight: normal;
    font-size: 18px;
    line-height: 24px;
    /* identical to box height */

    text-decoration-line: none;
    color: #FFFFFF;
}

.favourite {
    position: absolute;
    width: 100px;
    height: 24px;
    left: 60%;
    top: 0px;
    font-family: Microsoft YaHei;
    font-style: normal;
    font-weight: normal;
    font-size: 18px;
    line-height: 24px;
    /* identical to box height */
    text-decoration-line: none;
    color: #FFFFFF;
}

.compose {
    position: absolute;
    width: 100px;
    height: 24px;
    left: 70%;
    top: 0px;

    font-family: Microsoft YaHei;
    font-style: normal;
    font-weight: normal;
    font-size: 24px;
    line-height: 24px;
    text-decoration-line: none;
    /* identical to box height */
}
.mySpace {
    position: absolute;
    width: 36px;
    height: 24px;
    left: 95%;
    top: 19px;

    font-family: Microsoft YaHei;
    font-style: normal;
    font-weight: normal;
    font-size: 18px;
    line-height: 24px;
    /* identical to box height */
    color: #FFFFFF;
    text-decoration-line: none;
}

.login {
    position: absolute;
    width: 88px;
    height: 24px;
    left: 90%;
    top: 17px;
    font-family: Microsoft YaHei;
    font-style: normal;
    font-weight: normal;
    font-size: 18px;
    line-height: 24px;
    /* identical to box height */

    text-decoration-line: none;

    color: #FFFFFF;
}

.register {
    position: absolute;
    width: 88px;
    height: 24px;
    left: 92.5%;
    top: 17px;
    font-family: Microsoft YaHei;
    font-style: normal;
    font-weight: normal;
    font-size: 18px;
    line-height: 24px;
    /* identical to box height */

    text-decoration-line: none;

    color: #FFFFFF;
}
.el-submenu__title {
  font-size: 18px;
}
.el-menu{
  opacity: 1;
}
.el-menu-item{
  opacity:1;
  border-bottom: 0px;
}
.el-menu-item:hover{
  opacity:0.5!important;

}
.el-menu-item.is-active{
  background-color:transparent!important;
}

.el-submenu{
  opacity:1;
  border-bottom: 0px;
}
.el-submenu:hover{
  opacity:0.5!important;
  background-color:transparent!important;
}
.el-submenu.is-active{
  background-color:transparent!important;
}
</style>

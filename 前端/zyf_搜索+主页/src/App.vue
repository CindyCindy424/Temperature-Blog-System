<template>
<div id="app">
    <div :class="['navigation']">
        <img src="./assets/images/TempratureLogo.png" :class="{logo:isLogo}">
        <p :class="['title']">Temperature</p>
        <input :class="['search']" v-model="navSearchContent">
				<router-link :to="{path: '/SearchResult', query:{SearchContent:navSearchContent}}">
					<el-button icon="el-icon-search" circle style="position:absolute;top:10px;left:780px;color="></el-button>
				</router-link>
        <el-menu router :default-active="activeIndex2" class="el-menu-demo" mode="horizontal"
        @select="handleSelect"  background-color="#545c64" text-color="#FFFFFF" active-text-color="#FFFFFF">
          <el-menu-item index="/message" v-if='isLogin' :class="['message']">消息中心</el-menu-item>
          <el-menu-item index="/favourite" v-if='isLogin' :class="['favourite']">收藏夹</el-menu-item>
          <el-submenu index="/compose" v-if='isLogin' :class="['compose']">
            <template slot="title">创作</template>
            <el-menu-item index="/compose/text">发表文章</el-menu-item>
            <el-menu-item index="/TopicArea">发表话题</el-menu-item>
            <el-menu-item index="/Album">上传图片</el-menu-item>
          </el-submenu>
					<el-menu-item index="/Myspace" v-if='isLogin' :class="['space']">个人空间</el-menu-item>
        </el-menu>
        <router-link to="/Login"  :class="['login']" v-show="isLogin==0">登录</router-link>
        <router-link to="/Register" :class="['register']" v-show="isLogin==0">注册</router-link>
				<router-link to="/MyHome">
          <el-avatar v-show="isLogin==1" style="position:absolute;top:10px;left:1460px;" src="https://cube.elemecdn.com/0/88/03b0d39583f48206768a7534e55bcpng.png"></el-avatar>
				</router-link>
    </div>
    <router-view @loginSuc="loginSuccess()" @propAccount="changeAccount"></router-view>
    <footer>
      <p :style="footer">
       
      </p>
    </footer>
  </div>
</template>

<script>
export default {
  name: 'app',
  data: function () {
    return {
			navSearchContent: '',
      isLogo: true,
      isLogin: false,
      isRegister: true,
      account: '',
      activeIndex: '1',
      activeIndex2: '1',
      footer: {
        position: 'absolute',
        width: '997px',
        height: '130px',
        top: '950px',
        left: '35%',
        'font-family': 'Microsoft YaHei',
        'font-style': 'normal',
        'font-weight': 'normal',
        'font-size': '18px',
        'line-height': '24px',
        display: 'flex',
        'align-items': 'center',
        'text-align': 'center',
        color: '#999494'
      }
    }
  },
  mounted () {
    // 动态设置内容高度 让footer始终居底   header+footer的高度是100
    this.Height = document.documentElement.clientHeight - 100
    // 监听浏览器窗口变化
    window.onresize = () => { this.Height = document.documentElement.clientHeight - 100 }
    // this.$data.footer.top = document.documentElement.clientHeight
  },
  methods: {
    loginSuccess: function () {
      console.log('run in father!')
      this.isLogin = true
    },
    changeAccount: function (evtValue) {
      this.account = evtValue
      console.log('prop' + evtValue)
    },
    handleSelect (key, keyPath) {
      console.log(key, keyPath)
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
    background: #545c64;
		
}

.logo {
    position: absolute;
    width: 38px;
    height: 38px;
    left: 25px;
    top: 10px;
}

.title {
    position: absolute;
    width: 173px;
    height: 33px;
    left: 81px;
    top: 15px;
    margin-top: 0%;
    font-family: Lucida Handwriting;
    font-style: italic;
    font-weight: normal;
    font-size: 24px;
    line-height: 33px;
    color: #FFFFFF;
}

.search {
    position: absolute;
    width: 339px;
    height: 35px;
    left: 417px;
    top: 12px;
    background: #E8E2E2;
    border-radius: 30px;
}

.message {
    position: absolute;
    width: 110px;
    height: 24px;
    left: 832px;
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
    width: 110px;
    height: 24px;
    left: 969px;
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
    width: 110px;
    height: 24px;
    left: 1104px;
    top: 0px;

    font-family: Microsoft YaHei;
    font-style: normal;
    font-weight: normal;
    font-size: 18px;
    line-height: 24px;
    text-decoration-line: none;
    /* identical to box height */
    color: #FFFFFF;
}
.space {
	position: absolute;
	width: 110px;
	height: 24px;
	left: 1515px;
	top: 0px;
	
	font-family: Microsoft YaHei;
	font-style: normal;
	font-weight: normal;
	font-size: 18px;
	line-height: 24px;
	text-decoration-line: none;
	/* identical to box height */
	color: #FFFFFF;
}
.mySpace {
    position: absolute;
    width: 110px;
    height: 24px;
    left: 1500px;
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
    left: 1400px;
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
    left: 1450px;
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
</style>

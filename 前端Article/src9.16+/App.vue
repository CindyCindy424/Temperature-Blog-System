<template>
  <div id="app">
    <div :class="['navigation']">
      <img src="./assets/article/logo.png" :class="{logo:isLogo}" />
      <p :class="['title']">Temperature</p>
      <input :class="['search']" />
      <el-button icon="el-icon-search" circle style="position:absolute;top:10px;left:780px;color="></el-button>
      <el-menu
        :default-active="activeIndex2"
        class="el-menu-demo"
        mode="horizontal"
        @select="handleSelect"
        background-color="#545c64"
        text-color="#FFFFFF"
        active-text-color="#FFFFFF"
      >
        <el-menu-item index="/message" v-if="isLogin" :class="['message']">消息中心</el-menu-item>
        <el-menu-item index="/favourite" v-if="isLogin" :class="['favourite']"><router-link to="/favorite">收藏夹</router-link></el-menu-item>
        <el-submenu index="/compose" v-if="isLogin" :class="['compose']">
          <template slot="title">创作</template>
          <el-menu-item index="/compose/text"><router-link to="/UploadArticle">发表文章</router-link></el-menu-item>
          <el-menu-item index="/compose/topic"><router-link to="/MyArticle">发表话题</router-link></el-menu-item>
          <el-menu-item index="/compose/picture"><router-link to="/ViewArticle">上传图片</router-link></el-menu-item>
        </el-submenu>
      </el-menu>
      <router-link to="/Login" :class="['login']" v-show="isLogin==0">登录</router-link>
      <router-link to="/Register" :class="['register']" v-show="isLogin==0">注册</router-link>
      <el-avatar
        v-show="isLogin==1"
        style="position:absolute;top:10px;left:1460px;"
        src="https://cube.elemecdn.com/0/88/03b0d39583f48206768a7534e55bcpng.png"
      ></el-avatar>
      <router-link to="/MySpace" v-text="account" :class="['mySpace']" v-show="isLogin"></router-link>
      
    </div>
    <router-view @loginSuc="loginSuccess()" @propAccount="changeAccount"></router-view>
    <!--<footer>
      <p :style="footer">Copyright ©2020 Temperature team, All Rights Reserved.</p>
    </footer>-->
  </div>
</template>

<script>
export default {
  name: "app",
  components: {
  },
  data: function () {
    return {
      isLogo: true,
      isLogin: true,
      isRegister: true,
      account: "",
      activeIndex: "1",
      activeIndex2: "1",
      footer: {
        position: "absolute",
        width: "997px",
        height: "130px",
        top: "950px",
        left: "35%",
        "font-family": "Microsoft YaHei",
        "font-style": "normal",
        "font-weight": "normal",
        "font-size": "18px",
        "line-height": "24px",
        display: "flex",
        "align-items": "center",
        "text-align": "center",
        color: "#999494",
      },
    };
  },
  mounted() {
    // 动态设置内容高度 让footer始终居底   header+footer的高度是100
    this.Height = document.documentElement.clientHeight - 100;
    // 监听浏览器窗口变化
    window.onresize = () => {
      this.Height = document.documentElement.clientHeight - 100;
    };
    // this.$data.footer.top = document.documentElement.clientHeight
  },
  methods: {
    loginSuccess: function () {
      console.log("run in father!");
      this.isLogin = true;
    },
    changeAccount: function (evtValue) {
      this.account = evtValue;
      console.log("prop" + evtValue);
    },
    handleSelect(key, keyPath) {
      console.log(key, keyPath);
    },
  },
};
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
  z-index: 5;
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
  color: #ffffff;
}

.search {
  position: absolute;
  width: 339px;
  height: 35px;
  left: 417px;
  top: 12px;
  background: #e8e2e2;
  border-radius: 30px;
}

.message {
  position: absolute;
  width: 72px;
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
  color: #ffffff;
}

.favourite {
  position: absolute;
  width: 54px;
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
  color: #ffffff;
}

.compose {
  position: absolute;
  width: 36px;
  height: 24px;
  left: 1104px;
  top: 0px;

  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: normal;
  font-size: 24px;
  line-height: 24px;
  text-decoration-line: none;
  /* identical to box height */
  color: #ffffff;
}
.mySpace {
  position: absolute;
  width: 36px;
  height: 24px;
  left: 1412px;
  top: 19px;

  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: normal;
  font-size: 18px;
  line-height: 24px;
  /* identical to box height */
  color: #ffffff;
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

  color: #ffffff;
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

  color: #ffffff;
}
.el-submenu__title {
  font-size: 18px;
}
</style>

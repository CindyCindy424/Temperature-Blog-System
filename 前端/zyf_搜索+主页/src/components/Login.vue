<template>
  <div class="background" :style="backgroundDiv">
    <div :class="['intro']">
      <p :class="['introWord1']" align="left">{{introduction1}}
      </p>
      <p :class="['introWord2']" align="left">{{introduction2}}
      </p>
   </div>
   <div :class="['loginArea']">
      <p :class="['loginTitle']">登录Temperature</p>
      <el-alert v-if="isLoginSuccess===0" title="密码错误" type="error" show-icon
      style="position:absolute;top:320px;left:222px;width:300px;"></el-alert>
      <el-alert v-else-if="isLoginSuccess===1" title="登陆成功，正在跳转！" type="success" show-icon
      style="position:absolute;top:320px;left:222px;width:300px;"></el-alert>
      <table :class="['inputToken']">
        <tr>
          <td>用户名：</td>
        </tr>
        <tr>
          <td>密码：</td>
        </tr>
      </table>
      <input :class="['inputBox']" v-model="account">
      <input type="password" :class="['inputBox']" style="top:-30px;" v-model="password">
      <el-button type="danger" style="position:absolute;top:360px;left:480px;" @click="submit">登录</el-button>
   </div>
  </div>
</template>

<script>
export default {
  name: 'Login',
  data () {
    return {
      isBackground: true,
      backgroundDiv: {
        backgroundImage: 'url(' + require('../assets/images/babe-fyrcsrv8730487.jpg') + ')',
        backgroundRepeat: 'no-repeat'
      },
      introduction1: 'Temperature博客网是由数据库小组成员共同设计实现的，以分享生活，交流经验为主旨的博客网站。',
      introduction2: '加入Temperature的世界，用文字和图片尽情描绘生活的细节!',
      isLoginSuccess: -1,
      account: '',
      password: ''
    }
  },
  methods: {
    submit: function () {
      var seed = Math.floor(Math.random() * 1000)
      this.isLoginSuccess = seed % 2
      console.log(this.isLoginSuccess)
    }
  },
  watch: {
    isLoginSuccess: function (newIsLoginSuccess, oldIsLoginSuccess) {
      console.log('new' + newIsLoginSuccess + 'old' + oldIsLoginSuccess)
      if (newIsLoginSuccess === 0) {
        setTimeout(function () { this.isLoginSuccess = -1 }, 300)
      } else if (newIsLoginSuccess === 1) {
        console.log('run in son!')
        this.$emit('loginSuc')
        this.$emit('propAccount', this.account)
        console.log('origin ' + this.account + 'psw: ' + this.password)
      }
    }
  }
}
</script>

<style>
.background{
    position: absolute;
    width: 100%;
    height: 1339px;
    top:60px;
    left:0px;
}
.intro{
  position: absolute;
  width: 447px;
  height: 509px;
  left: 203px;
  top: 327px;

  background: rgba(48, 46, 46, 0.7);
  border-radius: 36px;

}
.introWord1{
    position: absolute;
    width: 291px;
    height: 371px;
    left: 50px;
    top: 100px;

    font-family: Microsoft YaHei;
    font-style: normal;
    font-weight: normal;
    font-size: 24px;
    line-height: 32px;
    color: #DDD8D8;
}
.introWord2{
    position: absolute;
    width: 291px;
    height: 371px;
    left: 50px;
    top: 250px;

    font-family: Microsoft YaHei;
    font-style: normal;
    font-weight: normal;
    font-size: 24px;
    line-height: 32px;

    color: #DDD8D8;
}
.loginArea{
  position: absolute;
  width: 615px;
  height: 509px;
  left: 586px;
  top: 327px;

  background: #FFFFFF;
  border: 1px solid #FFFFFF;
  box-sizing: border-box;
  border-radius: 28px;
}
.loginTitle{
  position: relative;
  width: 343px;
  height: 52.21px;
  left: 220px;
  top: 40px;
  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: normal;
  font-size: 24px;
  line-height: 32px;
  display: flex;
  align-items: center;
  text-align: center;

  color: #535151;
}
.inputToken{
  position: relative;
  width: 200px;
  height: 146.17px;
  left: 80px;
  top: 70px;

  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: normal;
  font-size: 18px;
  line-height: 24px;
  align-items: center;
  text-align: center;

  color: #000000;
}
.inputBox{
  position: relative;
  width: 327px;
  height: 45px;
  left: 80px;
  top: -60px;
  background: #FFFFFF;
  border: 1px solid #9C9A9A;
  box-sizing: border-box;
}
</style>

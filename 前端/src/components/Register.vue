<template>
    <div class="registerBackground" :style="backgroundDiv">
    <div class="registerArea">
        <p class="registerTitle">加入Temperature</p>
        <table :class="['inputToken']">
        <tr>
          <td>用户名：</td>
        </tr>
        <tr>
          <td>密码：</td>
        </tr>
        <tr>
          <td>确认密码：</td>
        </tr>
        <tr>
          <td>邮箱：</td>
        </tr>
        <tr>
          <td>手机号：</td>
        </tr>
        <tr>
          <td>微信号：</td>
        </tr>
      </table>
      <div>
      <input :class="['inputBox']" style="top:210px"  v-model="account">
      <p v-if="isAccountAvailable===0" style="position:absolute;left:510px;top:195px;font-family:Microsoft YaHei;color:red;" v-text="accountValidation"></p>
      <p v-else-if="isAccountAvailable===2" style="position:absolute;left:510px;top:205px;" v-loading="isAccountAvailable===2"></p>
      <i v-if="isAccountAvailable===1" style="position:absolute;left:500px;top:215px;color:#67C23A;height:100px;" class="el-icon-check"></i>
      <input type="password" :class="['inputBox']" style="top:270px;"   v-model="PSW">
      <input type="password" :class="['inputBox']" style="top:335px;"   v-model="confirmPSW">
       <transition name="el-fade-in">
      <i v-if="isConfirmPSW===1" style="position:absolute;left:510px;top:250px;color:#67C23A;" class="el-icon-check"></i>
       </transition>
      <p v-if="isConfirmPSW===0" style="position:absolute;left:467px;top:328px;font-family:Microsoft YaHei;color:red;" v-text="confirmPSWFail"></p>
      <input :class="['inputBox']" style="top:400px;" v-model="email">
      <input :class="['inputBox']" style="top:460px;"  v-model="telephone">
      <input :class="['inputBox']" style="top:520px;" v-model="wechat">
      </div>
      <el-button type="danger" style="position:absolute;top:580px;left:365px;" @click="joinUS">加入我们</el-button>
    </div>
  </div>
</template>
<script>
// <p :class="['joinUs']" >
export default {
  name: 'Register',
  data () {
    return {
      backgroundDiv: {
        backgroundImage: 'url(' + require('../assets/images/register.jpg') + ')',
        backgroundRepeat: 'no-repeat',
        backgroundSize: '100%,100%'
      },
      account: '',
      PSW: '',
      confirmPSW: '',
      email: '',
      telephone: '',
      wechat: '',
      accountValidation: '账户已存在！',
      isAccountAvailable: 2,
      isConfirmPSW: 0,
      confirmPSWFail: '两次输入密码不同！'
    }
  },
  methods: {
    joinUS: function () {
      console.log('账号：' + this.$data.account + ' 密码：' + this.$data.psw)
      this.isAccountAvailable = 1
    }
  },
  watch: {
    confirmPSW: function (oldConfirmPSW, newConfirmPSW) {
      if (newConfirmPSW !== this.PSW) {
        this.isConfirmPSW = 0
      } else {
        this.isConfirmPSW = 1
      }
    }
  }
}
</script>

<style>
.registerBackground{
    position: absolute;
    width: 100%;
    height: 1339px;
    top:60px;
    left:0px;
}
.registerArea{
    position: absolute;
    width: 615px;
    height: 755px;
    left: 412px;
    top: 310px;

    background: #FFFFFF;
    border: 1px solid #FFFFFF;
    box-sizing: border-box;
    border-radius: 28px;
}
.registerTitle{
    position: relative;
    width: 343px;
    height: 74.36px;
    left: 35%;
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
  left: 0px;
  top: 70px;
  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: normal;
  font-size: 18px;
  line-height: 60px;
  align-items: center;
  text-align: center;

  color: #000000;
}
.inputBox{
  position: absolute;
  width: 310px;
  height: 39px;
  left: 150px;
  background: #FFFFFF;
  border: 1px solid #9C9A9A;
  box-sizing: border-box;
}
.submit{
    position: relative;
    width: 182px;
    height: 48px;
    left: 150px;
    top: -150px;

    background: #DA4646;
    box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
    border-radius: 4px;
}
.joinUs{
    position: relative;
    width: 146px;
    height: 36px;
    top:-150px;
    left:400px;
    font-family: Microsoft YaHei;
    font-style: normal;
    font-weight: bold;
    font-size: 24px;
    line-height: 32px;
    display: flex;
    align-items: center;
    text-align: center;

    background-color: #DA4646;;
}
</style>

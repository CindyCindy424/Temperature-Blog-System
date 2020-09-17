<template>
    <div class="registerBackground" :style="backgroundDiv" :load="this.renderRegister()">
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
      <input @blur="isAccountRegister()" :class="['inputBox']" style="top:210px"  v-model="account">
      <p v-if="isAccountAvailable===0" style="position:absolute;left:500px;top:200px;font-family:Microsoft YaHei;color:red;" v-text="accountValidation"></p>
      <p v-else-if="isAccountAvailable===2" style="position:absolute;left:510px;top:205px;" v-loading="isAccountAvailable===2"></p>
      <i v-if="isAccountAvailable===1" style="position:absolute;left:500px;top:215px;color:#67C23A;height:100px;" class="el-icon-check"></i>
      <input type="password" :class="['inputBox']" style="top:270px;"   v-model="PSW">
      <input type="password" @blur="checkPassword()" :class="['inputBox']" style="top:335px;"   v-model="confirmPSW">
       <transition name="el-fade-in">
      <i v-if="isConfirmPSW===1" style="position:absolute;left:500px;top:345px;color:#67C23A;" class="el-icon-check"></i>
       </transition>
      <p v-if="isConfirmPSW===0" style="position:absolute;left:500px;top:328px;font-family:Microsoft YaHei;color:red;" v-text="confirmPSWFail"></p>
      <input :class="['inputBox']" style="top:395px;" v-model="email">
      <input :class="['inputBox']" style="top:460px;"  v-model="telephone">
      <input :class="['inputBox']" style="top:520px;" v-model="wechat">
      </div>
      <el-button type="danger" style="position:absolute;top:580px;left:385px;" @click="joinUS">加入我们</el-button>
      <el-alert title="上述信息有误，请修改后再试" type="error" style="position:absolute;left:80px;top:580px;width:300px;height:44px;" v-if='this.isRegisterSuc===0'>
      </el-alert>
      <el-alert title="注册成功，正在跳转..." type="success" style="position:absolute;left:80px;top:580px;width:300px;height:44px;" v-if='this.isRegisterSuc===1' show-icon>
      </el-alert>
    </div>
    <footer style="position:absolute;bottom:40px;left:40%;" :class="['footer']">
    <p> Copyright ©2020 Temperature team, All Rights Reserved.</p>
  </footer>
  </div>
</template>
<script>
// <p :class="['joinUs']" >
export default {
  name: 'Register',
  data () {
    return {
      backgroundDiv: {
        backgroundImage: 'url(' + require('../assets/images/register2.jpg') + ')',
        backgroundRepeat: 'no-repeat',
        backgroundSize: '100%'
      },
      account: '',
      PSW: '',
      confirmPSW: '',
      email: '',
      telephone: '',
      wechat: '',
      accountValidation: '账户已存在！',
      isAccountAvailable: -1,
      isConfirmPSW: -1,
      confirmPSWFail: '两次密码不同！',
      isRegisterSuc: -1
    }
  },
  methods: {
    joinUS: function () {
      if (this.isAccountAvailable === 1 && this.isConfirmPSW === 1) {
        var xhr = new XMLHttpRequest()
        xhr.onreadystatechange = () => {
          if (xhr.readyState === 4 && xhr.status === 200) {
            var response = xhr.responseText
            var isRegisterSuccess = JSON.parse(response)

            if (isRegisterSuccess.registerFlag === 1) {
              this.isRegisterSuc = 1
              setTimeout(() => { this.$router.push({path: '/login'}) }, 3000)
            } else {
              this.isRegisterSuc = 0
            }
          }
        }
        var headerToken = document.cookie.split(';')[0].split('=')[1]
        console.log(headerToken)
        xhr.open('POST', 'http://139.224.255.43:7779/Account/register?nick_name=' + this.account + '&password=' + this.PSW + '&email=' + this.email + '&tel=' + this.telephone + '&wechat=' + this.wechat)
        xhr.setRequestHeader('Authorization', 'Bearer ' + headerToken)
        xhr.send()
      } else {
        this.isRegisterSuc = 0
      }
    },

    renderRegister: function () {
      this.$emit('turnToRegister')
    },
    isAccountRegister: function () {
      this.isAccountAvailable = 2
      var xhr = new XMLHttpRequest()
      xhr.onreadystatechange = () => {
        if (xhr.readyState === 4 && xhr.status === 200) {
          var response = xhr.responseText
          var returnModel = JSON.parse(response)
          if (returnModel.result === 'False') {
            this.isAccountAvailable = 0
          } else {
            this.isAccountAvailable = 1
          }
        }
      }
      xhr.open('POST', 'http://139.224.255.43:7779/Account/nameCheck?username=' + this.account)
      xhr.send()
    },
    checkPassword: function () {
      if (this.PSW === this.confirmPSW) {
        this.isConfirmPSW = 1
      } else {
        this.isConfirmPSW = 0
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
    top:0px;
    left:0px;
}
.registerArea{
    position: absolute;
    width: 615px;
    height: 755px;
    left: 38%;
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
  left: 20px;
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
  left: 180px;
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
.footer{
  font-family: Microsoft YaHei;
font-style: normal;
font-weight: normal;
font-size: 18px;
line-height: 24px;
display: flex;
align-items: center;
text-align: center;

color: #999494;
}
</style>

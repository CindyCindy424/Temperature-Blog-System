<template>
    <div>
        <img :class="['mySpaceBackground']" src="../assets/images/banner.png">
        <el-card class="box-card" :class="['sideBar1']" style="width:200px;" v-on:click="switchToAccoutInfo()">
            <p>账号信息</p>
        </el-card>
        <el-card class="box-card" :class="['sideBar2']" style="width:200px;" v-on:click="switchToStar()">
            <p>我的关注</p>
        </el-card>
        <el-card class="box-card" :class="['sideBar3']" style="width:200px;" v-on:click="switchToFans()">
            <p>我的粉丝</p>
        </el-card>
        <el-card class="box-card" :class="['sideBar4']" style="width:200px;">
        </el-card>
        <el-card class="box-card" :class="['MySpacetitle']">
            <p>{{mySpaceTitle}}</p>
        </el-card>
        <el-card class="box-card" :class="['MySpaceContent']">
            <div>
                <AccountInfo v-if="toDisplay==1"></AccountInfo>
                <myStar v-if="toDisplay==2"></myStar>
                <myFans v-if="toDisplay==3"></myFans>
            </div>
        </el-card>
    </div>
</template>
<script>
import Vue from 'vue'
export default {
  name: 'mySpace',
  data: function () {
    return {
      mySpaceTitle: '个人空间管理>账号信息',
      toDisplay: 1
    }
  },
  methods: {
    switchToAccountInfo: function () {
      this.mySpaceTitle = '个人空间管理>账号信息'
      this.toDisplay = 1
      console.log('账号信息' + this.toDisplay)
    },
    switchToStar: function () {
      this.mySpaceTitle = '个人空间管理>我的关注'
      this.toDisplay = 2
      console.log('我的关注' + this.toDisplay)
    },
    switchToFans: function () {
      this.mySpaceTitle = '个人空间管理>我的粉丝'
      this.toDisplay = 3
      console.log('我的粉丝' + this.toDisplay)
    }
  }
}
Vue.component('AccountInfo', {
  data: function () {
    return {
      account: 'haha',
      psw: '',
      isEditable: false,
      input: '',
      input2: '',
      input3: '',
      isPSWChange: false,
      isChangePSWAvailable: false,
      changeBasicInfo: false,
      telephone: 'asd',
      email: 'asdf',
      location: 'asdf',
      wechat: 'asd',
      birthday: '11'
    }
  },
  template: `
   <div>
      <el-avatar src="https://cube.elemecdn.com/0/88/03b0d39583f48206768a7534e55bcpng.png" :size="150" :class="['avator']">
      </el-avatar>
      <p style="position:absolute;left:320px;top:40px;" :class="['word']">用户名：</p>
      <p v-text="account"  v-if="isEditable==false" style="position:absolute;left:400px;top:35px;font-size:25px;"></p>
      <el-input size="mini" id="modifyAccount" v-model="input" placeholder="请输入新用户名" v-if="isEditable==true" style="width:200px;position:absolute;left:400px;top:60px;">
      </el-input>
      <el-button size="mini" type="success" style="position:absolute;left:620px;top:60px;" v-on:click="changeAccount()" v-if="isEditable==true">确认</el-button>
      <el-button type="primary" icon="el-icon-edit" size="small" circle v-show="isEditable==false" v-on:click="editName()" style="position:absolute;left:470px;top:59px;"></el-button>
      <p style="position:absolute;left:320px;top:80px;" :class="['word']">密码：</p>
      <el-button type="warning" v-show="isPSWChange==false" style="position:absolute;left:380px;top:100px;" size="mini" v-on:click="editPassword()">点击修改密码</el-button>
      <el-input type="password" size="mini" v-model="input2" placeholder="请输入原密码" v-if="isPSWChange==true" style="width:150px;position:absolute;left:380px;top:100px;"></el-input>
      <el-button size="mini" type="success" style="position:absolute;left:500px;top:100px;" v-on:click="validateOriginPSW()" v-if="isPSWChange==true">确认</el-button>
      <el-input type="password" size="mini" v-model="input3" placeholder="请输入新密码" v-if="isChangePSWAvailable==true" style="width:150px;position:absolute;left:380px;top:100px;"></el-input>
      <el-button size="mini" type="success" style="position:absolute;left:500px;top:100px;" v-on:click="changePSW()" v-if="isChangePSWAvailable==true">确认</el-button>
      <p style="position:absolute;left:320px;top:120px;" :class="['word']">编辑个人信息</p>
      <el-switch v-model="changeBasicInfo" active-color="#13ce66" inactive-color="#ff4949" style="position:absolute;left:450px;top:143px;"></el-switch>
      <p style="position:absolute;left:320px;top:160px;" :class="['word']">所在地：</p>
      <p style="position:absolute;left:320px;top:200px;" :class="['word']">生日：</p>
      <p style="position:absolute;left:320px;top:240px;" :class="['word']">邮箱：</p>
      <p style="position:absolute;left:320px;top:280px;" :class="['word']">手机号：</p>
      <p style="position:absolute;left:320px;top:320px;" :class="['word']">微信号：</p>
      <el-input size="mini" v-model="location" style="width:150px;position:absolute;left:400px;top:180px;" :disabled="!changeBasicInfo"></el-input>
      <el-input size="mini" v-model="birthday" style="width:150px;position:absolute;left:400px;top:220px;" :disabled="!changeBasicInfo"></el-input>
      <el-input size="mini" v-model="email" style="width:150px;position:absolute;left:400px;top:260px;" :disabled="!changeBasicInfo"></el-input>
      <el-input size="mini" v-model="telephone" style="width:150px;position:absolute;left:400px;top:300px;" :disabled="!changeBasicInfo"></el-input>
      <el-input size="mini" v-model="wechat" style="width:150px;position:absolute;left:400px;top:340px;" :disabled="!changeBasicInfo"></el-input>
    </div>
  `,
  methods: {
    editName: function () {
      this.isEditable = true
    },
    changeAccount: function () {
      this.account = this.input
      console.log('after modify' + this.account)
    },
    editPassword: function () {
      this.isPSWChange = true
    },
    validateOriginPSW: function () {
      this.isPSWChange = false
      this.isChangePSWAvailable = true
    },
    changePSW: function () {
      this.psw = this.input3
      this.isChangePSWAvailable = false

      console.log(this.psw)
    }
  }
})
Vue.component('MyStar', {
  data: function () {
    return {}
  },
  template: '<div><p>asdfasdf</p></div>'
})
Vue.component('MyFans', {
  data: function () {
    return {}
  },
  template: '<div><p>asdfasdf</p></div>'
})
</script>

<style>
.mySpaceBackground{
    position: absolute;
    width: 100%;
    height: 352px;
    left: 0px;
    top: 60px;
}
.MySpaceContent{
    position: absolute;
    width: 1015px;
    height: 600px;
    left: 305px;
    top: 515px;
}
.word{
    font-family: Microsoft YaHei;
    font-size:20px;
}
.MySpacetitle{
    position: absolute;
    width: 1015px;
    height: 60px;
    left: 305px;
    top: 450px;

    background: #F9F8F8;
    box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
    font-family: Microsoft YaHei;
    font-style: normal;
    font-weight: normal;
    text-align: center;
    display: flex;
    align-items: center;
    letter-spacing: 0.75px;
    text-transform: uppercase;
}
.sideBar1{
    position: absolute;
    left:100px;
    top: 450px;

    font-family: Microsoft YaHei;
    font-style: normal;
    font-weight: normal;
    height: 60px;
    /* identical to box height */
    text-align: center;
    display: flex;
    align-items: center;
    letter-spacing: 0.75px;
    text-transform: uppercase;

    color: #B23535;
}
.sideBar2{
    position: absolute;
    left:100px;
    top: 515px;

    font-family: Microsoft YaHei;
    font-style: normal;
    font-weight: normal;
    height: 60px;
    /* identical to box height */

    display: flex;
    align-items: center;
    letter-spacing: 0.75px;
    text-transform: uppercase;

    color: #B23535;
}
.sideBar3{
    position: absolute;
    left:100px;
    top: 580px;

    font-family: Microsoft YaHei;
    font-style: normal;
    font-weight: normal;
    height: 60px;
    /* identical to box height */

    display: flex;
    align-items: center;
    letter-spacing: 0.75px;
    text-transform: uppercase;

    color: #B23535;
}
.sideBar4{
    position: absolute;
    left:100px;
    top: 650px;

    font-family: Microsoft YaHei;
    font-style: normal;
    font-weight: normal;
    height: 465px;
    /* identical to box height */

    display: flex;
    align-items: center;
    letter-spacing: 0.75px;
    text-transform: uppercase;

    color: #B23535;
}
.avator{
    position: absolute;
    left: 100px;
    top: 50px;
}
.text {
    font-size: 14px;
  }

  .item {
    padding: 18px 0;
  }

  .box-card {
    width: 1000px;
  }
</style>

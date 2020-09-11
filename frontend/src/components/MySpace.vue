<template>
    <div :load='this.test()' style="background-color:#908D8D;">
        <img :class="['mySpaceBackground']" src="../assets/images/banner.png">
        <el-button :class="['sideBar1']" style="width:280px;" v-on:click="switchToAccountInfo()">
          <p>账号信息</p>
        </el-button>
        <el-button :class="['sideBar2']" style="width:280px;" v-on:click="switchToStar()">
          <p>我的关注</p>
        </el-button>
        <el-button :class="['sideBar3']" style="width:280px;" v-on:click="switchToFans()">
          <p>我的粉丝</p>
        </el-button>
        <el-card class="box-card" :class="['sideBar4']" style="width:280px;">
        </el-card>
        <el-card class="box-card" :class="['MySpacetitle']" style="width:1000px;">
            <p>{{mySpaceTitle}}</p>
        </el-card>
        <el-card class="box-card" :class="['MySpaceContent']" style="width:1000px;">
            <div>
                <AccountInfo v-if="toDisplay==1" @changeAccount="changeAccount()"></AccountInfo>
                <myStar v-if="toDisplay==2" ></myStar>
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
    changeAccount: function () {
      this.$emit('changeAccount')
    },
    switchToAccountInfo: function () {
      this.mySpaceTitle = '个人空间管理>账号信息'
      this.toDisplay = 1
    },
    switchToStar: function () {
      this.mySpaceTitle = '个人空间管理>我的关注'
      this.toDisplay = 2
    },
    switchToFans: function () {
      this.mySpaceTitle = '个人空间管理>我的粉丝'
      this.toDisplay = 3
    },
    test: function () {
      var xhr = new XMLHttpRequest()
      xhr.onreadystatechange = () => {
        if (xhr.readyState === 4 && xhr.status === 200) {
          var response = xhr.responseText
          console.log(response)
        }
      }
      var headerToken = document.cookie.split(';')[0].split('=')[1]
      xhr.open('POST', 'http://139.224.255.43:7779/Account/getAvatrResource?nick_name=aaa')
      xhr.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      xhr.send()
    }
  }
}
Vue.component('AccountInfo', {
  data: function () {
    return {
      psw: '',
      isEditable: false,
      input: '',
      input2: '',
      input3: '',
      isPSWChange: false,
      isChangePSWAvailable: false,
      changeBasicInfo: false,
      returnInfo: '',
      nickName: '',
      newNickName: '',
      Location: '',
      Telephone: '',
      Email: '',
      Birthday: '',
      Wechat: '',
      Gender: '',
      account: '',
      fileList: [],
      avator: '',
      modifySubmit: false,
      newLocation: '',
      newTelephone: '',
      newEmail: '',
      newBirthday: '',
      newWechat: '',
      newGender: '',
      isOriginPassword: false,
      originPasswordWrong: '原密码错误请重新输入'
    }
  },
  template: `
   <div :load="this.getAllInfo()">
      <el-avatar :src="this.avator" :size="150" :class="['avator']">
      </el-avatar>
      <el-upload
        class="upload-demo"
        ref="upload"
        :file-list="fileList"
        :auto-upload="false"
        list-type="text"
        :limit="1"
        style="position:absolute;left:60px;top:240px;width:300px;">
        <el-button slot="trigger" size="small" type="primary">修改头像</el-button>
        <el-button style="margin-left: 10px;" size="small" type="success" @click="submitUpload">确定</el-button>
      </el-upload>
      <p style="position:absolute;left:400px;top:40px;" :class="['word']">用户名：</p>
      <p v-text="this.nickName"  v-if="isEditable==false" style="position:absolute;left:470px;top:35px;font-size:25px;"></p>
      <el-input size="mini" id="modifyAccount" v-model="input" placeholder="请输入新用户名" v-if="isEditable==true" style="width:200px;position:absolute;left:470px;top:60px;">
      </el-input>
      <el-button size="mini" type="success" style="position:absolute;left:670px;top:60px;" v-on:click="changeAccount()" v-if="isEditable==true">确认</el-button>
      <el-button type="primary" icon="el-icon-edit" size="small" circle v-show="isEditable==false" v-on:click="editName()" style="position:absolute;left:590px;top:59px;"></el-button>
      <p style="position:absolute;left:400px;top:80px;" :class="['word']">密码：</p>
      <el-button type="warning" v-show="isPSWChange==false" style="position:absolute;left:460px;top:100px;" size="mini" v-on:click="editPassword()">点击修改密码</el-button>
      <el-input type="password" size="mini" v-model="input2"  placeholder="请输入原密码" v-if="isPSWChange==true" style="width:150px;position:absolute;left:460px;top:100px;"></el-input>
      <el-button size="mini" type="success" style="position:absolute;left:580px;top:100px;" v-on:click="validateOriginPSW()" v-if="isPSWChange==true">确认</el-button>
      <el-button size="mini" type="warning" style="position:absolute;left:630px;top:100px;" v-on:click="giveupModifyPassword()" v-if="isPSWChange==true||isChangePSWAvailable==true">放弃</el-button>
      <p v-if="isOriginPassword===true" style="position:absolute;left:720px;top:90px;font-family:Microsoft YaHei;color:red;" v-text="originPasswordWrong"></p>
      <el-input type="password" size="mini" v-model="input3" placeholder="请输入新密码" v-if="isChangePSWAvailable==true" style="width:150px;position:absolute;left:460px;top:100px;"></el-input>
      <el-button size="mini" type="success" style="position:absolute;left:580px;top:100px;" v-on:click="changePSW()" v-if="isChangePSWAvailable==true">确认</el-button>
      <p style="position:absolute;left:400px;top:120px;" :class="['word']">编辑个人信息</p>
      <el-switch v-model="changeBasicInfo" active-color="#13ce66" inactive-color="#ff4949" style="position:absolute;left:530px;top:143px;"></el-switch>
      <p style="position:absolute;left:400px;top:160px;" :class="['word']">所在地：</p>
      <p style="position:absolute;left:400px;top:200px;" :class="['word']">生日：</p>
      <p style="position:absolute;left:400px;top:240px;" :class="['word']">邮箱：</p>
      <p style="position:absolute;left:400px;top:280px;" :class="['word']">手机号：</p>
      <p style="position:absolute;left:400px;top:320px;" :class="['word']">微信号：</p>
      <p style="position:absolute;left:400px;top:360px;" :class="['word']">性别：</p>
      <p v-text="this.Location"  v-if="!changeBasicInfo" style="position:absolute;left:480px;top:160px;font-size:20px;"></p>
      <p v-text="this.Birthday"  v-if="!changeBasicInfo" style="position:absolute;left:480px;top:200px;font-size:20px;"></p>
      <p v-text="this.Email"  v-if="!changeBasicInfo" style="position:absolute;left:480px;top:240px;font-size:20px;"></p>
      <p v-text="this.Telephone"  v-if="!changeBasicInfo" style="position:absolute;left:480px;top:280px;font-size:20px;"></p>
      <p v-text="this.Wechat"  v-if="!changeBasicInfo" style="position:absolute;left:480px;top:320px;font-size:20px;"></p>
      <p v-text="this.Gender"  v-if="!changeBasicInfo" style="position:absolute;left:480px;top:360px;font-size:20px;"></p>
      <p style="position:absolute;left:400px;top:400px;font-size:10px;color:grey;" v-if="changeBasicInfo">*无修改则不填写新数据</p>
      <el-input  :placeholder="this.Location" type="text" v-if="changeBasicInfo" size="mini" id="inputBoxLocation" v-model="newLocation" style="width:180px;position:absolute;left:480px;top:180px;"  clearable></el-input>
      <el-date-picker v-if="changeBasicInfo" v-model="newBirthday" type="date"  placeholder="选择日期" style="position:absolute;left:480px;top:220px;font-size:20px;width:180px;" size="small" value-format="yyyy-MM-dd"></el-date-picker>
      <el-input  :placeholder="this.Email" type="text" v-if="changeBasicInfo" size="mini" id="inputBoxEmail" v-model="newEmail" style="width:180px;position:absolute;left:480px;top:260px;"  clearable></el-input>
      <el-input  :placeholder="this.Telephone" type="text" v-if="changeBasicInfo" size="mini" id="inputBoxTelephone" v-model="newTelephone" style="width:180px;position:absolute;left:480px;top:300px;" clearable></el-input>
      <el-input  :placeholder="this.Wechat" type="text" v-if="changeBasicInfo" size="mini" id="inputBoxWechat"  v-model="newWechat" style="width:180px;position:absolute;left:480px;top:340px;" clearable ></el-input>
      <el-input  :placeholder="this.Gender" type="text" v-if="changeBasicInfo" size="mini" id="inputBoxWechat"  v-model="newGender" style="width:180px;position:absolute;left:480px;top:380px;" clearable ></el-input>
      <el-button type="success" @click="submitNewInfo()" v-if="changeBasicInfo==true" :loading="modifySubmit" style="height:40px;width:120px;position:absolute;left:510px;top:440px;">提交更新</el-button>
    </div>
  `,
  methods: {
    submitNewInfo: function () {
      this.modifySubmit = true
      var xhr = new XMLHttpRequest()
      xhr.onreadystatechange = () => {
        var response = xhr.responseText
        console.log(response)
        setTimeout(() => {
          this.modifySubmit = false
          this.changeBasicInfo = false
        }, 3000)
      }
      if (this.newLocation === '') {
        this.newLocation = this.Location
      }
      if (this.newTelephone === '') {
        this.newTelephone = this.Telephone
      }
      if (this.newEmail === '') {
        this.newEmail = this.Email
      }
      if (this.newBirthday === '') {
        this.newBirthday = this.Birthday
      }
      if (this.newWechat === '') {
        this.newWechat = this.Wechat
      }
      if (this.newGender === '') {
        this.newGender = this.Gender
      }
      var headerToken = document.cookie.split(';')[0].split('=')[1]
      xhr.open('POST', 'http://139.224.255.43:7779/Account/personalInfo?nick_name=' + this.nickName + '&gender=' + this.newGender + '&location=' + this.newLocation + '&email=' + this.newEmail + '&tel=' + this.newTelephone + '&wechat=' + this.newWechat)
      xhr.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      xhr.send()
    },
    giveupModifyPassword: function () {
      this.isPSWChange = false
      this.isChangePSWAvailable = false
    },
    submitUpload: function () {
      var xhr = new XMLHttpRequest()
      xhr.onreadystatechange = () => {
      }
      var headerToken = document.cookie.split(';')[0].split('=')[1]
      xhr.open('POST', 'http://139.224.255.43:7779/Account/Login?nick_name=' + this.account + '&password=' + this.password)
      xhr.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      xhr.send()
    },
    handleExceed (files, fileList) {
      this.$message.warning(`当前限制选择 3 个文件，本次选择了 ${files.length} 个文件，共选择了 ${files.length + fileList.length} 个文件`)
    },
    changeLocation: function (e) {
      this.Location = e
    },
    editName: function () {
      this.isEditable = true
    },
    changeAccount: function () {
      this.isEditable = false
      var xhr = new XMLHttpRequest()
      xhr.onreadystatechange = () => {
        if (xhr.readyState === 4 && xhr.status === 200) {
          this.nickName = this.input
          this.$emit('changeAccount')
        }
      }
      var headerToken = document.cookie.split(';')[0].split('=')[1]
      xhr.open('POST', 'http://139.224.255.43:7779/Account/nameModify?oldName=' + this.nickName + '&newName=' + this.input)
      xhr.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      xhr.send()
    },
    editPassword: function () {
      this.isPSWChange = true
    },
    validateOriginPSW: function () {
      var xhr = new XMLHttpRequest()
      xhr.onreadystatechange = () => {
        var response = xhr.responseText
        console.log(response)
        var returnModel = JSON.parse(response)
        if (returnModel.result === 'true') {
          this.isPSWChange = false
          this.isChangePSWAvailable = true
        } else {
          this.isOriginPassword = false
        }
      }
      var headerToken = document.cookie.split(';')[0].split('=')[1]
      xhr.open('POST', 'http://139.224.255.43:7779/Account/checkPW?nick_name=' + this.nickName + '&password=' + this.input2)
      xhr.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      xhr.send()
    },
    changePSW: function () {
      var xhr = new XMLHttpRequest()
      xhr.onreadystatechange = () => {
        var response = xhr.responseText
        console.log(response)
      }
      var headerToken = document.cookie.split(';')[0].split('=')[1]
      xhr.open('POST', 'http://139.224.255.43:7779/Account/passwordModify?nick_name=' + this.nickName + '&oldPW=' + this.input2 + '&newPW=' + this.input3)
      xhr.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      xhr.send()
      this.isChangePSWAvailable = false
    },
    getAllInfo: function () {
      var xhr = new XMLHttpRequest()
      xhr.onreadystatechange = () => {
        if (xhr.readyState === 4 && xhr.status === 200) {
          var response = xhr.responseText
          this.returnInfo = JSON.parse(response)
          this.Location = this.returnInfo.userInfo.location
          this.Email = this.returnInfo.userInfo.email
          this.Telephone = this.returnInfo.userInfo.tel
          this.Wechat = this.returnInfo.userInfo.wechat
          this.nickName = this.returnInfo.userInfo.nickName
          this.Birthday = this.returnInfo.userInfo.dob.slice(0, 10)
          this.Gender = this.returnInfo.userInfo.gender
        }
      }
      var headerToken = document.cookie.split(';')[0].split('=')[1]

      xhr.open('POST', 'http://139.224.255.43:7779/Account/getUserInfoByNickName?nick_name=' + this.$route.query.account)
      xhr.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      xhr.send()
      var xhr2 = new XMLHttpRequest()
      xhr2.onreadystatechange = () => {
        if (xhr2.readyState === 4 && xhr2.status === 200) {
          var response = xhr2.responseText
          this.returnInfo = JSON.parse(response)
          if (this.returnInfo.flag === 1) {
            this.avator = 'http://139.224.255.43:7779/BlogPics/Avator/' + this.returnInfo.path.split('\\')[2]
          }
        }
      }
      xhr2.open('POST', 'http://139.224.255.43:7779/Account/getAvatrResource?nick_name=' + this.$route.query.account)
      xhr2.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      xhr2.send()
    }
  }
})
Vue.component('MyStar', {
  data: function () {
    return {
      Star: []
    }
  },
  mounted () {
    this.getMyStar()
  },
  methods: {
    getMyStar: function () {
      var xhr = new XMLHttpRequest()
      xhr.onreadystatechange = () => {
        if (xhr.readyState === 4 && xhr.status === 200) {
          var response = xhr.responseText
          var returnModel = JSON.parse(response)
          if (returnModel.returnFlag === 1) {
            this.Star = returnModel.followList
            for (var i = 0; i < this.Star.length; i++) {
              this.Star[i].avator = 'http://139.224.255.43:7779/' + this.Star[i].avator.split('\\')[0] + '/' + this.Star[i].avator.split('\\')[1] + '/' + this.Star[i].avator.split('\\')[2]
            }
          }
        }
      }
      var headerToken = document.cookie.split(';')[0].split('=')[1]
      xhr.open('POST', 'http://139.224.255.43:7779/Account/getFollowListByNickName?nick_name=' + this.$route.query.account)
      xhr.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      xhr.send()
    }
  },
  template: `
    <table>
        <tr  v-for="(item,index) in Star" v-if="index%6==0&&Star[index+5]!=null">
          <td class="avatorInterval">
             <el-avatar :size="120" :src="Star[index].avator" v-if="Star[index]!=null"></el-avatar>
             <p v-text="Star[index].name"></p>
          </td>
          <td class="avatorInterval">
             <el-avatar :size="120" :src="Star[index+1].avator" v-if="Star[index+1]!=null"></el-avatar>
             <p v-text="Star[index+1].name"></p>
          </td>
          <td class="avatorInterval">
             <el-avatar :size="120" :src="Star[index+2].avator" v-if="Star[index+2]!=null"></el-avatar>
             <p v-text="Star[index+2].name"></p>
          </td>
          <td class="avatorInterval">
             <el-avatar :size="120" :src="Star[index+3].avator" v-if="Star[index+3]!=null"></el-avatar>
             <p v-text="Star[index+3].name"></p>
          </td>
          <td class="avatorInterval">
             <el-avatar :size="120" :src="Star[index+4].avator" v-if="Star[index+4]!=null"></el-avatar>
             <p v-text="Star[index+4].name"></p>
          </td>
          <td class="avatorInterval">
             <el-avatar :size="120" :src="Star[index+5].avator" v-if="Star[index+5]!=null"></el-avatar>
             <p v-text="Star[index+5].name"></p>
          </td>
        </tr>
        <tr>
        <td v-for="n in (this.Star.length%6)" class="avatorInterval">
            <el-avatar :size="120" :src="Star[Star.length-(Star.length%6)+n-1].avator" ></el-avatar>
            <p v-text="Star[Star.length-(Star.length%6)+n-1].name"></p>
        </td>
        </td>
        </tr>
    </table>
  `
})
Vue.component('MyFans', {
  data: function () {
    return {
      Star: []
    }
  },
  mounted () {
    this.getMyFans()
  },
  methods: {
    getMyFans: function () {
      console.log(this.account)
      var xhr = new XMLHttpRequest()
      xhr.onreadystatechange = () => {
        if (xhr.readyState === 4 && xhr.status === 200) {
          var response = xhr.responseText
          console.log('responst=' + response)
          var returnModel = JSON.parse(response)
          if (returnModel.returnFlag === 1) {
            this.Star = returnModel.list
            for (var i = 0; i < this.Star.length; i++) {
              this.Star[i].avator = 'http://139.224.255.43:7779/' + this.Star[i].avator.split('\\')[0] + '/' + this.Star[i].avator.split('\\')[1] + '/' + this.Star[i].avator.split('\\')[2]
            }
          }
        }
      }
      var headerToken = document.cookie.split(';')[0].split('=')[1]
      xhr.open('POST', 'http://139.224.255.43:7779/Account/getFansListByNickName?nick_name=' + this.$route.query.account)
      xhr.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      xhr.send()
    }
  },
  template: `
    <table>
        <tr  v-for="(item,index) in Star" v-if="index%6==0&&Star[index+5]!=null">
          <td class="avatorInterval">
             <el-avatar :size="120" :src="Star[index].avator" v-if="Star[index]!=null"></el-avatar>
             <p v-text="Star[index].name"></p>
          </td>
          <td class="avatorInterval">
             <el-avatar :size="120" :src="Star[index+1].avator" v-if="Star[index+1]!=null"></el-avatar>
             <p v-text="Star[index+1].name"></p>
          </td>
          <td class="avatorInterval">
             <el-avatar :size="120" :src="Star[index+2].avator" v-if="Star[index+2]!=null"></el-avatar>
             <p v-text="Star[index+2].name"></p>
          </td>
          <td class="avatorInterval">
             <el-avatar :size="120" :src="Star[index+3].avator" v-if="Star[index+3]!=null"></el-avatar>
             <p v-text="Star[index+3].name"></p>
          </td>
          <td class="avatorInterval">
             <el-avatar :size="120" :src="Star[index+4].avator" v-if="Star[index+4]!=null"></el-avatar>
             <p v-text="Star[index+4].name"></p>
          </td>
          <td class="avatorInterval">
             <el-avatar :size="120" :src="Star[index+5].avator" v-if="Star[index+5]!=null"></el-avatar>
             <p v-text="Star[index+5].name"></p>
          </td>
        </tr>
        <tr>
        <td v-for="n in (this.Star.length%6)" class="avatorInterval">
            <el-avatar :size="120" :src="Star[Star.length-(Star.length%6)+n-1].avator" ></el-avatar>
            <p v-text="Star[Star.length-(Star.length%6)+n-1].name"></p>
        </td>
        </td>
        </tr>
    </table>
  `
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
    height: 1137px;
    left: 35%;
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
    left: 35%;
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
    left:23.4%;
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
    left:23%;
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
    left:23%;
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
    left:23.4%;
    top: 650px;

    font-family: Microsoft YaHei;
    font-style: normal;
    font-weight: normal;
    height: 1000px;
    /* identical to box height */

    display: flex;
    align-items: center;
    letter-spacing: 0.75px;
    text-transform: uppercase;

    color: #B23535;
}
.avator{
    position: absolute;
    left: 150px;
    top: 50px;
}
.text {
    font-size: 14px;
  }

  .item {
    padding: 18px 0;
  }

.avatorInterval{
  width:150px;
}
</style>

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
            <p style="color: #756F6F;font-family: Microsoft YaHei;font-style: normal;font-size:24px;line-height: 32px;font-weight: bold;">{{mySpaceTitle}}</p>
        </el-card>
        <div>
        <el-card class="box-card" :class="['MySpaceContent']" style="width:1000px;">
            <div>
                <AccountInfo v-if="toDisplay==1" @changeAccount="changeAccount()"></AccountInfo>
                <myStar v-if="toDisplay==2" ></myStar>
                <myFans v-if="toDisplay==3"></myFans>
            </div>
        </el-card>
        </div>
        <div>
          <footer style="position:absolute;top:1400px;;left:40%;" :class="['footer']">
            <p> Copyright ©2020 Temperature team, All Rights Reserved.</p>
            <p><br/>&nbsp;</p>
          </footer>
        </div>
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
      userID: '',
      isOriginPassword: true,
      originPasswordWrong: '原密码错误请重新输入',
      year: '',
      month: '',
      day: '',
      dialogVisible: false
    }
  },
  computed: {
    action: function () { return 'http://139.224.255.43:7779/Account/createAvatorByName?nick_name=' + this.$route.query.account }
  },
  template: `
   <div :load="this.getAllInfo()">
      <el-avatar :src="this.avator" :size="150" :class="['avator']">
      </el-avatar>
      <el-upload
        class="upload-demo"
        ref="upload"
        :file-list="this.fileList"
        :auto-upload="false"
        :action="this.action"
        list-type="text"
        :limit="1"
        :headers=this.headers
        style="position:absolute;left:20px;top:240px;width:300px;">
        <el-button slot="trigger" size="small" type="primary">修改头像</el-button>
        <el-button style="margin-left: 10px;" size="small" type="success" @click="submitUpload">确定</el-button>
      </el-upload>
      <p style="position:absolute;left:350px;top:50px;" :class="['word']">用户名：</p>
      
      <el-input size="mini" id="modifyAccount" v-model="input" placeholder="请输入新用户名" v-if="isEditable==true" style="width:200px;position:absolute;left:530px;top:60px;">
      </el-input>
      <el-button size="mini" type="success" style="position:absolute;left:720px;top:60px;" v-on:click="changeAccount()" v-if="isEditable==true">确认</el-button>
      <p v-text="this.nickName"  v-if="isEditable==false" style="position:absolute;left:435px;top:42px;font-size:25px;"></p>
      <el-button type="primary" icon="el-icon-edit" size="small" circle v-show="isEditable==false" v-on:click="editName()" style="position:relative;left:40px;top:43px;"></el-button>
      <p style="position:absolute;left:350px;top:150px;" :class="['word']">密码：</p>
      <p style="position:absolute;left:350px;top:100px;" :class="['word']">用户ID:</p>
      <p style="position:absolute;left:450px;top:100px;" :class="['word']" v-text="this.userID"></p>
      <hr align=center width=960 color=#969292 SIZE=1 style="position:absolute;top:320px;"></hr>
      <el-button v-show="isPSWChange==false" style="position:absolute;left:420px;top:170px;" size="mini" v-on:click="editPassword()">点击修改密码</el-button>
      <el-input type="password" size="small" v-model="input2"  placeholder="请输入原密码" v-if="isPSWChange==true" style="width:150px;position:absolute;left:420px;top:170px;"></el-input>
      <el-button size="mini" style="position:absolute;left:540px;top:170px;height:32px;" v-on:click="validateOriginPSW()" v-if="isPSWChange==true">确认</el-button>
      <el-button size="mini" style="position:absolute;left:590px;top:170px;height:32px;" v-on:click="giveupModifyPassword()" v-if="isPSWChange==true||isChangePSWAvailable==true">放弃</el-button>
      <p v-if="isOriginPassword===false" style="position:absolute;left:670px;top:160px;font-family:Microsoft YaHei;color:red;" v-text="originPasswordWrong"></p>
      <el-input type="password" size="small" v-model="input3" placeholder="请输入新密码" v-if="isChangePSWAvailable==true" style="width:150px;position:absolute;left:420px;top:170px;"></el-input>
      <el-dialog
        title="消息"
        :visible.sync="dialogVisible"
        width="30%"
        style="top:30%;"
        :before-close="handleClose">
        <span>修改密码成功</span>
        <span slot="footer" class="dialog-footer">
          <el-button type="primary" @click="dialogVisible = false">确 定</el-button>
        </span>
      </el-dialog>
      <p :class="['word']" style="position:absolute;left:350px;top:200px;">公告:</p>
      <p :class="['word']" style="position:absolute;left:430px;top:200px;" v-text="this.userAnnouncement"></p>
      <el-button size="mini" style="position:absolute;left:540px;top:170px;" v-on:click="changePSW()" v-if="isChangePSWAvailable==true">确认</el-button>
      <p style="position:absolute;left:810px;top:317px;" :class="['word']">修改资料</p>
      <el-switch v-model="changeBasicInfo" active-color="#13ce66" inactive-color="#ff4949" style="position:absolute;left:900px;top:340px;"></el-switch>
      <p style="position:absolute;left:300px;top:450px;" :class="['word']">所在地：</p>
      <p style="position:absolute;left:300px;top:500px;" :class="['word']">生日：</p>
      <p style="position:absolute;left:300px;top:550px;" :class="['word']">邮箱：</p>
      <p style="position:absolute;left:300px;top:600px;" :class="['word']">手机号：</p>
      <p style="position:absolute;left:300px;top:650px;" :class="['word']">微信号：</p>
      <p style="position:absolute;left:300px;top:400px;" :class="['word']">性别：</p>
      <p v-text="this.Location"  v-if="!changeBasicInfo" style="position:absolute;left:480px;top:450px;font-size:20px;"></p>
      <p v-text="this.Birthday"  v-if="!changeBasicInfo" style="position:absolute;left:480px;top:500px;font-size:20px;"></p>
      <p v-text="this.Email"  v-if="!changeBasicInfo" style="position:absolute;left:480px;top:550px;font-size:20px;"></p>
      <p v-text="this.Telephone"  v-if="!changeBasicInfo" style="position:absolute;left:480px;top:600px;font-size:20px;"></p>
      <p v-text="this.Wechat"  v-if="!changeBasicInfo" style="position:absolute;left:480px;top:650px;font-size:20px;"></p>
      <p v-text="this.Gender"  v-if="!changeBasicInfo" style="position:absolute;left:480px;top:400px;font-size:20px;"></p>
      <p style="position:absolute;left:600px;top:695px;font-size:10px;color:grey;" v-if="changeBasicInfo">*无修改则不填写新数据</p>
      <el-input  :placeholder="this.Location" type="text" v-if="changeBasicInfo" size="small" id="inputBoxLocation" v-model="newLocation" style="width:280px;position:absolute;left:480px;top:470px;"  clearable></el-input>
      <el-date-picker v-if="changeBasicInfo" v-model="newBirthday" type="date"  placeholder="选择日期" style="position:absolute;left:480px;top:520px;font-size:20px;width:280px;" size="small" value-format="yyyy-MM-dd"></el-date-picker>
      <el-input  :placeholder="this.Email" type="text" v-if="changeBasicInfo" size="small" id="inputBoxEmail" v-model="newEmail" style="width:280px;position:absolute;left:480px;top:570px;"  clearable></el-input>
      <el-input  :placeholder="this.Telephone" type="text" v-if="changeBasicInfo" size="small" id="inputBoxTelephone" v-model="newTelephone" style="width:280px;position:absolute;left:480px;top:620px;" clearable></el-input>
      <el-input  :placeholder="this.Wechat" type="text" v-if="changeBasicInfo" size="small" id="inputBoxWechat"  v-model="newWechat" style="width:280px;position:absolute;left:480px;top:670px;" clearable ></el-input>
      <el-radio label="男" v-model="newGender" v-if="changeBasicInfo" style="position:absolute;left:480px;top:420px;font-size:20px;" size="medium"></el-radio>
      <el-radio label="女" v-model="newGender" v-if="changeBasicInfo" style="position:absolute;left:600px;top:420px;font-size:20px;" size="medium"></el-radio>
      <el-button type="success" @click="submitNewInfo()" v-if="changeBasicInfo==true" :loading="modifySubmit" style="height:40px;width:120px;position:absolute;left:640px;top:740px;">提交更新</el-button>
    </div>
  `,
  methods: {
    handleClose (done) {
      this.$confirm('确认关闭？')
        .then(_ => {
          done()
        })
        .catch(_ => {})
    },
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
      } else {
        this.year = this.newBirthday.split('-')[0]
        this.month = this.newBirthday.split('-')[1]
        this.day = this.newBirthday.split('-')[2]
      }
      if (this.newWechat === '') {
        this.newWechat = this.Wechat
      }
      if (this.newGender === '') {
        this.newGender = this.Gender
      }
      var headerToken = document.cookie.split(';')[0].split('=')[1]
      xhr.open('POST', 'http://139.224.255.43:7779/Account/personalInfo?nick_name=' + this.nickName + '&gender=' + this.newGender + '&location=' + this.newLocation + '&email=' + this.newEmail + '&tel=' + this.newTelephone + '&wechat=' + this.newWechat + '&year=' + this.year + '&month=' + this.month + '&day=' + this.day)
      xhr.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      xhr.send()
    },
    giveupModifyPassword: function () {
      this.isPSWChange = false
      this.isChangePSWAvailable = false
      this.isOriginPassword = true
    },
    submitUpload: function () {
      var fileValue = document.querySelector('.el-upload .el-upload__input')
      var fd = new window.FormData()
      // 配置post请求的参数。参数名file,后面跟要传的文件，参数名fileType，值为category（看后端的具体要求）
      fd.append('uploadedPhoto', fileValue.files[0])
      var xhr = new XMLHttpRequest()
      xhr.open('POST', 'http://139.224.255.43:7779/Account/createAvatorByName?nick_name=' + this.$route.query.account, true)
      var headerToken = document.cookie.split(';')[0].split('=')[1]
      xhr.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      // url就是要发送的post请求的地址
      xhr.send(fd)
      xhr.onload = () => {
        if (xhr.status === 200) {
          this.imgurl = JSON.parse(xhr.responseText).url
          var xhr2 = new XMLHttpRequest()
          xhr2.onreadystatechange = () => {
            if (xhr2.readyState === 4 && xhr2.status === 200) {
              var response = xhr2.responseText
              this.returnInfo = JSON.parse(response)
              if (this.returnInfo.flag === 1) {
                this.avator = 'http://139.224.255.43:7779/BlogPics/Avator/' + this.returnInfo.path.split('\\')[2]
                this.fileList = []
              }
            }
          }
          xhr2.open('POST', 'http://139.224.255.43:7779/Account/getAvatrResource?nick_name=' + this.$route.query.account)
          xhr2.setRequestHeader('Authorization', 'Bearer ' + headerToken)
          xhr2.send()
        }
      }
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
        var returnModel = JSON.parse(response)
        if (returnModel.result === 'true') {
          this.isPSWChange = false
          this.isChangePSWAvailable = true
          this.isOriginPassword = true
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
        var isSuccess = JSON.parse(response)
        if (isSuccess.modifyFlag === 1) {
          this.dialogVisible = true
        }
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
          this.userID = this.returnInfo.userInfo.userId
          this.userAnnouncement = this.returnInfo.userAnnouncement
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
    height: 835px;
    left: 35%;
    top: 515px;
}
.word{
  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: normal;
  font-size: 20px;
  line-height: 24px;

  color: #000000;
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
    height: 700px;
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

.avatorInterval{
  width:150px;
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

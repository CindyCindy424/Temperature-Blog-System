<template>
    <div>
        <img :class="['mainPageBackground']" src="../assets/images/banner.png">
        <el-link type="info" :class="['divisionTitle']" @click="switchToStudy()" style="position:absolute;top:500px;left:400px;display:block;">学习</el-link>
        <p :class="['divisionTitle']" style="position:absolute;top:468px;left:520px;">|</p>
        <el-link type="info" :class="['divisionTitle']"  style="position:absolute;top:500px;left:600px;display:block;">生活</el-link>
        <p :class="['divisionTitle']" style="position:absolute;top:468px;left:720px;">|</p>
        <el-link type="info" :class="['divisionTitle']"  style="position:absolute;top:500px;left:800px;display:block;">开发</el-link>
        <p :class="['divisionTitle']" style="position:absolute;top:468px;left:920px;">|</p>
        <el-link type="info" :class="['divisionTitle']" style="position:absolute;top:500px;left:1000px;display:block;">游戏</el-link>
        <p :class="['divisionTitle']" style="position:absolute;top:468px;left:1120px;">|</p>
        <el-link type="info" :class="['divisionTitle']" style="position:absolute;top:500px;left:1200px;display:block;">娱乐</el-link>
        <p :class="['divisionTitle']" style="position:absolute;top:468px;left:1320px;">|</p>
        <el-link type="info" :class="['divisionTitle']" style="position:absolute;top:500px;left:1400px;display:block;">体育</el-link>
        <el-link type="info" :class="['divisionTitle']" style="position:absolute;top:600px;left:400px;display:block;">影视</el-link>
        <p :class="['divisionTitle']" style="position:absolute;top:568px;left:520px;">|</p>
        <el-link type="info" :class="['divisionTitle']" style="position:absolute;top:600px;left:600px;display:block;">咨询</el-link>
        <p :class="['divisionTitle']" style="position:absolute;top:568px;left:720px;">|</p>
        <el-link type="info" :class="['divisionTitle']" style="position:absolute;top:600px;left:800px;display:block;">时尚</el-link>
        <p :class="['divisionTitle']" style="position:absolute;top:568px;left:920px;">|</p>
        <el-link type="info" :class="['divisionTitle']" style="position:absolute;top:600px;left:1000px;display:block;">舞蹈</el-link>
        <p :class="['divisionTitle']" style="position:absolute;top:568px;left:1120px;">|</p>
        <el-link type="info" :class="['divisionTitle']" style="position:absolute;top:600px;left:1200px;display:block;">音乐</el-link>
        <p :class="['divisionTitle']" style="position:absolute;top:568px;left:1320px;">|</p>
        <el-link type="info" :class="['divisionTitle']" style="position:absolute;top:600px;left:1400px;display:block;">其他</el-link>
        <div :class="['recommodation']" >
            <p :class="['titleFont']" v-text='this.section1Title'></p>
            <img :src='this.section1Img' width="40" height="40" style="position:absolute;top:4px;left:10px;">
        </div>
        <el-card class="box-card" style="position:absolute;top:800px;left:400px;width:1200px;">
            <h1 v-text="this.recommodation[0].Title" style="text-align:left;font-size:28px;"></h1>
            <p v-text="this.recommodation[0].ArticleContent" :class="['recommodationIntro']"></p>
            <img src="../assets/images/艺术设计.png" width="200px" height="150px" style="position:absolute;left:630px;top:10px;">
            <el-link type="info" v-text="'分类：'+this.recommodation[0].Title" :class="['recommodationText']" style="position:absolute;top:25px;left:870px;"></el-link>
            <el-avatar :src="this.userAvator[0]" :size="50" style="position:absolute;top:120px;left:870px;"></el-avatar>
            <el-link type="info" v-text="this.userInfo[0]" :class="['recommodationText']" style="position:absolute;top:130px;left:920px;"></el-link>
            <p v-text="this.recommodation[0].ArticleLikes" :class="['recommodationText']" style="position:absolute;top:3px;left:1150px;color:#5F7D95"></p>
            <img src="../assets/images/hand.png" width="30px" height="30px" style="position:absolute;top:20px;left:1100px;">
            <p v-text="this.recommodation[0].ReadNum" :class="['recommodationText']" style="position:absolute;top:110px;left:1150px;color:#5F7D95"></p>
            <img src="../assets/images/eye.png" width="30px" height="30px" style="position:absolute;top:130px;left:1100px;">
        </el-card>
        <el-card class="box-card" style="position:absolute;top:1000px;left:400px;width:1200px;">
            <h1 v-text="this.recommodation[1].Title" style="text-align:left;font-size:28px;"></h1>
            <p v-text="this.recommodation[1].ArticleContent" :class="['recommodationIntro']"></p>
            <img src="../assets/images/艺术设计.png" width="200px" height="150px" style="position:absolute;left:630px;top:20px;">
            <el-link type="info" v-text="'分类：'+this.recommodation[1].Title" :class="['recommodationText']" style="position:absolute;top:25px;left:870px;"></el-link>
            <el-avatar :src="this.userAvator[1]" :size="50" style="position:absolute;top:120px;left:870px;"></el-avatar>
            <el-link type="info" v-text="this.userInfo[1]" :class="['recommodationText']" style="position:absolute;top:130px;left:920px;"></el-link>
            <p v-text="this.recommodation[1].ArticleLikes" :class="['recommodationText']" style="position:absolute;top:3px;left:1150px;color:#5F7D95"></p>
            <img src="../assets/images/hand.png" width="30px" height="30px" style="position:absolute;top:20px;left:1100px;">
            <p v-text="this.recommodation[1].ReadNum" :class="['recommodationText']" style="position:absolute;top:110px;left:1150px;color:#5F7D95"></p>
            <img src="../assets/images/eye.png" width="30px" height="30px" style="position:absolute;top:130px;left:1100px;">
        </el-card>
        <el-card class="box-card" style="position:absolute;top:1200px;left:400px;width:1200px;">
            <h1 v-text="this.recommodation[2].Title" style="text-align:left;font-size:28px;"></h1>
            <p v-text="this.recommodation[2].ArticleContent" :class="['recommodationIntro']"></p>
            <img src="../assets/images/艺术设计.png" width="200px" height="150px" style="position:absolute;left:630px;top:20px;">
            <el-link type="info" v-text="'分类：'+this.recommodation[2].Title" :class="['recommodationText']" style="position:absolute;top:25px;left:870px;"></el-link>
            <el-avatar :src="this.userAvator[2]" :size="50" style="position:absolute;top:120px;left:870px;"></el-avatar>
            <el-link type="info" v-text="this.userInfo[2]" :class="['recommodationText']" style="position:absolute;top:130px;left:920px;"></el-link>
            <p v-text="this.recommodation[2].ArticleLikes" :class="['recommodationText']" style="position:absolute;top:3px;left:1150px;color:#5F7D95"></p>
            <img src="../assets/images/hand.png" width="30px" height="30px" style="position:absolute;top:20px;left:1100px;">
            <p v-text="this.recommodation[2].ReadNum" :class="['recommodationText']" style="position:absolute;top:110px;left:1150px;color:#5F7D95"></p>
            <img src="../assets/images/eye.png" width="30px" height="30px" style="position:absolute;top:130px;left:1100px;">
        </el-card>
        <el-card class="box-card" style="position:absolute;top:1400px;left:400px;width:1200px;">
            <h1 v-text="this.recommodation[3].Title" style="text-align:left;font-size:28px;"></h1>
            <p v-text="this.recommodation[3].ArticleContent" :class="['recommodationIntro']"></p>
            <img src="../assets/images/艺术设计.png" width="200px" height="150px" style="position:absolute;left:630px;top:20px;">
            <el-link type="info" v-text="'分类：'+this.recommodation[3].Title" :class="['recommodationText']" style="position:absolute;top:25px;left:870px;"></el-link>
            <el-avatar :src="this.userAvator[3]" :size="50" style="position:absolute;top:120px;left:870px;"></el-avatar>
            <el-link type="info" v-text="this.userInfo[3]" :class="['recommodationText']" style="position:absolute;top:130px;left:920px;"></el-link>
            <p v-text="this.recommodation[3].ArticleLikes" :class="['recommodationText']" style="position:absolute;top:3px;left:1150px;color:#5F7D95"></p>
            <img src="../assets/images/hand.png" width="30px" height="30px" style="position:absolute;top:20px;left:1100px;">
            <p v-text="this.recommodation[3].ReadNum" :class="['recommodationText']" style="position:absolute;top:110px;left:1150px;color:#5F7D95"></p>
            <img src="../assets/images/eye.png" width="30px" height="30px" style="position:absolute;top:130px;left:1100px;">
        </el-card>
        <div :class="['topic']">
            <p :class="['titleFont']">话题速递</p>
            <img src="../assets/images/analytics.png" width="40" height="40" style="position:absolute;top:4px;left:10px;">
        </div>
        <div style="position:absolute;top:1750px;left:400px;">
        <img src="../assets/images/babe-fyrcsrv8730487.jpg" width="500" height="400" >
        <p :class="['topicFont']" style="width:300px;height:600px;position:absolute;position:absolute;top:25%;left:35%;font-size:40px;" v-text="this.topic[0].TopicTitle"></p>
        </div>
        <div style="position:absolute;top:1750px;left:920px;">
            <img src="../assets/images/艺术设计.png" width="200" height="200" >
            <p :class="['topicFont']" style="width:300px;height:600px;position:absolute;top:25%;left:35%;font-size:20px;" v-text="this.topic[1].TopicTitle"></p>
        </div>
        <div style="position:absolute;top:1970px;left:920px;">
            <img src="../assets/images/自然美景.png" width="200" height="180" >
            <p :class="['topicFont']" style="width:170px;height:600px;position:absolute;top:25%;left:35%;font-size:20px;" v-text="this.topic[2].TopicTitle"></p>
        </div>
        <div style="position:absolute;top:1750px;left:1140px;">
            <img src="../assets/images/宇宙深空.png" width="450" height="400" >
            <p :class="['topicFont']" style="width:300px;height:600px;position:absolute;top:25%;left:35%;font-size:30px;text-align:center;" v-text="this.topic[3].TopicTitle"></p>
        </div>
        <div :class="['topicArea']">
            <p :class="['topicAreaTitle']">话题区</p>
        </div>
        <el-card class="box-card"  style="position:absolute;top:700px;left:1730px;width:400px;">
            <p :class="['titleFont']" style="left:15%;font-size:30px;font-family: Microsoft YaHei;font-style: normal;font-weight: normal;color: #7B7B7B;">博文热榜</p>
            <img src="../assets/images/fire.png" width="40" height="40" style="position:absolute;top:4px;left:10px;">
            <ul class="hotTopic">
                <li>
                    <p v-text="this.recommodation[0].Title" class="textFont"></p>
                </li>
                <li>
                <p v-text="this.recommodation[1].Title" class="textFont"></p>
                </li>
                <li>
                <p v-text="this.recommodation[2].Title" class="textFont"></p>
                </li>
                <li>
                <p v-text="this.recommodation[3].Title" class="textFont"></p>
                </li>
                <li>
                <p v-text="this.recommodation[4].Title" class="textFont"></p>
                </li>
                <li>
                <p v-text="this.recommodation[5].Title" class="textFont"></p>
                </li>
                <li>
                <p v-text="this.recommodation[6].Title" class="textFont"></p>
                </li>
                <li>
                <p v-text="this.recommodation[7].Title" class="textFont"></p>
                </li>
                <li>
                <p v-text="this.recommodation[8].Title" class="textFont"></p>
                </li>
                <li>
                <p v-text="this.recommodation[9].Title" class="textFont"></p>
                </li>
            </ul>
        </el-card>
        <el-card class="box-card"  style="position:absolute;top:1600px;left:1730px;width:400px;">
            <p :class="['titleFont']" style="left:15%;font-size:30px;font-family: Microsoft YaHei;font-style: normal;font-weight: normal;color: #7B7B7B;">热门博主</p>
            <img src="../assets/images/star (2).png" width="40" height="40" style="position:absolute;top:4px;left:10px;">
            <ul class="hotTopic">
                <li>
                    <p v-text="this.recommodation[0].Title" class="textFont"></p>
                </li>
                <li>
                <p v-text="this.recommodation[1].Title" class="textFont"></p>
                </li>
                <li>
                <p v-text="this.recommodation[2].Title" class="textFont"></p>
                </li>
                <li>
                <p v-text="this.recommodation[3].Title" class="textFont"></p>
                </li>
                <li>
                <p v-text="this.recommodation[4].Title" class="textFont"></p>
                </li>
                <li>
                <p v-text="this.recommodation[5].Title" class="textFont"></p>
                </li>
                <li>
                <p v-text="this.recommodation[6].Title" class="textFont"></p>
                </li>
                <li>
                <p v-text="this.recommodation[7].Title" class="textFont"></p>
                </li>
                <li>
                <p v-text="this.recommodation[8].Title" class="textFont"></p>
                </li>
                <li>
                <p v-text="this.recommodation[9].Title" class="textFont"></p>
                </li>
            </ul>
        </el-card>
    </div>
</template>

<script>
export default {
  name: 'MainPage',
  data () {
    return {
      recommodation: [],
      topic: [],
      study: [
        {title: '数据库学习心得', intro: '数据库是一门有意义的课程.....', author: '小明', avator: 'https://cube.elemecdn.com/0/88/03b0d39583f48206768a7534e55bcpng.png', watches: '20', likes: '30'},
        {title: '数据库学习心得', intro: '数据库是一门有意义的课程.....', author: '小明', avator: 'https://cube.elemecdn.com/0/88/03b0d39583f48206768a7534e55bcpng.png', watches: '20', likes: '30'},
        {title: '数据库学习心得', intro: '数据库是一门有意义的课程.....', author: '小明', avator: 'https://cube.elemecdn.com/0/88/03b0d39583f48206768a7534e55bcpng.png', watches: '20', likes: '30'}
      ],
      userInfo: [],
      userAvator: [],
      currentArea: '',
      recommodationText: '热门推荐',
      selectedText: '博文精选',
      section1Title: '热门推荐',
      recommodationImg: '../assets/images/star.png',
      selectedImg: require('../assets/images/hand.png'),
      section1Img: require('../assets/images/star.png')

    }
  },
  methods: {
    switchToStudy: function () {
      console.log('hello')
    },
    getUserName: function () {
      var xhr = new XMLHttpRequest()
      xhr.onreadystatechange = () => {
        if (xhr.readyState === 4 && xhr.status === 200) {
          var response = xhr.responseText
          var returnModel = JSON.parse(response)
          this.userInfo[0] = returnModel.userInfo.nickName
          this.$forceUpdate()
        }
      }
      var headerToken = document.cookie.split(';')[0].split('=')[1]
      xhr.open('POST', 'http://139.224.255.43:7779/Account/getUserInfoByID?user_id=' + this.recommodation[0].UserId)
      xhr.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      xhr.send()
      var xhr1 = new XMLHttpRequest()
      xhr1.onreadystatechange = () => {
        if (xhr1.readyState === 4 && xhr1.status === 200) {
          var response = xhr1.responseText
          var returnModel = JSON.parse(response)
          this.userInfo[1] = returnModel.userInfo.nickName
          this.$forceUpdate()
        }
      }
      xhr1.open('POST', 'http://139.224.255.43:7779/Account/getUserInfoByID?user_id=' + this.recommodation[1].UserId)
      xhr1.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      xhr1.send()
      var xhr2 = new XMLHttpRequest()
      xhr2.onreadystatechange = () => {
        if (xhr2.readyState === 4 && xhr2.status === 200) {
          var response = xhr2.responseText
          var returnModel = JSON.parse(response)
          this.userInfo[2] = returnModel.userInfo.nickName
          this.$forceUpdate()
        }
      }
      xhr2.open('POST', 'http://139.224.255.43:7779/Account/getUserInfoByID?user_id=' + this.recommodation[2].UserId)
      xhr2.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      xhr2.send()
      var xhr3 = new XMLHttpRequest()
      xhr3.onreadystatechange = () => {
        if (xhr3.readyState === 4 && xhr3.status === 200) {
          var response = xhr3.responseText
          var returnModel = JSON.parse(response)
          this.userInfo[3] = returnModel.userInfo.nickName
          this.$forceUpdate()
          this.getUserAvator()
        }
      }
      xhr3.open('POST', 'http://139.224.255.43:7779/Account/getUserInfoByID?user_id=' + this.recommodation[3].UserId)
      xhr3.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      xhr3.send()
    },
    getUserAvator: function () {
      var xhr = new XMLHttpRequest()
      xhr.onreadystatechange = () => {
        if (xhr.readyState === 4 && xhr.status === 200) {
          var response = xhr.responseText
          var returnModel = JSON.parse(response)
          this.userAvator[0] = 'http://139.224.255.43:7779/BlogPics/Avator/' + returnModel.path.split('\\')[2]
          this.$forceUpdate()
        }
      }
      var headerToken = document.cookie.split(';')[0].split('=')[1]
      xhr.open('POST', 'http://139.224.255.43:7779/Account/getAvatrResource?nick_name=' + this.userInfo[0])
      xhr.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      xhr.send()
      var xhr1 = new XMLHttpRequest()
      xhr1.onreadystatechange = () => {
        if (xhr1.readyState === 4 && xhr1.status === 200) {
          var response = xhr1.responseText
          var returnModel = JSON.parse(response)
          this.userAvator[1] = 'http://139.224.255.43:7779/BlogPics/Avator/' + returnModel.path.split('\\')[2]
          this.$forceUpdate()
        }
      }
      xhr1.open('POST', 'http://139.224.255.43:7779/Account/getAvatrResource?nick_name=' + this.userInfo[1])
      xhr1.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      xhr1.send()
      var xhr2 = new XMLHttpRequest()
      xhr2.onreadystatechange = () => {
        if (xhr2.readyState === 4 && xhr2.status === 200) {
          var response = xhr2.responseText
          var returnModel = JSON.parse(response)
          this.userAvator[2] = 'http://139.224.255.43:7779/BlogPics/Avator/' + returnModel.path.split('\\')[2]
          this.$forceUpdate()
        }
      }
      xhr2.open('POST', 'http://139.224.255.43:7779/Account/getAvatrResource?nick_name=' + this.userInfo[2])
      xhr2.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      xhr2.send()
      var xhr3 = new XMLHttpRequest()
      xhr3.onreadystatechange = () => {
        if (xhr3.readyState === 4 && xhr3.status === 200) {
          var response = xhr3.responseText
          var returnModel = JSON.parse(response)
          this.userAvator[3] = 'http://139.224.255.43:7779/BlogPics/Avator/' + returnModel.path.split('\\')[2]
          this.$forceUpdate()
        }
      }
      xhr3.open('POST', 'http://139.224.255.43:7779/Account/getAvatrResource?nick_name=' + this.userInfo[3])
      xhr3.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      xhr3.send()
    }
  },
  mounted () {
    var xhr = new XMLHttpRequest()
    xhr.onreadystatechange = () => {
      if (xhr.readyState === 4 && xhr.status === 200) {
        var response = xhr.responseText
        var returnModel = JSON.parse(response)
        var topics = JSON.parse(returnModel.topics)
        this.topic = topics
      }
    }
    var headerToken = document.cookie.split(';')[0].split('=')[1]
    xhr.open('POST', 'http://139.224.255.43:7779/Topic/getHotestTopic?takeTopicNum=4')
    xhr.setRequestHeader('Authorization', 'Bearer ' + headerToken)
    xhr.send()
    var xhr2 = new XMLHttpRequest()
    xhr2.onreadystatechange = () => {
      if (xhr2.readyState === 4 && xhr2.status === 200) {
        var response = xhr2.responseText
        var returnModel = JSON.parse(response)
        var recommadations = JSON.parse(returnModel.articless)
        this.recommodation = recommadations
        this.getUserName()
      }
    }
    xhr2.open('POST', 'http://139.224.255.43:7779/Article/getArticlebyreadnum?takeArticleNum=10')
    xhr2.setRequestHeader('Authorization', 'Bearer ' + headerToken)
    xhr2.send()
  }
}

</script>

<style>
.mainPageBackground{
    position: absolute;
    width: 100%;
    height: 352px;
    left: 0px;
    top: 60px;
}
.divisionTitle{
    font-family: Microsoft YaHei;
    font-style: normal;
    font-weight: normal;
    line-height: 24px;
    text-align: center;
    font-size:30px;
}
.recommodation{
    position: absolute;
    width: 182px;
    height: 52px;
    left: 400px;
    top: 700px;
    background: #E5E5E5;
    box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
    border-radius: 20px;

}
.topic{
    position: absolute;
    width: 182px;
    height: 52px;
    left: 400px;
    top: 1650px;
    background: #E5E5E5;
    box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
    border-radius: 20px;
}
.study{
    position: absolute;
    width: 182px;
    height: 52px;
    left: 180px;
    top: 2200px;
    background: #E5E5E5;
    box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
    border-radius: 20px;
}
.topicFont{
    font-family: Microsoft YaHei;
    font-style: normal;
    font-weight: normal;
    font-size: 24px;
    line-height: 50px;
    display: flex;
    color: #ECE7E7;
}
.titleFont{
    position: absolute;
    left: 55px;
    top:-20px;
    font-family: Microsoft YaHei;
    font-style: normal;
    font-weight: normal;
    font-size: 28px;
    line-height: 32px;
    align-items: center;
    text-align: center;
    color: #545151;
}
.recommodationIntro{
    font-family: Microsoft YaHei;
    font-style: normal;
    font-weight: normal;
    font-size: 22px;
    line-height: 24px;
    text-align:left;
    color: #000000;
    width:600px;
}
.recommodationText{
    font-family: Microsoft YaHei;
    font-style: normal;
    font-weight: normal;
    font-size: 23px;
    line-height: 24px;
}
.text {
    font-size: 14px;
  }

.item {
    padding: 18px 0;
}
.box-card {
    width: 480px;
  }
.topicArea{
    position: absolute;
    width: 300px;
    height: 120px;
    left: 1780px;
    top: 500px;
    border-radius: 15px;
    background: url(../assets/images/universeLogo.jpg);
    background-size:300px 120px;
    background-repeat: no-repeat;
}
.topicAreaTitle{
position: absolute;
width: 108px;
height: 48px;
left:150px;
top:0px;
font-family: Microsoft YaHei;
font-style: normal;
font-weight: bold;
font-size: 36px;
line-height: 48px;
/* identical to box height */

display: flex;
align-items: center;
text-align: center;

color: #FFFFFF;
}
.hotTopic{
    position: relative;
    top:50px;
    left:-20px;
}
.hotTopic li{
    height:50px;
    list-style:none;
}
.textFont{

font-family: Microsoft YaHei;
font-style: normal;
font-weight: normal;
text-align: center;
font-size: 24px;
line-height: 24px;

color: #969191;

}
</style>

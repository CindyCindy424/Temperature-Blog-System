<template>
    <div>
        <img :class="['mainPageBackground']" src="../assets/images/banner.png">
        <el-button size="large" v-if="this.buttonActivate[1]===false" :class="['divisionTitle']" @click="switchToPage(1)" style="width:125px!important;height:60px!important;position:absolute;top:500px;left:400px;margin-left:10px;display:block;box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);border-radius: 5px;">学习</el-button>
        <el-button v-if="this.buttonActivate[2]===false" :class="['divisionTitle']" @click="switchToPage(2)" style="width:125px!important;height:60px!important;position:absolute;top:500px;left:600px;display:block;box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);border-radius: 5px;">生活</el-button>
        <el-button v-if="this.buttonActivate[3]===false" :class="['divisionTitle']" @click="switchToPage(3)" style="width:125px!important;height:60px!important;position:absolute;top:500px;left:800px;display:block;box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);border-radius: 5px;">开发</el-button>
        <el-button v-if="this.buttonActivate[4]===false" :class="['divisionTitle']" @click="switchToPage(4)" style="width:125px!important;height:60px!important;position:absolute;top:500px;left:1000px;display:block;box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);border-radius: 5px;">游戏</el-button>
        <el-button v-if="this.buttonActivate[5]===false" :class="['divisionTitle']" @click="switchToPage(5)" style="width:125px!important;height:60px!important;position:absolute;top:500px;left:1200px;display:block;box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);border-radius: 5px;">娱乐</el-button>
        <el-button v-if="this.buttonActivate[6]===false" :class="['divisionTitle']" @click="switchToPage(6)" style="width:125px!important;height:60px!important;position:absolute;top:500px;left:1400px;display:block;box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);border-radius: 5px;">体育</el-button>
        <el-button v-if="this.buttonActivate[7]===false" :class="['divisionTitle']" @click="switchToPage(7)" style="width:125px!important;height:60px!important;position:absolute;top:600px;left:400px;display:block;box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);border-radius: 5px;">影视</el-button>
        <el-button v-if="this.buttonActivate[8]===false" :class="['divisionTitle']" @click="switchToPage(8)" style="width:125px!important;height:60px!important;position:absolute;top:600px;left:600px;display:block;box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);border-radius: 5px;">咨询</el-button>
        <el-button v-if="this.buttonActivate[9]===false" :class="['divisionTitle']" @click="switchToPage(9)" style="width:125px!important;height:60px!important;position:absolute;top:600px;left:800px;display:block;box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);border-radius: 5px;">时尚</el-button>
        <el-button v-if="this.buttonActivate[10]===false" :class="['divisionTitle']" @click="switchToPage(10)" style="width:125px!important;height:60px!important;position:absolute;top:600px;left:1000px;display:block;box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);border-radius: 5px;">舞蹈</el-button>
        <el-button v-if="this.buttonActivate[11]===false" :class="['divisionTitle']" @click="switchToPage(11)" style="width:125px!important;height:60px!important;position:absolute;top:600px;left:1200px;display:block;box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);border-radius: 5px;">音乐</el-button>
        <el-button v-if="this.buttonActivate[12]===false" :class="['divisionTitle']" @click="switchToPage(12)" style="width:125px!important;height:60px!important;position:absolute;top:600px;left:1400px;display:block;box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);border-radius: 5px;">其他</el-button>
        <el-button type="danger" v-if="this.buttonActivate[1]===true" :class="['divisionTitle']" @click="switchToPage(1)" style="width:125px!important;height:60px!important;position:absolute;top:500px;margin-left:10px;left:400px;display:block;box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);border-radius: 5px;">学习</el-button>
        <el-button type="danger" v-if="this.buttonActivate[2]===true" :class="['divisionTitle']" @click="switchToPage(2)" style="width:125px!important;height:60px!important;position:absolute;top:500px;left:600px;display:block;box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);border-radius: 5px;">生活</el-button>
        <el-button type="danger" v-if="this.buttonActivate[3]===true" :class="['divisionTitle']" @click="switchToPage(3)" style="width:125px!important;height:60px!important;position:absolute;top:500px;left:800px;display:block;box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);border-radius: 5px;">开发</el-button>
        <el-button type="danger" v-if="this.buttonActivate[4]===true" :class="['divisionTitle']" @click="switchToPage(4)" style="width:125px!important;height:60px!important;width:125px!important;height:60px!important;position:absolute;top:500px;left:1000px;display:block;box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);border-radius: 5px;">游戏</el-button>
        <el-button type="danger" v-if="this.buttonActivate[5]===true" :class="['divisionTitle']" @click="switchToPage(5)" style="width:125px!important;height:60px!important;position:absolute;top:500px;left:1200px;display:block;box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);border-radius: 5px;">娱乐</el-button>
        <el-button type="danger" v-if="this.buttonActivate[6]===true" :class="['divisionTitle']" @click="switchToPage(6)" style="width:125px!important;height:60px!important;position:absolute;top:500px;left:1400px;display:block;box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);border-radius: 5px;">体育</el-button>
        <el-button type="danger" v-if="this.buttonActivate[7]===true" :class="['divisionTitle']" @click="switchToPage(7)" style="width:125px!important;height:60px!important;position:absolute;top:600px;left:400px;display:block;box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);border-radius: 5px;">影视</el-button>
        <el-button type="danger" v-if="this.buttonActivate[8]===true" :class="['divisionTitle']" @click="switchToPage(8)" style="width:125px!important;height:60px!important;position:absolute;top:600px;left:600px;display:block;box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);border-radius: 5px;">咨询</el-button>
        <el-button type="danger" v-if="this.buttonActivate[9]===true" :class="['divisionTitle']" @click="switchToPage(9)" style="width:125px!important;height:60px!important;position:absolute;top:600px;left:800px;display:block;box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);border-radius: 5px;">时尚</el-button>
        <el-button type="danger" v-if="this.buttonActivate[10]===true" :class="['divisionTitle']" @click="switchToPage(10)" style="width:125px!important;height:60px!important;position:absolute;top:600px;left:1000px;display:block;box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);border-radius: 5px;">舞蹈</el-button>
        <el-button type="danger" v-if="this.buttonActivate[11]===true" :class="['divisionTitle']" @click="switchToPage(11)" style="width:125px!important;height:60px!important;position:absolute;top:600px;left:1200px;display:block;box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);border-radius: 5px;">音乐</el-button>
        <el-button type="danger" v-if="this.buttonActivate[12]===true" :class="['divisionTitle']" @click="switchToPage(12)" style="width:125px!important;height:60px!important;position:absolute;top:600px;left:1400px;display:block;box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);border-radius: 5px;">其他</el-button>
        <p :class="['titleFont']" v-text='this.section1Title' style="position:absolute;top:700px;left:500px;"></p>
        <img :src='this.section1Img' width="50" height="50" style="position:absolute;top:730px;left:400px;">
        <hr width=1200 color=#DDD6D6 style="position:absolute;top:780px;left:400px;">
        <div style="position:absolute;top:800px;left:400px;width:1200px;" v-if="this.isZone===false">
            <h1 v-text="this.recommodation[0].Title" style="text-align:left;font-size:28px;letter-spacing:2px;"></h1>
            <p v-text="this.recommodation[0].ArticleContent" :class="['recommodationIntro']"></p>
            <img src="../assets/images/艺术设计.png" width="270px" height="140px" style="position:absolute;left:560px;top:10px;">
            <el-link type="info" v-text="'-'+this.recommodationDivision[0].zoneName+'-'" :class="['recommodationText']" style="position:absolute;top:25px;left:870px;"></el-link>
            <el-avatar :src="this.userAvator[0]" :size="40" style="position:absolute;top:106px;left:870px;"></el-avatar>
            <el-link type="info" v-text="this.userInfo[0]" :class="['recommodationText']" style="position:absolute;top:120px;left:920px;"></el-link>
            <p v-text="this.recommodation[0].ArticleLikes" :class="['recommodationText']" style="position:absolute;top:41px;left:1150px;color:#5F7D95"></p>
            <img src="../assets/images/hand.png" width="30px" height="30px" style="position:absolute;top:60px;left:1100px;">
            <p v-text="this.recommodation[0].ReadNum" :class="['recommodationText']" style="position:absolute;top:90px;left:1150px;color:#5F7D95"></p>
            <img src="../assets/images/eye.png" width="30px" height="30px" style="position:absolute;top:110px;left:1100px;">
        </div>
        <hr width=1200 color=#DDD6D6 style="position:absolute;top:960px;left:400px;">
        <div :class="['box']" style="position:absolute;top:980px;left:400px;width:1200px;" v-if="this.isZone===false">
            <h1 v-text="this.recommodation[1].Title" style="text-align:left;font-size:28px;"></h1>
            <p v-text="this.recommodation[1].ArticleContent" :class="['recommodationIntro']"></p>
            <img src="../assets/images/艺术设计.png" width="270px" height="140px" style="position:absolute;left:560px;top:10px;">
            <el-link type="info" v-text="'-'+this.recommodationDivision[1].zoneName+'-'" :class="['recommodationText']" style="position:absolute;top:25px;left:870px;"></el-link>
            <el-avatar :src="this.userAvator[1]" :size="40" style="position:absolute;top:106px;left:870px;"></el-avatar>
            <el-link type="info" v-text="this.userInfo[1]" :class="['recommodationText']" style="position:absolute;top:120px;left:920px;"></el-link>
            <p v-text="this.recommodation[1].ArticleLikes" :class="['recommodationText']" style="position:absolute;top:41px;left:1150px;color:#5F7D95"></p>
            <img src="../assets/images/hand.png" width="30px" height="30px" style="position:absolute;top:60px;left:1100px;">
            <p v-text="this.recommodation[1].ReadNum" :class="['recommodationText']" style="position:absolute;top:90px;left:1150px;color:#5F7D95"></p>
            <img src="../assets/images/eye.png" width="30px" height="30px" style="position:absolute;top:110px;left:1100px;">
        </div>
        <hr width=1200 color=#DDD6D6 style="position:absolute;top:1140px;left:400px;">
        <div :class="['box']" class="box-card" style="position:absolute;top:1160px;left:400px;width:1200px;" v-if="this.isZone===false">
            <h1 v-text="this.recommodation[2].Title" style="text-align:left;font-size:28px;"></h1>
            <p v-text="this.recommodation[2].ArticleContent" :class="['recommodationIntro']"></p>
            <img src="../assets/images/艺术设计.png" width="270px" height="140px" style="position:absolute;left:560px;top:10px;">
            <el-link type="info" v-text="'-'+this.recommodationDivision[2].zoneName+'-'" :class="['recommodationText']" style="position:absolute;top:25px;left:870px;"></el-link>
            <el-avatar :src="this.userAvator[2]" :size="40" style="position:absolute;top:106px;left:870px;"></el-avatar>
            <el-link type="info" v-text="this.userInfo[2]" :class="['recommodationText']" style="position:absolute;top:120px;left:920px;"></el-link>
            <p v-text="this.recommodation[2].ArticleLikes" :class="['recommodationText']" style="position:absolute;top:41px;left:1150px;color:#5F7D95"></p>
            <img src="../assets/images/hand.png" width="30px" height="30px" style="position:absolute;top:60px;left:1100px;">
            <p v-text="this.recommodation[2].ReadNum" :class="['recommodationText']" style="position:absolute;top:90px;left:1150px;color:#5F7D95"></p>
            <img src="../assets/images/eye.png" width="30px" height="30px" style="position:absolute;top:110px;left:1100px;">
        </div>
        <hr width=1200 color=#DDD6D6 style="position:absolute;top:1320px;left:400px;">
        <div :class="['box']" class="box-card" style="position:absolute;top:1340px;left:400px;width:1200px;" v-if="this.isZone===false">
             <h1 v-text="this.recommodation[3].Title" style="text-align:left;font-size:28px;"></h1>
            <p v-text="this.recommodation[3].ArticleContent" :class="['recommodationIntro']"></p>
            <img src="../assets/images/艺术设计.png" width="270px" height="140px" style="position:absolute;left:560px;top:10px;">
            <el-link type="info" v-text="'-'+this.recommodationDivision[3].zoneName+'-'" :class="['recommodationText']" style="position:absolute;top:25px;left:870px;"></el-link>
            <el-avatar :src="this.userAvator[3]" :size="40" style="position:absolute;top:106px;left:870px;"></el-avatar>
            <el-link type="info" v-text="this.userInfo[3]" :class="['recommodationText']" style="position:absolute;top:120px;left:920px;"></el-link>
            <p v-text="this.recommodation[3].ArticleLikes" :class="['recommodationText']" style="position:absolute;top:41px;left:1150px;color:#5F7D95"></p>
            <img src="../assets/images/hand.png" width="30px" height="30px" style="position:absolute;top:60px;left:1100px;">
            <p v-text="this.recommodation[3].ReadNum" :class="['recommodationText']" style="position:absolute;top:90px;left:1150px;color:#5F7D95"></p>
            <img src="../assets/images/eye.png" width="30px" height="30px" style="position:absolute;top:110px;left:1100px;">
        </div>
        <hr width=1200 color=#DDD6D6 style="position:absolute;top:1500px;left:400px;">
        <div :class="['box']" style="position:absolute;top:800px;left:400px;width:1200px;" v-if="this.isZone===true">
            <h1 v-text="this.sectionArticles.articleDetail[0].title" style="text-align:left;font-size:28px;"></h1>
            <p v-text="this.sectionArticles.articleDetail[0].articleContent.slice(0,10)" :class="['recommodationIntro']"></p>
            <img src="../assets/images/艺术设计.png" width="270px" height="140px" style="position:absolute;left:560px;top:10px;">
            <el-link type="info" v-text="'-'+this.currentSection+'-'" :class="['recommodationText']" style="position:absolute;top:25px;left:870px;"></el-link>
            <el-avatar :src="'http://139.224.255.43:7779/'+this.sectionArticles.articleDetail[0].avatr" :size="40" style="position:absolute;top:106px;left:870px;"></el-avatar>
            <el-link type="info" v-text="this.sectionArticles.articleDetail[0].nick_name" :class="['recommodationText']" style="position:absolute;top:120px;left:920px;"></el-link>
            <p v-text="this.sectionArticles.articleDetail[0].articleLikes" :class="['recommodationText']" style="position:absolute;top:41px;left:1150px;color:#5F7D95"></p>
            <img src="../assets/images/hand.png" width="30px" height="30px" style="position:absolute;top:60px;left:1100px;">
            <p v-text="this.sectionArticles.articleDetail[0].readNum" :class="['recommodationText']" style="position:absolute;top:90px;left:1150px;color:#5F7D95"></p>
            <img src="../assets/images/eye.png" width="30px" height="30px" style="position:absolute;top:110px;left:1100px;">
        </div>
        <hr width=1200 color=#DDD6D6 style="position:absolute;top:960px;left:400px;">
        <div :class="['box']" style="position:absolute;top:980px;left:400px;width:1200px;" v-if="this.isZone===true">
             <h1 v-text="this.sectionArticles.articleDetail[1].title" style="text-align:left;font-size:28px;"></h1>
            <p v-text="this.sectionArticles.articleDetail[1].articleContent.slice(0,10)" :class="['recommodationIntro']"></p>
            <img src="../assets/images/艺术设计.png" width="270px" height="140px" style="position:absolute;left:560px;top:10px;">
            <el-link type="info" v-text="'-'+this.currentSection+'-'" :class="['recommodationText']" style="position:absolute;top:25px;left:870px;"></el-link>
            <el-avatar :src="'http://139.224.255.43:7779/'+this.sectionArticles.articleDetail[1].avatr" :size="40" style="position:absolute;top:106px;left:870px;"></el-avatar>
            <el-link type="info" v-text="this.sectionArticles.articleDetail[1].nick_name" :class="['recommodationText']" style="position:absolute;top:120px;left:920px;"></el-link>
            <p v-text="this.sectionArticles.articleDetail[1].articleLikes" :class="['recommodationText']" style="position:absolute;top:41px;left:1150px;color:#5F7D95"></p>
            <img src="../assets/images/hand.png" width="30px" height="30px" style="position:absolute;top:60px;left:1100px;">
            <p v-text="this.sectionArticles.articleDetail[1].readNum" :class="['recommodationText']" style="position:absolute;top:90px;left:1150px;color:#5F7D95"></p>
            <img src="../assets/images/eye.png" width="30px" height="30px" style="position:absolute;top:110px;left:1100px;">
        </div>
        <hr width=1200 color=#DDD6D6 style="position:absolute;top:1140px;left:400px;">
        <div :class="['box']" style="position:absolute;top:1160px;left:400px;width:1200px;" v-if="this.isZone===true">
            <h1 v-text="this.sectionArticles.articleDetail[2].title" style="text-align:left;font-size:28px;"></h1>
            <p v-text="this.sectionArticles.articleDetail[2].articleContent.slice(0,10)" :class="['recommodationIntro']"></p>
            <img src="../assets/images/艺术设计.png" width="270px" height="140px" style="position:absolute;left:560px;top:10px;">
            <el-link type="info" v-text="'-'+this.currentSection+'-'" :class="['recommodationText']" style="position:absolute;top:25px;left:870px;"></el-link>
            <el-avatar :src="'http://139.224.255.43:7779/'+this.sectionArticles.articleDetail[2].avatr" :size="40" style="position:absolute;top:106px;left:870px;"></el-avatar>
            <el-link type="info" v-text="this.sectionArticles.articleDetail[2].nick_name" :class="['recommodationText']" style="position:absolute;top:120px;left:920px;"></el-link>
            <p v-text="this.sectionArticles.articleDetail[2].articleLikes" :class="['recommodationText']" style="position:absolute;top:41px;left:1150px;color:#5F7D95"></p>
            <img src="../assets/images/hand.png" width="30px" height="30px" style="position:absolute;top:60px;left:1100px;">
            <p v-text="this.sectionArticles.articleDetail[2].readNum" :class="['recommodationText']" style="position:absolute;top:90px;left:1150px;color:#5F7D95"></p>
            <img src="../assets/images/eye.png" width="30px" height="30px" style="position:absolute;top:110px;left:1100px;">
        </div>
        <hr width=1200 color=#DDD6D6 style="position:absolute;top:1320px;left:400px;">
        <div :class="['box']" style="position:absolute;top:1340px;left:400px;width:1200px;" v-if="this.isZone===true">
             <h1 v-text="this.sectionArticles.articleDetail[3].title" style="text-align:left;font-size:28px;"></h1>
            <p v-text="this.sectionArticles.articleDetail[3].articleContent.slice(0,10)" :class="['recommodationIntro']"></p>
            <img src="../assets/images/艺术设计.png" width="270px" height="140px" style="position:absolute;left:560px;top:10px;">
            <el-link type="info" v-text="'-'+this.currentSection+'-'" :class="['recommodationText']" style="position:absolute;top:25px;left:870px;"></el-link>
            <el-avatar :src="'http://139.224.255.43:7779/'+this.sectionArticles.articleDetail[3].avatr" :size="40" style="position:absolute;top:106px;left:870px;"></el-avatar>
            <el-link type="info" v-text="this.sectionArticles.articleDetail[3].nick_name" :class="['recommodationText']" style="position:absolute;top:120px;left:920px;"></el-link>
            <p v-text="this.sectionArticles.articleDetail[3].articleLikes" :class="['recommodationText']" style="position:absolute;top:41px;left:1150px;color:#5F7D95"></p>
            <img src="../assets/images/hand.png" width="30px" height="30px" style="position:absolute;top:60px;left:1100px;">
            <p v-text="this.sectionArticles.articleDetail[3].readNum" :class="['recommodationText']" style="position:absolute;top:90px;left:1150px;color:#5F7D95"></p>
            <img src="../assets/images/eye.png" width="30px" height="30px" style="position:absolute;top:110px;left:1100px;">
        </div>
        <hr width=1200 color=#DDD6D6 style="position:absolute;top:1500px;left:400px;">
        <div v-if="this.isZone===false" style="position:absolute;top:1600px;left:400px;">
            <p :class="['titleFont']" style="width:200px;left:60px;">话题速递</p>
            <img src="../assets/images/analytics.png" width="50" height="50" style="position:absolute;top:4px;left:10px;">
        </div>
        <div style="position:absolute;top:1750px;left:400px;" v-if="this.isZone===false">
        <img src="../assets/images/babe-fyrcsrv8730487.jpg" width="500" height="265px">
        <p :class="['topicFont']" style="width:280px;height:100px;position:absolute;position:absolute;top:25%;left:20%;font-size:40px;" v-text="this.topic[0].TopicTitle"></p>
        </div>
        <div style="position:absolute;top:1750px;left:920px;" v-if="this.isZone===false">
          <img src="../assets/images/艺术设计.png" width="320" height="116px" style="position:absolute;top:0px;left:0px;">
          <p :class="['topicFont']" style="width:200px;height:110px;position:absolute;top:40%;left:60px;;font-size:20px;" v-text="this.topic[1].TopicTitle"></p>
        </div>
        <div style="position:absolute;top:1882px;left:920px;" v-if="this.isZone===false">
            <img src="../assets/images/自然美景.png" width="320" height="131px" style="position:absolute;top:0px;left:0px;">
            <p :class="['topicFont']" style="width:200px;height:90px;position:absolute;top:40%;left:60px;font-size:20px;" v-text="this.topic[2].TopicTitle"></p>
        </div>
        <div style="position:absolute;top:1750px;left:1260px;" v-if="this.isZone===false">
            <img src="../assets/images/宇宙深空.png" width="341.47px" height="265px" >
            <p :class="['topicFont']" style="width:300px;height:100px;position:absolute;top:25%;left:8%;font-size:30px;" v-text="this.topic[3].TopicTitle"></p>
        </div>
        <div v-if="this.isZone===true" style="position:absolute;top:1600px;left:400px;">
            <p :class="['titleFont']" style="width:200px;left:60px;">最近更新</p>
            <img src="../assets/images/star.png" width="50" height="50" style="position:absolute;top:4px;left:10px;">
        </div>
        <hr width=1200 color=#DDD6D6 style="position:absolute;top:1670px;left:400px;">
        <div v-if="this.isZone===true">
             <el-card class="box-card" style="position:absolute;top:1730px;left:400px;width:1200px;height:200px;">
                    <h1 v-text="this.sectionArticles.articleDetail[0].title" style="text-align:left;font-size:28px;position:absolute;left:40px;"></h1>
                    <el-avatar :src="'http://139.224.255.43:7779/'+this.sectionArticles.articleDetail[0].avatr" :size="50" style="position:absolute;top:130px;left:30px;"></el-avatar>
                    <el-link type="info" v-text="this.sectionArticles.articleDetail[0].nick_name" :class="['recommodationText']" style="position:absolute;top:145px;left:90px;"></el-link>
                    <p v-text="this.sectionArticles.articleDetail[0].articleContent.slice(0,20)+'...'" :class="['recommodationText']" style="position:absolute;top:65px;left:40px;"></p>
                    <p v-text="this.sectionArticles.articleDetail[0].articleLikes" :class="['recommodationText']" style="position:absolute;top:110px;left:850px;color:#5F7D95"></p>
                    <img src="../assets/images/hand.png" width="30px" height="30px" style="position:absolute;top:130px;left:800px;">
                    <p v-text="this.sectionArticles.articleDetail[0].readNum" :class="['recommodationText']" style="position:absolute;top:110px;left:1050px;color:#5F7D95"></p>
                    <img src="../assets/images/eye.png" width="30px" height="30px" style="position:absolute;top:130px;left:1000px;">
              </el-card>
              <el-card class="box-card" style="position:absolute;top:1950px;left:400px;width:1200px;height:200px;">
                    <h1 v-text="this.sectionArticles.articleDetail[1].title" style="text-align:left;font-size:28px;position:absolute;left:40px;"></h1>
                    <el-avatar :src="'http://139.224.255.43:7779/'+this.sectionArticles.articleDetail[1].avatr" :size="50" style="position:absolute;top:130px;left:30px;"></el-avatar>
                    <el-link type="info" v-text="this.sectionArticles.articleDetail[1].nick_name" :class="['recommodationText']" style="position:absolute;top:145px;left:90px;"></el-link>
                    <p v-text="this.sectionArticles.articleDetail[1].articleContent.slice(0,20)+'...'" :class="['recommodationText']" style="position:absolute;top:65px;left:40px;"></p>
                    <p v-text="this.sectionArticles.articleDetail[1].articleLikes" :class="['recommodationText']" style="position:absolute;top:110px;left:850px;color:#5F7D95"></p>
                    <img src="../assets/images/hand.png" width="30px" height="30px" style="position:absolute;top:130px;left:800px;">
                    <p v-text="this.sectionArticles.articleDetail[1].readNum" :class="['recommodationText']" style="position:absolute;top:110px;left:1050px;color:#5F7D95"></p>
                    <img src="../assets/images/eye.png" width="30px" height="30px" style="position:absolute;top:130px;left:1000px;">
              </el-card>
              <el-card class="box-card" style="position:absolute;top:2170px;left:400px;width:1200px;height:200px;">
                    <h1 v-text="this.sectionArticles.articleDetail[2].title" style="text-align:left;font-size:28px;position:absolute;left:40px;"></h1>
                    <el-avatar :src="'http://139.224.255.43:7779/'+this.sectionArticles.articleDetail[2].avatr" :size="50" style="position:absolute;top:130px;left:30px;"></el-avatar>
                    <el-link type="info" v-text="this.sectionArticles.articleDetail[2].nick_name" :class="['recommodationText']" style="position:absolute;top:145px;left:90px;"></el-link>
                    <p v-text="this.sectionArticles.articleDetail[2].articleContent.slice(0,20)+'...'" :class="['recommodationText']" style="position:absolute;top:65px;left:40px;"></p>
                    <p v-text="this.sectionArticles.articleDetail[2].articleLikes" :class="['recommodationText']" style="position:absolute;top:110px;left:850px;color:#5F7D95"></p>
                    <img src="../assets/images/hand.png" width="30px" height="30px" style="position:absolute;top:130px;left:800px;">
                    <p v-text="this.sectionArticles.articleDetail[2].readNum" :class="['recommodationText']" style="position:absolute;top:110px;left:1050px;color:#5F7D95"></p>
                    <img src="../assets/images/eye.png" width="30px" height="30px" style="position:absolute;top:130px;left:1000px;">
              </el-card>
              <el-card class="box-card" style="position:absolute;top:2390px;left:400px;width:1200px;height:200px;">
                    <h1 v-text="this.sectionArticles.articleDetail[3].title" style="text-align:left;font-size:28px;position:absolute;left:40px;"></h1>
                    <el-avatar :src="'http://139.224.255.43:7779/'+this.sectionArticles.articleDetail[3].avatr" :size="50" style="position:absolute;top:130px;left:30px;"></el-avatar>
                    <el-link type="info" v-text="this.sectionArticles.articleDetail[3].nick_name" :class="['recommodationText']" style="position:absolute;top:145px;left:90px;"></el-link>
                    <p v-text="this.sectionArticles.articleDetail[3].articleContent.slice(0,20)+'...'" :class="['recommodationText']" style="position:absolute;top:65px;left:40px;"></p>
                    <p v-text="this.sectionArticles.articleDetail[3].articleLikes" :class="['recommodationText']" style="position:absolute;top:110px;left:850px;color:#5F7D95"></p>
                    <img src="../assets/images/hand.png" width="30px" height="30px" style="position:absolute;top:130px;left:800px;">
                    <p v-text="this.sectionArticles.articleDetail[3].readNum" :class="['recommodationText']" style="position:absolute;top:110px;left:1050px;color:#5F7D95"></p>
                    <img src="../assets/images/eye.png" width="30px" height="30px" style="position:absolute;top:130px;left:1000px;">
              </el-card>
              <el-card class="box-card" style="position:absolute;top:2610px;left:400px;width:1200px;height:200px;">
                    <h1 v-text="this.sectionArticles.articleDetail[4].title" style="text-align:left;font-size:28px;position:absolute;left:40px;"></h1>
                    <el-avatar :src="'http://139.224.255.43:7779/'+this.sectionArticles.articleDetail[4].avatr" :size="50" style="position:absolute;top:130px;left:30px;"></el-avatar>
                    <el-link type="info" v-text="this.sectionArticles.articleDetail[4].nick_name" :class="['recommodationText']" style="position:absolute;top:145px;left:90px;"></el-link>
                    <p v-text="this.sectionArticles.articleDetail[4].articleContent.slice(0,20)+'...'" :class="['recommodationText']" style="position:absolute;top:65px;left:40px;"></p>
                    <p v-text="this.sectionArticles.articleDetail[4].articleLikes" :class="['recommodationText']" style="position:absolute;top:110px;left:850px;color:#5F7D95"></p>
                    <img src="../assets/images/hand.png" width="30px" height="30px" style="position:absolute;top:130px;left:800px;">
                    <p v-text="this.sectionArticles.articleDetail[4].readNum" :class="['recommodationText']" style="position:absolute;top:110px;left:1050px;color:#5F7D95"></p>
                    <img src="../assets/images/eye.png" width="30px" height="30px" style="position:absolute;top:130px;left:1000px;">
              </el-card>
              <el-card class="box-card" style="position:absolute;top:2830px;left:400px;width:1200px;height:200px;">
                    <h1 v-text="this.sectionArticles.articleDetail[5].title" style="text-align:left;font-size:28px;position:absolute;left:40px;"></h1>
                    <el-avatar :src="'http://139.224.255.43:7779/'+this.sectionArticles.articleDetail[5].avatr" :size="50" style="position:absolute;top:130px;left:30px;"></el-avatar>
                    <el-link type="info" v-text="this.sectionArticles.articleDetail[5].nick_name" :class="['recommodationText']" style="position:absolute;top:145px;left:90px;"></el-link>
                    <p v-text="this.sectionArticles.articleDetail[5].articleContent.slice(0,20)+'...'" :class="['recommodationText']" style="position:absolute;top:65px;left:40px;"></p>
                    <p v-text="this.sectionArticles.articleDetail[5].articleLikes" :class="['recommodationText']" style="position:absolute;top:110px;left:850px;color:#5F7D95"></p>
                    <img src="../assets/images/hand.png" width="30px" height="30px" style="position:absolute;top:130px;left:800px;">
                    <p v-text="this.sectionArticles.articleDetail[5].readNum" :class="['recommodationText']" style="position:absolute;top:110px;left:1050px;color:#5F7D95"></p>
                    <img src="../assets/images/eye.png" width="30px" height="30px" style="position:absolute;top:130px;left:1000px;">
              </el-card>
              <el-card class="box-card" style="position:absolute;top:3050px;left:400px;width:1200px;height:200px;">
                    <h1 v-text="this.sectionArticles.articleDetail[6].title" style="text-align:left;font-size:28px;position:absolute;left:40px;"></h1>
                    <el-avatar :src="'http://139.224.255.43:7779/'+this.sectionArticles.articleDetail[6].avatr" :size="50" style="position:absolute;top:130px;left:30px;"></el-avatar>
                    <el-link type="info" v-text="this.sectionArticles.articleDetail[6].nick_name" :class="['recommodationText']" style="position:absolute;top:145px;left:90px;"></el-link>
                    <p v-text="this.sectionArticles.articleDetail[6].articleContent.slice(0,20)+'...'" :class="['recommodationText']" style="position:absolute;top:65px;left:40px;"></p>
                    <p v-text="this.sectionArticles.articleDetail[6].articleLikes" :class="['recommodationText']" style="position:absolute;top:110px;left:850px;color:#5F7D95"></p>
                    <img src="../assets/images/hand.png" width="30px" height="30px" style="position:absolute;top:130px;left:800px;">
                    <p v-text="this.sectionArticles.articleDetail[6].readNum" :class="['recommodationText']" style="position:absolute;top:110px;left:1050px;color:#5F7D95"></p>
                    <img src="../assets/images/eye.png" width="30px" height="30px" style="position:absolute;top:130px;left:1000px;">
              </el-card>
              <el-card class="box-card" style="position:absolute;top:3270px;left:400px;width:1200px;height:200px;">
                    <h1 v-text="this.sectionArticles.articleDetail[7].title" style="text-align:left;font-size:28px;position:absolute;left:40px;"></h1>
                    <el-avatar :src="'http://139.224.255.43:7779/'+this.sectionArticles.articleDetail[7].avatr" :size="50" style="position:absolute;top:130px;left:30px;"></el-avatar>
                    <el-link type="info" v-text="this.sectionArticles.articleDetail[7].nick_name" :class="['recommodationText']" style="position:absolute;top:145px;left:90px;"></el-link>
                    <p v-text="this.sectionArticles.articleDetail[7].articleContent.slice(0,20)+'...'" :class="['recommodationText']" style="position:absolute;top:65px;left:40px;"></p>
                    <p v-text="this.sectionArticles.articleDetail[7].articleLikes" :class="['recommodationText']" style="position:absolute;top:110px;left:850px;color:#5F7D95"></p>
                    <img src="../assets/images/hand.png" width="30px" height="30px" style="position:absolute;top:130px;left:800px;">
                    <p v-text="this.sectionArticles.articleDetail[7].readNum" :class="['recommodationText']" style="position:absolute;top:110px;left:1050px;color:#5F7D95"></p>
                    <img src="../assets/images/eye.png" width="30px" height="30px" style="position:absolute;top:130px;left:1000px;">
              </el-card>
              <el-card class="box-card" style="position:absolute;top:3490px;left:400px;width:1200px;height:200px;">
                    <h1 v-text="this.sectionArticles.articleDetail[8].title" style="text-align:left;font-size:28px;position:absolute;left:40px;"></h1>
                    <el-avatar :src="'http://139.224.255.43:7779/'+this.sectionArticles.articleDetail[8].avatr" :size="50" style="position:absolute;top:130px;left:30px;"></el-avatar>
                    <el-link type="info" v-text="this.sectionArticles.articleDetail[8].nick_name" :class="['recommodationText']" style="position:absolute;top:145px;left:90px;"></el-link>
                    <p v-text="this.sectionArticles.articleDetail[8].articleContent.slice(0,20)+'...'" :class="['recommodationText']" style="position:absolute;top:65px;left:40px;"></p>
                    <p v-text="this.sectionArticles.articleDetail[8].articleLikes" :class="['recommodationText']" style="position:absolute;top:110px;left:850px;color:#5F7D95"></p>
                    <img src="../assets/images/hand.png" width="30px" height="30px" style="position:absolute;top:130px;left:800px;">
                    <p v-text="this.sectionArticles.articleDetail[8].readNum" :class="['recommodationText']" style="position:absolute;top:110px;left:1050px;color:#5F7D95"></p>
                    <img src="../assets/images/eye.png" width="30px" height="30px" style="position:absolute;top:130px;left:1000px;">
              </el-card>
              <el-card class="box-card" style="position:absolute;top:3710px;left:400px;width:1200px;height:200px;">
                    <h1 v-text="this.sectionArticles.articleDetail[9].title" style="text-align:left;font-size:28px;position:absolute;left:40px;"></h1>
                    <el-avatar :src="'http://139.224.255.43:7779/'+this.sectionArticles.articleDetail[9].avatr" :size="50" style="position:absolute;top:130px;left:30px;"></el-avatar>
                    <el-link type="info" v-text="this.sectionArticles.articleDetail[9].nick_name" :class="['recommodationText']" style="position:absolute;top:145px;left:90px;"></el-link>
                    <p v-text="this.sectionArticles.articleDetail[9].articleContent.slice(0,20)+'...'" :class="['recommodationText']" style="position:absolute;top:65px;left:40px;"></p>
                    <p v-text="this.sectionArticles.articleDetail[9].articleLikes" :class="['recommodationText']" style="position:absolute;top:110px;left:850px;color:#5F7D95"></p>
                    <img src="../assets/images/hand.png" width="30px" height="30px" style="position:absolute;top:130px;left:800px;">
                    <p v-text="this.sectionArticles.articleDetail[9].readNum" :class="['recommodationText']" style="position:absolute;top:110px;left:1050px;color:#5F7D95"></p>
                    <img src="../assets/images/eye.png" width="30px" height="30px" style="position:absolute;top:130px;left:1000px;">
              </el-card>
        </div>
        <div :class="['topicArea']">
            <p :class="['topicAreaTitle']">话题区</p>
        </div>
        <div style="position:absolute;top:700px;left:1780px;width:300px;">
            <div :class="['rankTitle']">
              <p :class="['titleFont']" style="top:0px;left:37%;font-size:30px;font-family: Microsoft YaHei;font-style: normal;font-weight: bold;color: #FFFFFF;">博文热榜</p>
              <img src="../assets/images/fire.png" style="position:relative;left:-70px;top:20px;" width="55" height="55" >
            </div>
            <ul class="hotTopic">
                <li :class="['rankList']">
                    <p :class="['rankNumber']" style="position:relative;left:-110px;top:20px;">1</p>
                    <p v-text="this.recommodation[0].Title" class="textFont" style="position:relative;left:70px;"></p>
                    <p v-text="this.recommodation[0].ArticleContent" class="textContent" style="position:relative;top:-180px;left:30px;"></p>
                </li>
                <li :class="['rankList']" style="position:relative;top:-27px;">
                  <p :class="['rankNumber']" style="position:relative;left:-110px;top:20px;">2</p>
                  <p v-text="this.recommodation[1].Title" class="textFont" style="position:relative;left:70px;"></p>
                  <p v-text="this.recommodation[1].ArticleContent" class="textContent" style="position:relative;top:-180px;left:30px;"></p>
                </li>
                <li :class="['rankList']" style="position:relative;top:-58px;">
                <p :class="['rankNumber']" style="position:relative;left:-110px;top:20px;">3</p>
                <p v-text="this.recommodation[2].Title" class="textFont" style="position:relative;left:70px;"></p>
                <p v-text="this.recommodation[2].ArticleContent" class="textContent" style="position:relative;top:-180px;left:30px;"></p>
                </li>
                <li :class="['rankList']" style="position:relative;top:-88px;">
                <p :class="['rankNumber']" style="position:relative;left:-110px;top:20px;">4</p>
                <p v-text="this.recommodation[3].Title" class="textFont" style="position:relative;left:70px;"></p>
                <p v-text="this.recommodation[3].ArticleContent" class="textContent" style="position:relative;top:-180px;left:30px;"></p>
                </li>
                <li :class="['rankList']" style="position:relative;top:-118px;">
                <p :class="['rankNumber']" style="position:relative;left:-110px;top:20px;">5</p>
                <p v-text="this.recommodation[4].Title" class="textFont" style="position:relative;left:70px;"></p>
                <p v-text="this.recommodation[4].ArticleContent" class="textContent" style="position:relative;top:-180px;left:30px;"></p>
                </li>
                <li :class="['rankList']" style="position:relative;top:-145px;">
                <p :class="['rankNumber']" style="position:relative;left:-110px;top:20px;">6</p>
                <p v-text="this.recommodation[5].Title" class="textFont" style="position:relative;left:70px;"></p>
                <p v-text="this.recommodation[5].ArticleContent" class="textContent" style="position:relative;top:-180px;left:30px;"></p>
                </li>
            </ul>
        </div>
        <div style="position:absolute;top:1600px;left:1780px;width:300px;">
            <div :class="['rankTitle']">
              <p :class="['titleFont']" style="top:0px;left:37%;font-size:30px;font-family: Microsoft YaHei;font-style: normal;font-weight: bold;color: #FFFFFF;">热门博主</p>
              <img src="../assets/images/star (2).png" style="position:relative;left:-70px;top:20px;" width="55" height="55" >
            </div>
            <ul class="hotTopic">
                <li :class="['rankList']">
                    <p :class="['rankNumber']" style="position:relative;left:-110px;top:20px;">1</p>
                    <p v-text="this.hotUser[0].userName" class="textFont" style="position:relative;left:70px;"></p>
                    <p v-text="this.hotUserAnnounce[0]" class="textContent" style="position:relative;top:-180px;left:30px;"></p>
                </li>
                <li :class="['rankList']" style="position:relative;top:-27px;">
                  <p :class="['rankNumber']" style="position:relative;left:-110px;top:20px;">2</p>
                  <p v-text="this.hotUser[1].userName" class="textFont" style="position:relative;left:70px;"></p>
                  <p v-text="this.hotUserAnnounce[1]" class="textContent" style="position:relative;top:-180px;left:30px;"></p>
                </li>
                <li :class="['rankList']" style="position:relative;top:-58px;">
                <p :class="['rankNumber']" style="position:relative;left:-110px;top:20px;">3</p>
                <p v-text="this.hotUser[2].userName" class="textFont" style="position:relative;left:70px;"></p>
                <p v-text="this.hotUserAnnounce[2]" class="textContent" style="position:relative;top:-180px;left:30px;"></p>
                </li>
                <li :class="['rankList']" style="position:relative;top:-88px;">
                <p :class="['rankNumber']" style="position:relative;left:-110px;top:20px;">4</p>
                <p v-text="this.hotUser[3].userName" class="textFont" style="position:relative;left:70px;"></p>
                <p v-text="this.hotUserAnnounce[3]" class="textContent" style="position:relative;top:-180px;left:30px;"></p>
                </li>
                <li :class="['rankList']" style="position:relative;top:-118px;">
                <p :class="['rankNumber']" style="position:relative;left:-110px;top:20px;">5</p>
                <p v-text="this.hotUser[4].userName" class="textFont" style="position:relative;left:70px;"></p>
                <p v-text="this.hotUserAnnounce[4]" class="textContent" style="position:relative;top:-180px;left:30px;"></p>
                </li>
                <li :class="['rankList']" style="position:relative;top:-145px;">
                <p :class="['rankNumber']" style="position:relative;left:-110px;top:20px;">6</p>
                <p v-text="this.hotUser[5].userName" class="textFont" style="position:relative;left:70px;"></p>
                <p v-text="this.hotUserAnnounce[5]" class="textContent" style="position:relative;top:-180px;left:30px;"></p>
                </li>
            </ul>
        </div>
        <div v-if="this.isZone===true">
          <footer style="position:absolute;top:3980px;;left:40%;" :class="['footer']">
            <p> Copyright ©2020 Temperature team, All Rights Reserved.</p>
            <p><br/>&nbsp;</p>
          </footer>
        </div>
    </div>
</template>

<script>
export default {
  name: 'MainPage',
  data () {
    return {
      recommodation: [],
      topic: [],
      userInfo: [],
      userAvator: [],
      currentArea: '',
      recommodationText: '热门推荐',
      selectedText: '博文精选',
      section1Title: '热门推荐',
      recommodationImg: require('../assets/images/star.png'),
      selectedImg: require('../assets/images/star (2).png'),
      section1Img: require('../assets/images/star.png'),
      hotUser: [],
      isZone: false,
      sectionArticles: [],
      recommodationDivision: [],
      currentSection: '',
      buttonActivate: [false, false, false, false, false, false, false, false, false, false, false, false, false],
      hotUserAnnounce: []
    }
  },
  methods: {
    switchToPage: function (section) {
      console.log(section)
      this.buttonActivate[section] = true
      this.section1Title = this.selectedText
      this.section1Img = this.selectedImg
      this.isZone = true
      this.getZoneNewArticle(section)
      var xhr = new XMLHttpRequest()
      xhr.onreadystatechange = () => {
        var response = xhr.responseText
        var returnModel = JSON.parse(response)
        this.currentSection = returnModel.zoneName
      }
      var headerToken = document.cookie.split(';')[0].split('=')[1]
      xhr.open('POST', 'http://139.224.255.43:7779/Zone/getZoneName?zoneID=' + section)
      xhr.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      xhr.send()
    },
    getZoneNewArticle: function (section) {
      var xhr = new XMLHttpRequest()
      xhr.onreadystatechange = () => {
        var response = xhr.responseText
        var returnModel = JSON.parse(response)
        this.sectionArticles = returnModel
      }
      var headerToken = document.cookie.split(';')[0].split('=')[1]
      xhr.open('POST', 'http://139.224.255.43:7779/Article/getArticleByPage?pageNum=1&pageSize=10&zoneId=' + section)
      xhr.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      xhr.send()
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
    },
    getHotUser: function () {
      var xhr = new XMLHttpRequest()
      xhr.onreadystatechange = () => {
        var response = xhr.responseText
        this.hotUser = JSON.parse(response)
        for (var i = 0; i < 6; i++) { this.getHotUserAnnouncement(i) }
      }
      var headerToken = document.cookie.split(';')[0].split('=')[1]
      xhr.open('POST', 'http://139.224.255.43:7779/Account/getFamousUser')
      xhr.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      xhr.send()
    },
    getHotUserAnnouncement: function (index) {
      var xhr = new XMLHttpRequest()
      xhr.onreadystatechange = () => {
        var response = xhr.responseText
        this.hotUserAnnounce[index] = JSON.parse(response).content.announcementContent
      }
      var headerToken = document.cookie.split(';')[0].split('=')[1]
      xhr.open('POST', 'http://139.224.255.43:7779/Account/getAnnouncementByNickName?nick_name=' + this.hotUser[index].userName)
      xhr.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      xhr.send()
    },
    getDivision: function () {
      var xhr = new XMLHttpRequest()
      xhr.onreadystatechange = () => {
        var response = xhr.responseText
        this.recommodationDivision[0] = JSON.parse(response)
      }
      var headerToken = document.cookie.split(';')[0].split('=')[1]
      xhr.open('POST', 'http://139.224.255.43:7779/Zone/getZoneName?zoneID=' + this.recommodation[0].ZoneId)
      xhr.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      xhr.send()
      var xhr1 = new XMLHttpRequest()
      xhr1.onreadystatechange = () => {
        var response = xhr1.responseText
        this.recommodationDivision[1] = JSON.parse(response)
      }
      xhr1.open('POST', 'http://139.224.255.43:7779/Zone/getZoneName?zoneID=' + this.recommodation[1].ZoneId)
      xhr1.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      xhr1.send()
      var xhr2 = new XMLHttpRequest()
      xhr2.onreadystatechange = () => {
        var response = xhr2.responseText
        this.recommodationDivision[2] = JSON.parse(response)
      }
      xhr2.open('POST', 'http://139.224.255.43:7779/Zone/getZoneName?zoneID=' + this.recommodation[2].ZoneId)
      xhr2.setRequestHeader('Authorization', 'Bearer ' + headerToken)
      xhr2.send()
      var xhr3 = new XMLHttpRequest()
      xhr3.onreadystatechange = () => {
        var response = xhr3.responseText
        this.recommodationDivision[3] = JSON.parse(response)
      }
      xhr3.open('POST', 'http://139.224.255.43:7779/Zone/getZoneName?zoneID=' + this.recommodation[3].ZoneId)
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
        var recommadations = JSON.parse(returnModel.Result)
        this.recommodation = recommadations
        this.getUserName()
        this.getHotUser()
        this.getDivision()
      }
    }
    xhr2.open('POST', 'http://139.224.255.43:7779/Article/getRecommandedArticle?pageNum=1&pageSize=10')
    xhr2.setRequestHeader('Authorization', 'Bearer ' + headerToken)
    xhr2.send()
  }
}

</script>

<style>
.mainPageBackground{
    position: absolute;
    width: 100%;
    height: 412px;
    left: 0px;
    top: 0px;
}
.divisionTitle{
    font-family: Microsoft YaHei;
    font-style: normal;
    font-weight: bold;
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
    line-height: 30px;
    color: #ECE7E7;
    overflow: hidden;
    text-overflow: ellipsis;
    text-align:center;
}
.titleFont{
    position: absolute;
    left: 55px;
    top:-20px;
    font-family: Microsoft YaHei;
    font-style: normal;
    font-weight: normal;
    font-size: 32px;
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
    color: #605C5C;
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
    top:-38px;
    left:-38.8px;
}
.hotTopic li{
    height:120px;
    list-style:none;
}
.textFont{

font-family: Microsoft YaHei;
font-style: normal;
font-weight: normal;
font-size: 24px;
line-height: 120px;
width:150px;
color: #FFFFFF;
position:relative;
left:-44px;
top:-83px;
text-align: left;
}
.textContent{
font-family: Microsoft YaHei;
font-style: normal;
font-weight: normal;
text-align: left;
font-size: 24px;
line-height: 120px;
height:80px;
width:200px;
overflow:hidden;
color: #FFFFFF;
}
.rankTitle{
  box-shadow: 4px 4px 1px rgba(0, 0, 0, 0.25);
  border-radius: 10px;
  background: #605C5C;
  height:100px;
}
.rankList{
  background: #444242;
  box-shadow: inset 0px 4px 4px rgba(0, 0, 0, 0.25);
  width:300px;
}
.rankNumber{
  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: bold;
  font-size: 28px;
  line-height: 29px;
  color: #C76B4E;
}
.el-button{
  margin-left:0px!important;
}
</style>

<template>
  <div id="Photo">
    <img id="banner" src="../assets/images/banner.png" />
    <div id="album-header">
      <span>
        <router-link to="/Album" id="return">相册</router-link>
        >{{albumName}}
      </span>
    </div>
    <div id="menu">
      <el-avatar
        id="album-avatar"
        shape="square"
        :size="135"
        src="https://empty"
        @error="errorHandler"
      >
        <img src="https://cube.elemecdn.com/e/fd/0fc7d20532fdaf769a25683617711png.png" />
      </el-avatar>
      <div id="album-info">
        <span id="album-name">{{albumName}} /共{{photoNumber}}张</span>
        <br />
        <br />
        <span id="album-time">{{albumTime}}</span>
        <br />
        <br />
        <span id="album-intro">{{albumIntro}}</span>
      </div>
      <el-upload
        class="upload-demo"
        action="https://jsonplaceholder.typicode.com/posts/"
        multiple
        :show-file-list="true"
        :limit="3"
        :file-list="fileList"
      >
        <div id="upload-photo" v-on:click="uploadPhoto($event)" style="cursor:pointer;">
          <span>上传图片/视频</span>
        </div>
      </el-upload>
      <br />
      <div id="delete-album" v-on:click="deletePhoto" style="cursor:pointer;">
        <span ref="pdelete">删除图片/视频</span>
      </div>
    </div>
    <div id="photo-list">
      <el-row>
        <el-col
          :span="8"
          v-for="(photo, index) in photolist"
          :key="photo.PhotoId"
          :offset="index > 0 ? 0 : 0"
        >
          <el-card :body-style="{padding:'0px'}" class="pcard">
            <i
              class="el-icon-close"
              v-if="ispDelete"
              style="position:absolute; left:262px; font-size:25px;"
              @click="confirmpDelete(photo.PhotoId)"
            ></i>
            <img
              :src="rootUrl+photo.PhotoAddress"
              ref="img"
              class="image"
              style="cursor: pointer;"
              @click="view"
            />
            <div style="padding:19px;border: 1px solid #B9B4B4;background:#f3f0f0;">
              <span ref="period" style="cursor: pointer;" @click="view">
                {{photo.PhotoUploadTime}}
                <!-- <span style="margin-left: 30px;" @click="view">{{photoTime}}</span> -->
              </span>
            </div>
          </el-card>
        </el-col>
      </el-row>
    </div>
    <div id="cover" ref="cover"></div>
    <div id="delete-photo-in" v-if="ispDelIn">
      <div id="pd-header">
        <span>删除照片</span>
      </div>
      <span id="pconfirm">您确定要删除照片吗？</span>
      <div id="pconfirm-button" @click="confirmpDeleteIn">
        <span style="cursor: pointer;">确认</span>
      </div>
      <div id="pcancelDelete-button" @click="cancelpDeleteIn">
        <span style="cursor: pointer;">取消</span>
      </div>
    </div>
    <div id="view-photo" v-if="isView">
      <div class="out-block">
        <div class="block">
          <el-carousel height="520px" :autoplay="false">
            <el-carousel-item v-for="item in photos" :key="item.photoID">
              <el-image style="width: 100%; height: 100%;" :src="item.src" :fit="fit"></el-image>
            </el-carousel-item>
          </el-carousel>
          <span id="photo-description">和同学一起开心地做课设</span>
        </div>
      </div>
      <div class="right-block">
        <i
          class="el-icon-close"
          style="position:relative; left:225px; top:-10px; font-size:25px; color:red; z-index:3; cursor:pointer"
          v-if="isView"
          @click="shutdown"
        ></i>
        <el-avatar id="vp-avatar" :size="60" src="https://empty" @error="errorHandler">
          <img src="https://cube.elemecdn.com/e/fd/0fc7d20532fdaf769a25683617711png.png" />
        </el-avatar>
        <div id="vp-info">
          <span id="vpname">{{vpname}}</span>发布于
          <span id="vptime">{{vpdate}}</span>
        </div>
        <i class="el-icon-s-comment" @click="addLikes"></i>
        <span id="like-num">{{likeNum}}</span>
        <i class="el-icon-star-on"></i>
        <span id="com-num">{{comNum}}</span>
        <el-avatar id="vp-avatar2" :size="60" src="https://empty" @error="errorHandler">
          <img src="https://cube.elemecdn.com/e/fd/0fc7d20532fdaf769a25683617711png.png" />
        </el-avatar>
        <el-input id="com-con" type="textarea" :rows="2" placeholder="添加评论" v-model="textarea"></el-input>
        <div id="make-com" @click="makeComment">
          <span>发表</span>
        </div>
        <div class="comment-block">
        <!-- <el-card class="comment-block"> -->
          <div v-for="com in comments" :key="com.photoID" class="text item">
            <div class="com-item">
              <el-avatar id="com-avatar" :size="30" src="https://empty" @error="errorHandler">
                <img :src="com.src" />
              </el-avatar>
              <span id="coms-name">{{com.name+": "}}</span>
              <span id="coms-con">{{com.PhotoCommentContent}}</span>
              <el-button id="com-del" type="text" @click="deleteComment(com.PhotoCommentId)">删除</el-button>
            </div>
          </div>
        <!-- </el-card> -->
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Photo",

  data() {
    return {
      fileList: [
        {
          name: "food.jpeg",
          url:
            "https://fuss10.elemecdn.com/3/63/4e7f3a15429bfda99bce42a18cdd1jpeg.jpeg?imageMogr2/thumbnail/360x360/format/webp/quality/100",
        },
        {
          name: "food2.jpeg",
          url:
            "https://fuss10.elemecdn.com/3/63/4e7f3a15429bfda99bce42a18cdd1jpeg.jpeg?imageMogr2/thumbnail/360x360/format/webp/quality/100",
        },
      ],
      rootUrl: "http://139.224.255.43:7779/",
      albumName: "成长相册",
      photoNumber: "0",
      albumTime: "2020.8.12",
      albumIntro: "记录了我成长的点点滴滴",
      photoDate: "2020.8.12",
      photoTime: "21:50",
      vpname: "TEMP",
      vpdate: "2020.8.22",
      likeNum: "233",
      comNum: "233",
      textarea: "",
      deletePhotoId: "",
      ispDelete: false,
      ispDelIn: false,
      isView: false,
      getAjaxComments: 0,
      getAjaxPhotos: 0,
      getAjaxDeletePhoto: 0,
      getAjaxMakeComment: 0,
      getAjaxDeleteComment:0,
      token:
        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ3d3ciLCJqdGkiOiJiMmJmYjg0Ni02MDM5LTQxYjktOWUxNi1jYzRiZGE5ZjI1NmUiLCJleHAiOjE1OTk4Mjc1MDAsImlzcyI6Imh0dHBzOi8vd3d3LmNuYmxvZ3MuY29tL2NoZW5ndGlhbiIsImF1ZCI6Imh0dHBzOi8vd3d3LmNuYmxvZ3MuY29tL2NoZW5ndGlhbiJ9.JWEs1nds7cqVwquIRglaVBrYLHEmfuZAxrsqbrNuYsc",
      photos: [
        {
          photoID: "1",
          src: require("../assets/images/艺术设计.png"),
        },
        {
          photoID: "2",
          src: require("../assets/images/宇宙深空.png"),
        },
        {
          photoID: "3",
          src: require("../assets/images/自然美景.png"),
        },
      ],
      photolist: [],
      comments: [],
    };
  },
  created: function () {
    this.getCommentsList();
    this.getPhotoList();
    this.getQuery();
  },
  methods: {
    getQuery: function () {
      this.albumName = this.$route.query.fromAlbumName;
      this.albumTime = this.$route.query.fromAlbumTime;
      this.albumIntro = this.$route.query.fromAlbumIntro;
    },
    // getCookieForPhoto: function () {
    // var cookie=document.cookie;
    //   var cookieArr=cookie.split(";");
    //   for(var i=0;i<cookieArr.length;i++){
    //     var keyAndValue=cookieArr[i].split("=");
    //     if(keyAndValue[0].trim()=="token"){
    //       return keyAndValue[1];
    //     }
    //   }
    //   for (var i = 0; i < document.cookie.length; i++) {
    //     var keyAndValue = document.cookie.split("=");
    //     if (keyAndValue[0].trim() == "albumID") {
    //       return keyAndValue[1];
    //     }
    //   }
    // },
    addLikes:function(){
      var getAjaxLike=new XMLHttpRequest();
      getAjaxLike.open(
        "POST",
        "http://139.224.255.43:7779//Photo/deletePhotoCommentByID?/Photo/setPhotoLike?=31",
        true
      );
      getAjaxLike.setRequestHeader(
        "Authorization",
        "Bearer " + this.token
      );
      getAjaxLike.onreadystatechange =  function () {
      if (
        getAjaxLike.readyState == 4 &&
        getAjaxLike.status == 200
      ) {
        return;
      }
      };
      getAjaxLike.send();
    },
    deleteComment:function(cid){
      this.getAjaxDeleteComment = new XMLHttpRequest();
      ("");
      this.getAjaxDeleteComment.open(
        "POST",
        "http://139.224.255.43:7779//Photo/deletePhotoCommentByID?commentID="+cid,
        true
      );
      this.getAjaxDeleteComment.setRequestHeader(
        "Authorization",
        "Bearer " + this.token
      );
      this.getAjaxDeleteComment.onreadystatechange = this.getDC;
      this.getAjaxDeleteComment.send();
    },
    getDC: function () {
      if (
        this.getAjaxDeleteComment.readyState == 4 &&
        this.getAjaxDeleteComment.status == 200
      ) {
        this.getCommentsList();
      }
    },
    makeComment: function () {
      this.getAjaxMakeComment = new XMLHttpRequest();
      ("");
      this.getAjaxMakeComment.open(
        "POST",
        "http://139.224.255.43:7779/Photo/createPhotoCommentByID?content=" +
          this.textarea +
          "&photoID=31&userID=3",
        true
      );
      this.getAjaxMakeComment.setRequestHeader(
        "Authorization",
        "Bearer " + this.token
      );
      this.getAjaxMakeComment.onreadystatechange = this.getMC;
      this.getAjaxMakeComment.send();
    },
    getMC: function () {
      if (
        this.getAjaxPhotos.readyState == 4 &&
        this.getAjaxPhotos.status == 200
      ) {
        this.getCommentsList();
      }
    },
    getPhotoList: function () {
      var albumId = this.$route.query.fromAlbum;
      this.getAjaxPhotos = new XMLHttpRequest();
      this.getAjaxPhotos.open(
        "POST",
        "http://139.224.255.43:7779/Photo/getAllPhotoByID?albumID=" +
          albumId +
          "&userID=3",
        true
      );
      this.getAjaxPhotos.setRequestHeader(
        "Authorization",
        "Bearer " + this.token
      );
      this.getAjaxPhotos.onreadystatechange = this.getPL;
      this.getAjaxPhotos.send();
    },
    getPL: function () {
      if (
        this.getAjaxPhotos.readyState == 4 &&
        this.getAjaxPhotos.status == 200
      ) {
        this.photolist = JSON.parse(
          JSON.parse(this.getAjaxPhotos.responseText).photos
        ); // eslint-disable-line no-unused-vars
        for (var i = 0; i < this.photolist.length; i++) {
          this.photolist[i].PhotoUploadTime = this.photolist[
            i
          ].PhotoUploadTime.replace("T", " ");
        }
        this.photoNumber = this.photolist.length;
      }
    },
    getCommentsList: function () {
      this.getAjaxComments = new XMLHttpRequest();
      this.getAjaxComments.open(
        "POST",
        "http://139.224.255.43:7779/Photo/getPhotoCommentByID?photoID=31",
        true
      );
      this.getAjaxComments.setRequestHeader(
        "Authorization",
        "Bearer " + this.token
      );
      this.getAjaxComments.onreadystatechange = this.getCL;
      this.getAjaxComments.send();
    },
    getCL: function () {
      if (
        this.getAjaxComments.readyState == 4 &&
        this.getAjaxComments.status == 200
      ) {
        this.comments = JSON.parse(
          JSON.parse(this.getAjaxComments.responseText).photoComments
        ); // eslint-disable-line no-unused-vars
        //console.log(this.comments);
      }
    },
    errorHandler: function () {
      return true;
    },
    uploadPhoto: function () {
      var albumId = this.$route.query.fromAlbum;
      var getAjaxUploadPhoto = new XMLHttpRequest();
      getAjaxUploadPhoto.open(
        "POST",
        "http://139.224.255.43:7779/Photo/createPhotoByID?albumID=" +
          albumId +
          "&userID=3",
        true
      );
      getAjaxUploadPhoto.setRequestHeader(
        "Authorization",
        "Bearer " + this.token
      );
      getAjaxUploadPhoto.onreadystatechange = function () {
        if (
          getAjaxUploadPhoto.readyState == 4 &&
          getAjaxUploadPhoto.status == 200
        ) {
          return;
        }
      };
      getAjaxUploadPhoto.send();
      return;
    },
    deletePhoto: function () {
      if (this.ispDelete == false) {
        this.$refs.pdelete.innerHTML = "取消";
        this.ispDelete = true;
      } else {
        this.$refs.pdelete.innerHTML = "删除照片";
        this.ispDelete = false;
      }
      return;
    },
    confirmpDelete: function (pid) {
      this.ispDelIn = true;
      this.$refs.cover.style.display = "block";
      this.deletePhotoId = pid;
      return;
    },
    confirmpDeleteIn: function () {
      this.getAjaxDeletePhoto = new XMLHttpRequest();
      this.getAjaxDeletePhoto.open(
        "POST",
        "http://139.224.255.43:7779/Photo/deletePhotoByID?photoID=" +
          this.deletePhotoId,
        true
      );
      this.getAjaxDeletePhoto.setRequestHeader(
        "Authorization",
        "Bearer " + this.token
      );
      this.getAjaxDeletePhoto.onreadystatechange = this.getDP;
      this.getAjaxDeletePhoto.send();
      this.ispDelIn = false;
      this.$refs.cover.style.display = "none";
    },
    getDP: function () {
      if (
        this.getAjaxDeletePhoto.readyState == 4 &&
        this.getAjaxDeletePhoto.status == 200
      ) {
        this.getPhotoList();
      }
    },
    cancelpDeleteIn: function () {
      this.ispDelIn = false;
      this.$refs.cover.style.display = "none";
    },
    view: function () {
      this.isView = true;
      this.$refs.cover.style.display = "block";
    },
    shutdown: function () {
      this.isView = false;
      this.$refs.cover.style.display = "none";
    },
  },
};
</script>

<style>
#Photo {
  position: absolute;
  background-color: rgb(241, 240, 240);
  width: 100%;
  height: 3000px;
}
#album-header {
  position: relative;
  width: 1440px;
  height: 87px;
  left: 0px;
  top: 50px;

  background: #f9f8f8;
  box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
}
#album-header > span {
  position: relative;
  left: -1200px;
  top: 25px;

  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: bold;
  font-size: 24px;
  line-height: 32px;
  display: flex;
  align-items: center;

  color: #756f6f;
}
#return {
  position: relative;
  left: 620px;
  top: 0px;

  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: bold;
  font-size: 24px;
  line-height: 32px;
  display: flex;
  align-items: center;

  color: #756f6f;
}
#menu {
  position: relative;
  width: 513px;
  left: -420px;
  top: -400px;
}
#album-avatar {
  position: absolute;
  width: 132px;
  height: 133px;
  left: 71px;
  top: 533px;

  background: #c4c4c4;
}
#album-info {
  position: relative;
  width: 552px;
  height: 127px;
  left: 57px;
  top: 539px;
  float: left;

  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: normal;
  font-size: 24px;
  line-height: 32px;

  color: #000000;
}
#album-name {
  position: relative;
  left: -30px;
}
#album-time {
  position: relative;
  float: left;
  left: 176px;
  bottom: 10px;
  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: normal;
  font-size: 16px;
  line-height: 32px;

  color: #aca7a7;
}
#album-intro {
  position: relative;
  float: left;
  left: 176px;
  bottom: 23px;
  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: normal;
  font-size: 16px;
  line-height: 32px;

  color: #aca7a7;
}
#upload-photo {
  position: absolute;
  width: 269px;
  height: 42px;
  left: 967px;
  top: 540px;

  background: #ba2222;
  border: 1px solid #9b0a0a;
  box-sizing: border-box;
  border-radius: 6px;
}
#upload-photo > span {
  position: absolute;
  width: 244px;
  height: 21px;
  left: 11px;
  top: 9px;

  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: normal;
  font-size: 18px;
  line-height: 24px;
  text-align: center;

  color: #f0e8e8;
}
#delete-album {
  position: absolute;
  width: 269px;
  height: 42px;
  left: 967px;
  top: 624px;

  background: #f2dede;
  border: 1px solid #c79e9e;
  box-sizing: border-box;
  border-radius: 6px;
}
#delete-album > span {
  position: relative;
  width: 142px;
  height: 24px;
  left: 1px;
  top: 8px;

  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: normal;
  font-size: 18px;
  line-height: 24px;
  /* identical to box height */

  text-align: center;

  color: #000000;
}
#photo-list {
  position: relative;
  width: 1440px;
  height: 1381px;
  top: 320px;
}
.pcard {
  position: relative;
  width: 288px;
  height: 295px;
  top: -50px;
  margin-bottom: 70px;
  margin-left: 85px;
}
.image {
  height: 248px;
  width: 100%;
  display: block;
}
#delete-photo-in {
  position: relative;
  width: 875px;
  height: 254.65px;
  top: -1200px;
  z-index: 3;
  border: 2px solid #b9b4b4;
  box-shadow: 0px 0px 4px 2px darkgray;
  background: #f0eeee;
}
#pd-header {
  width: 875px;
  height: 66.89px;
  left: 299px;
  top: 246px;

  background: #ba2222;
}
#pd-header > span {
  position: absolute;
  width: 222px;
  height: 21.14px;
  left: 30px;
  top: 20px;

  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: bold;
  font-size: 36px;
  line-height: 48px;
  display: flex;
  align-items: center;
  text-align: center;

  color: #f2efef;
}
#pconfirm {
  position: absolute;
  left: 300px;
  top: 135px;

  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: 290;
  font-size: 20px;
  line-height: 24px;
  display: flex;
  align-items: center;

  color: #000000;
}
#pconfirm-button {
  position: absolute;
  width: 137px;
  height: 33px;
  left: 550px;
  top: 200px;

  background: #ba2222;
}
#pconfirm-button > span {
  position: absolute;
  width: 96px;
  height: 20px;
  left: 48px;
  top: 5px;

  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: normal;
  font-size: 18px;
  line-height: 24px;
  display: flex;
  align-items: center;
  text-align: center;

  color: #fffcfc;
}
#pcancelDelete-button {
  position: absolute;
  width: 137px;
  height: 33px;
  left: 710px;
  top: 200px;

  background: #e5d4d4;
}
#pcancelDelete-button > span {
  position: absolute;
  width: 96px;
  height: 20px;
  left: 48px;
  top: 5px;

  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: normal;
  font-size: 18px;
  line-height: 24px;
  display: flex;
  align-items: center;
  text-align: center;

  color: #535151;
}

.out-block {
  position: relative;
  width: 684px;
  height: 664px;
  left: -440px;
  top: 185px;

  background: rgb(82, 78, 78);
}
.block {
  position: relative;
  top: 50px;
  width: 628px;
}
#photo-description {
  position: relative;
  left: -200px;
  top: 30px;
  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: normal;
  font-size: 18px;
  line-height: 24px;

  color: #fffefe;
}
.right-block {
  position: relative;
  width: 460px;
  height: 664px;
  left: 132px;
  top: -479px;
  background: rgb(231, 230, 230);
}
#vp-avatar {
  position: relative;
  left: -160px;
  top: 40px;
}
#vp-info {
  position: relative;
  left: 2px;
  top: -8px;

  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: normal;
  font-size: 18px;
  line-height: 24px;

  color: #000000;
}
#vpname {
  margin-right: 20px;
}
#vptime {
  margin-left: 5px;
  color: rgb(197, 197, 197);
}
.el-icon-star-on {
  position: relative;
  font-size: 35px;
  top: 20px;
  left: -120px;
}
.el-icon-s-comment {
  position: relative;
  font-size: 30px;
  left: 110px;
  top: 20px;
}
#like-num {
  position: relative;
  left: -45px;
  top: 15px;
  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: normal;
  font-size: 18px;
  line-height: 24px;

  color: #000000;
}
#com-num {
  position: relative;
  left: 60px;
  top: 15px;
  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: normal;
  font-size: 18px;
  line-height: 24px;

  color: #000000;
}
#vp-avatar2 {
  position: relative;
  left: -210px;
  top: 130px;
}
#com-con {
  position: relative;
  width: 267px;
  height: 85px;
  left: 40px;
  top: 75px;

  background: #f9f4f4;
  border: 1px solid #d8cece;
  box-sizing: border-box;
}
#make-com {
  position: relative;
  width: 122px;
  height: 31px;
  left: 155px;
  top: 105px;
  cursor: pointer;

  background: #da4646;
  border-radius: 4px;
}
#make-com > span {
  position: relative;
  width: 107px;
  height: 19px;
  left: 37px;
  top: 6px;

  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: normal;
  font-size: 18px;
  line-height: 24px;
  display: flex;
  align-items: center;
  text-align: center;

  color: #f9f5f5;
}
.text {
  font-size: 14px;
}

.item {
  padding: 3px 0;
}
.com-item {
  height: 48px;
  background: #ffffff;
  box-shadow: 0px 2px 1px rgba(0, 0, 0, 0.25);
}
.comment-block {
  position: relative;
  top: 140px;
  width: 450px;
  height:245px;
  overflow: auto;
}
#com-avatar {
  float: left;
  position: relative;
  top: 2px;
  left: 5px;
}
#coms-name {
  float: left;
  position: relative;
  top: 8px;
  left: 10px;
}
#coms-con {
  float: left;
  position: relative;
  left: 20px;
  top: 7px;
  width: 280px;
  height: 40px;
	overflow: auto;
  word-wrap: break-word;
  text-align: left;
  color: #7c7777;
}
#com-del {
  float: right;
  position: relative;
  top: 18px;
}
#view-photo {
  position: relative;
  z-index: 3;
  left: 200px;
  top: -1850px;
}
</style>
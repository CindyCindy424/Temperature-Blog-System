<template>
  <div id="yyw-Photo">
    <img id="yyw-banner" src="../assets/images/banner.png" />
    <div id="yyw-album-header">
      <span id="yyw-album-return">
        <router-link to="/Album" id="yyw-return">相册 ></router-link>
        <span style="position:relative; left:650px">{{albumName}}</span>
      </span>
    </div>
    <div id="yyw-menu">
      <!-- <el-avatar
        id="album-avatar"
        shape="square"
        :size="135"
        :src="albumCover"
        @error="errorHandler"
        :fit="contain"
      >-->
      <img id="yyw-album-avatar" shape="square" :size="135" :src="albumCover" object-fit="contain" />
      <!-- <img src="https://cube.elemecdn.com/e/fd/0fc7d20532fdaf769a25683617711png.png" />
      </el-avatar>-->
      <div id="yyw-album-info">
        <span id="yyw-album-name">{{albumName}} /共{{photoNumber}}张</span>
        <br />
        <br />
        <span id="yyw-album-time">{{albumTime}}</span>
        <br />
        <br />
        <span id="yyw-album-intro">{{albumIntro}}</span>
      </div>

      <el-upload
        class="yyw-upload-demo"
        ref="upload"
        action="https://jsonplaceholder.typicode.com/posts/"
        :file-list="fileList"
        :auto-upload="false"
        :limit="1"
        style="position:absolute;top:-2000px"
      >
        <div
          id="yyw-upload-photo"
          style="cursor:pointer;"
          slot="trigger"
          v-if="isUser"
          @click="isUpload=true"
        >
          <span>上传图片/视频</span>
        </div>
        <!-- <el-button class="upload-photo" slot="trigger" size="small" @click="isUpload=true">选择图片进行上传</el-button> -->
        <el-button
          class="yyw-confirm-upload-photo"
          size="small"
          type="success"
          @click="submitUpload();isUpload=false"
          v-if="isUpload"
        >确认上传</el-button>
      </el-upload>
      <br />
      <div id="yyw-delete-album" v-on:click="deletePhoto" style="cursor:pointer;" v-if="isUser">
        <span ref="pdelete">删除图片/视频</span>
      </div>
    </div>
    <div id="yyw-photo-list">
      <el-row>
        <el-col
          :span="8"
          v-for="(photo, index) in photolist"
          :key="photo.PhotoId"
          :offset="index > 0 ? 0 : 0"
        >
          <img
            src="../assets/images/close.png"
            v-if="ispDelete"
            style="position:relative; left:132px; top:-32px; width:20px; height:20px; z-index:3; cursor:pointer;"
            @click="confirmpDelete(photo.PhotoId)"
          />
          <el-card :body-style="{padding:'0px'}" class="yyw-pcard">

            <el-dialog title="提示" :visible.sync="ispDelIn" width="30%">
              <span>您确定要删除照片吗？</span>
              <span slot="footer" class="yyw-dialog-footer">
                <el-button type="primary" @click="ispDelIn = false;confirmpDeleteIn()">确 定</el-button>
                <el-button @click="ispDelIn = false">取 消</el-button>
              </span>
            </el-dialog>
            <img
              :src="rootUrl+photo.PhotoAddress"

              class="yyw-image"
              style="cursor: pointer;"
              @click="view(index,photo.PhotoId)"
            />
            <div style="padding:19px;border: 1px solid #B9B4B4;background:#f3f0f0;">
              <span r style="cursor: pointer;" @click="view(index,photo.PhotoId)">
                {{photo.PhotoUploadTime}}
                <!-- <span style="margin-left: 30px;" @click="view">{{photoTime}}</span> -->
              </span>
            </div>
          </el-card>
        </el-col>
      </el-row>
      <div class="yyw-page-block-p">
        <el-pagination
          layout="prev, pager, next"
          :total="photoNumber"
          :page-size="9"
          :current-page.sync="pageNumP"
          @current-change="handleCurrentPage"
          @prev-click="handleCurrentPagePre"
          @next-click="handleCurrentPageNext"
          hide-on-single-page="true"
        ></el-pagination>
      </div>
    </div>
    <!-- <div id="cover" ref="cover"></div>
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
    </div>-->
    <div id="yyw-view-photo" v-if="isView">
      <div class="yyw-out-block">
        <div class="yyw-block">
          <el-carousel
            height="580px"
            :autoplay="false"
            :initial-index="photoIndex"
            @change="change"
          >
            <el-carousel-item v-for="item in photos" :key="item.PhotoId">
              <el-image
                style="width: 100%; height: 100%;"
                :src="rootUrl+item.PhotoAddress"
                object-fit="contain"
              ></el-image>
            </el-carousel-item>
          </el-carousel>
        </div>
      </div>
      <div class="yyw-right-block">
        <i
        class="el-icon-close"
          style="position:relative; left:235px; top:-25px; font-size:25px; color:red; z-index:3; cursor:pointer"
          v-if="isView"
          @click="shutdown"
        ></i>
        <el-avatar id="yyw-vp-avatar" :size="60" :src="vpavatar" @error="errorHandler">
          <img src="https://cube.elemecdn.com/e/fd/0fc7d20532fdaf769a25683617711png.png" />
        </el-avatar>
        <div id="yyw-vp-info">
          <span id="yyw-vpname">{{vpname}}</span>发布于
          <span id="yyw-vptime">{{vpdate}}</span>
        </div>
        <!-- <i class="icon-like" @click="addLikes" style="cursor:pointer"></i> -->
        <img
          src="../assets/images/beforelike.png"
          class="yyw-icon-like"
          @click="addLikes"
          style="cursor:pointer"
          ref="likes"
        />
        <img
          src="../assets/images/like.png"
          class="yyw-icon-alreadylike"
          style="cursor:default"
          ref="alreadylikes"
          v-if="islike"
        />
        <span id="yyw-like-num">{{likeNum}}</span>
        <img
          src="../assets/images/com.png"
          class="yyw-icon-com"
          style="position: relative;font-size: 30px;left: 45px;top: 20px;"
        />
        <span id="yyw-com-num">{{comNum}}</span>
        <el-avatar id="yyw-vp-avatar2" :size="60" :src="vpavatar" @error="errorHandler">
          <img src="https://cube.elemecdn.com/e/fd/0fc7d20532fdaf769a25683617711png.png" />
        </el-avatar>
        <el-input id="yyw-com-con" type="textarea" :rows="2" placeholder="添加评论" v-model="textarea"></el-input>
        <div id="yyw-make-com" @click="makeComment">
          <span>发表</span>
        </div>
        <div class="yyw-comment-block">
          <!-- <el-card class="comment-block"> -->
          <div v-for="com in comments" :key="com.photoID" >
            <div class="yyw-com-item">
              <el-avatar id="yyw-com-avatar" :size="30" :src="rootUrl+com.avatar" @error="errorHandler">
                <img src="https://cube.elemecdn.com/e/fd/0fc7d20532fdaf769a25683617711png.png" />
              </el-avatar>
              <span id="yyw-coms-name">{{com.userName+": "}}</span>
              <span id="yyw-coms-con">{{com.photoCommentContent}}</span>
              <el-button
                id="yyw-com-del"
                type="text"
                @click="deleteComment(com.photoCommentID)"
                :v-if="isUser"
              >删除</el-button>
            </div>
          </div>
          <!-- </el-card> -->
        </div>
      </div>
    </div>
    <p id="yyw-pfooter">Copyright ©2020 Temperature team, All Rights Reserved.</p>
  </div>
</template>

<script>
export default {
  name: 'Photo',

  data () {
    return {
      fileList: [],
      rootUrl: 'http://139.224.255.43:7779/',
      albumName: '成长相册',
      photoNumber: '0',
      albumTime: '2020.8.12',
      albumIntro: '记录了我成长的点点滴滴',
      albumId: '',
      albumCover: '',
      photoDate: '2020.8.12',
      photoTime: '21:50',
      pageNumP: 1,
      vpname: 'TEMP',
      vpdate: '2020.8.22',
      vpavatar: '',
      currentPhotoId: '44',
      likeNum: '233',
      comNum: '233',
      textarea: '',
      deletePhotoId: '',
      photoIndex: '',
      userID: '3',
      islike: true,
      ispDelete: false,
      ispDelIn: false,
      isView: false,
      isUser: true,
      isUpload: false,
      getAjaxComments: 0,
      getAjaxPhotos: 0,
      getAjaxDeletePhoto: 0,
      getAjaxMakeComment: 0,
      getAjaxDeleteComment: 0,
      getAjaxPhotoInfo: 0,
      getAjaxLike: 0,
      getAjaxIfAdd: 0,
      token:
        'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ3d3ciLCJqdGkiOiI3NjE2MzZmMS01ZjUzLTQwNTMtYTNjNy1kOGZmN2Y3N2ViM2UiLCJleHAiOjE2MDAzNDczMjQsImlzcyI6Imh0dHBzOi8vd3d3LmNuYmxvZ3MuY29tL2NoZW5ndGlhbiIsImF1ZCI6Imh0dHBzOi8vd3d3LmNuYmxvZ3MuY29tL2NoZW5ndGlhbiJ9.8YbTxW9Ae_xt1d2cDTL5TpXDxYIx3VclkhYwQqw4450',
      photos: [],
      photolist: [],
      comments: []
    }
  },
  created: function () {
    this.getQuery()
    this.getPhotoNum()
    this.getPhotoList()
  },
  methods: {
    getQuery: function () {
      this.albumName = this.$route.query.fromAlbumName
      this.albumTime = this.$route.query.fromAlbumTime
      this.albumIntro = this.$route.query.fromAlbumIntro
      this.albumId = this.$route.query.fromAlbum
      this.albumCover = this.$route.query.fromAlbumCover
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

    change: function (aim, origin) {
      console.log(origin)
      this.currentPhotoId = this.photos[aim].PhotoId
      this.getPhotoInfo()
      this.getCommentsList()
      this.ifadd()
    },
    handleCurrentPage: function () {
      // console.log(this.pageNumP);
      this.getPhotoList()
    },
    handleCurrentPagePre: function () {
      // console.log(this.pageNumP);
      this.pageNumP--
      this.getPhotoList()
    },
    handleCurrentPageNext: function () {
      // console.log(this.pageNumP);
      this.pageNumP++
      this.getPhotoList()
    },
    ifadd: function () {
      this.getAjaxIfAdd = new XMLHttpRequest()
      this.getAjaxIfAdd.open(
        'GET',
        'http://139.224.255.43:7779/Photo/checkPhotoLike?photoID=' +
          this.currentPhotoId +
          '&userID=' + this.userID,
        true
      )
      this.getAjaxIfAdd.setRequestHeader(
        'Authorization',
        'Bearer ' + this.token
      )
      this.getAjaxIfAdd.onreadystatechange = this.getIF
      this.getAjaxIfAdd.send()
    },
    getIF: function () {
      if (
        this.getAjaxIfAdd.readyState === 4 &&
        this.getAjaxIfAdd.status === 200
      ) {
        var receive = JSON.parse(this.getAjaxIfAdd.responseText).hasSetLike
        if (receive === 1) {
          // this.$refs.alreadylikes.style.cursor = "default";
          this.islike = true
        } else {
          // this.$refs.alreadylikes.style.cursor = "pointer";
          this.islike = false
        }
      }
    },
    addLikes: function () {
      this.getAjaxLike = new XMLHttpRequest()
      this.getAjaxLike.open(
        'GET',
        'http://139.224.255.43:7779/Photo/setPhotoLike?photoID=' +
          this.currentPhotoId +
          '&userID=' + this.userID,
        true
      )
      this.getAjaxLike.setRequestHeader(
        'Authorization',
        'Bearer ' + this.token
      )
      this.getAjaxLike.onreadystatechange = this.getL
      this.getAjaxLike.send()
    },
    getL: function () {
      if (this.getAjaxLike.readyState === 4 && this.getAjaxLike.status === 200) {
        // this.$refs.alreadylikes.style.cursor = "default";
        this.islike = true
        this.getPhotoInfo()
      }
    },
    deleteComment: function (cid) {
      this.getAjaxDeleteComment = new XMLHttpRequest()
      this.getAjaxDeleteComment.open(
        'POST',
        'http://139.224.255.43:7779//Photo/deletePhotoCommentByID?commentID=' +
          cid,
        true
      )
      this.getAjaxDeleteComment.setRequestHeader(
        'Authorization',
        'Bearer ' + this.token
      )
      this.getAjaxDeleteComment.onreadystatechange = this.getDC
      this.getAjaxDeleteComment.send()
    },
    getDC: function () {
      if (
        this.getAjaxDeleteComment.readyState === 4 &&
        this.getAjaxDeleteComment.status === 200
      ) {
        this.getCommentsList()
      }
    },
    makeComment: function () {
      this.getAjaxMakeComment = new XMLHttpRequest()
      this.getAjaxMakeComment.open(
        'POST',
        'http://139.224.255.43:7779/Photo/createPhotoCommentByID?content=' +
          this.textarea +
          '&photoID=' +
          this.currentPhotoId +
          '&userID=' + this.userID,
        true
      )
      this.getAjaxMakeComment.setRequestHeader(
        'Authorization',
        'Bearer ' + this.token
      )
      this.getAjaxMakeComment.onreadystatechange = this.getMC
      this.getAjaxMakeComment.send()
    },
    getMC: function () {
      if (
        this.getAjaxPhotos.readyState === 4 &&
        this.getAjaxPhotos.status === 200
      ) {
        this.getPhotoInfo()
        this.getCommentsList()
      }
    },
    getPhotoNum: function () {
      this.getAjaxPhotoNum = new XMLHttpRequest()
      this.getAjaxPhotoNum.open(
        'POST',
        'http://139.224.255.43:7779/Photo/getAllPhotoByPage?albumID=' +
          this.albumId +
          '&userID=' + this.userID + '&pageNum=1&pageSize=9999',
        true
      )
      this.getAjaxPhotoNum.setRequestHeader(
        'Authorization',
        'Bearer ' + this.token
      )
      this.getAjaxPhotoNum.onreadystatechange = this.getPN
      this.getAjaxPhotoNum.send()
    },
    getPN: function () {
      if (
        this.getAjaxPhotoNum.readyState === 4 &&
        this.getAjaxPhotoNum.status === 200
      ) {
        var receive = JSON.parse(
          JSON.parse(this.getAjaxPhotoNum.responseText).photos
        ) // eslint-disable-line no-unused-vars
        this.photoNumber = receive.length
      }
    },
    getPhotoList: function () {
      this.getAjaxPhotos = new XMLHttpRequest()
      this.getAjaxPhotos.open(
        'POST',
        'http://139.224.255.43:7779/Photo/getAllPhotoByPage?albumID=' +
          this.albumId +
          '&userID=' + this.userID + '&pageNum=' +
          this.pageNumP +
          '&pageSize=9',
        true
      )

      this.getAjaxPhotos.setRequestHeader(
        'Authorization',
        'Bearer ' + this.token
      )
      this.getAjaxPhotos.onreadystatechange = this.getPL
      this.getAjaxPhotos.send()
    },
    getPL: function () {
      if (
        this.getAjaxPhotos.readyState === 4 &&
        this.getAjaxPhotos.status === 200
      ) {
        this.photolist = JSON.parse(
          JSON.parse(this.getAjaxPhotos.responseText).photos
        ) // eslint-disable-line no-unused-vars
        this.photos = this.photolist
        for (var i = 0; i < this.photolist.length; i++) {
          this.photolist[i].PhotoUploadTime = this.photolist[i].PhotoUploadTime.replace('T', ' ')
        }
      }
    },
    getCommentsList: function () {
      this.getAjaxComments = new XMLHttpRequest()
      this.getAjaxComments.open(
        'POST',
        'http://139.224.255.43:7779/Photo/getPhotoCommentByID?photoID=' +
          this.currentPhotoId,
        true
      )
      this.getAjaxComments.setRequestHeader(
        'Authorization',
        'Bearer ' + this.token
      )
      this.getAjaxComments.onreadystatechange = this.getCL
      this.getAjaxComments.send()
    },
    getCL: function () {
      if (
        this.getAjaxComments.readyState === 4 &&
        this.getAjaxComments.status === 200
      ) {
        this.comments = JSON.parse(
          JSON.parse(this.getAjaxComments.responseText).photoComments
        ) // eslint-disable-line no-unused-vars
        // console.log(this.comments);
      }
    },
    errorHandler: function () {
      return true
    },
    submitUpload: function () {
      var fileValue = document.querySelector('.el-upload .el-upload__input')
      var fd = new window.FormData()
      fd.append('uploadedPhoto', fileValue.files[0])
      var getAjaxUploadPhoto = new XMLHttpRequest()
      getAjaxUploadPhoto.open(
        'POST',
        'http://139.224.255.43:7779/Photo/createPhotoByID?albumID=' +
          this.albumId +
          '&userID=' + this.userID,
        true
      )
      getAjaxUploadPhoto.setRequestHeader(
        'Authorization',
        'Bearer ' + this.token
      )
      getAjaxUploadPhoto.send(fd)
      getAjaxUploadPhoto.onload = () => {
        if (getAjaxUploadPhoto.status === 200) {
          var imgurl =
            this.rootUrl +
            JSON.parse(JSON.parse(getAjaxUploadPhoto.responseText).uploadPaths)
          this.getPhotoNum()
          this.pageNumP = Math.ceil(this.photoNumber / 9)
          if (this.pageNumP === 0) this.pageNumP = 1
          if (this.photoNumber === 0) this.albumCover = imgurl
          this.getPhotoList()
        }
      }
    },
    deletePhoto: function () {
      if (this.ispDelete === false) {
        this.$refs.pdelete.innerHTML = '取消'
        this.ispDelete = true
      } else {
        this.$refs.pdelete.innerHTML = '删除照片'
        this.ispDelete = false
      }
    },
    confirmpDelete: function (pid) {
      this.ispDelIn = true
      // this.$refs.cover.style.display = "block";
      this.deletePhotoId = pid
    },
    confirmpDeleteIn: function () {
      this.getAjaxDeletePhoto = new XMLHttpRequest()
      this.getAjaxDeletePhoto.open(
        'POST',
        'http://139.224.255.43:7779/Photo/deletePhotoByID?photoID=' +
          this.deletePhotoId,
        true
      )
      this.getAjaxDeletePhoto.setRequestHeader(
        'Authorization',
        'Bearer ' + this.token
      )
      this.getAjaxDeletePhoto.onreadystatechange = this.getDP
      this.getAjaxDeletePhoto.send()
      this.ispDelIn = false
      // this.$refs.cover.style.display = "none";
    },
    getDP: function () {
      if (
        this.getAjaxDeletePhoto.readyState === 4 &&
        this.getAjaxDeletePhoto.status === 200
      ) {
        this.handleCurrentPage()
        this.getPhotoNum()
        this.getPhotoList()
      }
    },
    cancelpDeleteIn: function () {
      this.ispDelIn = false
      // this.$refs.cover.style.display = "none";
    },
    view: function (e, a) {
      this.photoIndex = e
      this.currentPhotoId = a
      this.isView = true
      // this.$refs.cover.style.display = "block";
      this.getPhotoInfo()
      this.getCommentsList()
      this.ifadd()
    },
    getPhotoInfo: function () {
      this.getAjaxPhotoInfo = new XMLHttpRequest()
      this.getAjaxPhotoInfo.open(
        'POST',
        'http://139.224.255.43:7779/Photo/getPhotoDetail?photoID=' +
          this.currentPhotoId,
        true
      )
      this.getAjaxPhotoInfo.setRequestHeader(
        'Authorization',
        'Bearer ' + this.token
      )
      this.getAjaxPhotoInfo.onreadystatechange = this.getPI
      this.getAjaxPhotoInfo.send()
    },
    getPI: function () {
      if (
        this.getAjaxPhotoInfo.readyState === 4 &&
        this.getAjaxPhotoInfo.status === 200
      ) {
        var receive = JSON.parse(this.getAjaxPhotoInfo.responseText)
          .photoDetail // eslint-disable-line no-unused-vars
        this.vpname = receive.userName
        this.vpdate = receive.photoUploadTime.split('T')[0]
        this.likeNum = receive.photoLikes
        this.comNum = receive.commentNums
        this.vpavatar = this.rootUrl + receive.avatar
      }
    },
    shutdown: function () {
      this.isView = false
      // this.$refs.cover.style.display = "none";
    }
  }
}
</script>

<style>
/* * {
  margin: 0 auto;
} */
#yyw-Photo {
  position: absolute;
  background-color: rgb(241, 240, 240);
  width: 100%;
  height: 2300px;
}
#yyw-album-header {
  margin: 0 auto;
  position: relative;
  width: 1440px;
  height: 87px;
  left: 0px;
  top: 50px;

  background: #f9f8f8;
  box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
}
#yyw-album-return {
  position: relative;
  left: -570px;
  top: 28px;

  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: bold;
  font-size: 24px;
  line-height: 32px;
  display: flex;
  align-items: center;

  color: #756f6f;
}
#yyw-return {
  position: relative;
  left: 620px;
  top: 0px;
  text-decoration:underline;

  font-family: Microsoft YaHei;
  font-style: normal;
  font-weight: bold;
  font-size: 24px;
  line-height: 32px;
  display: flex;
  align-items: center;

  color: #756f6f;
}
#yyw-menu {
  margin: 0 auto;
  position: relative;
  width: 513px;
  left: -420px;
  top: -400px;
}
#yyw-album-avatar {
  position: absolute;
  width: 132px;
  height: 133px;
  left: 71px;
  top: 533px;

  background: #c4c4c4;
}
#yyw-album-info {
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
#yyw-album-name {
  position: relative;
  left: -30px;
}
#yyw-album-time {
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
#yyw-album-intro {
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
#yyw-upload-photo {
  position: absolute;
  width: 269px;
  height: 42px;
  left: 967px;
  top: 2540px;

  background: #ba2222;
  border: 1px solid #9b0a0a;
  box-sizing: border-box;
  border-radius: 6px;
}
.yyw-confirm-upload-photo {
  position: absolute;
  width: 269px;
  height: 42px;
  left: 967px;
  top: 2540px;
  font-size: 18px;

  background: #ba2222;
  border: 1px solid #9b0a0a;
  box-sizing: border-box;
  border-radius: 6px;
}
.yyw-confirm-upload-photo > span {
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
#yyw-upload-photo > span {
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
#yyw-delete-album {
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
#yyw-delete-album > span {
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
#yyw-photo-list {
  margin: 0 auto;
  position: relative;
  width: 1440px;
  height: 1381px;
  top: 320px;
}
.yyw-pcard {
  position: relative;
  width: 288px;
  height: 295px;
  top: -50px;
  margin-bottom: 70px;
  margin-left: 85px;
}
.yyw-image {
  height: 248px;
  width: 100%;
  display: block;
}
.yyw-out-block {
    margin: 0 auto;
  position: relative;
  width: 684px;
  height: 664px;
  left: -440px;
  top: 185px;

  background: rgba(82, 78, 78, 0.705);
}
.yyw-block {
  margin: 0 auto;
  position: relative;
  top: 40px;
  width: 628px;
}
.yyw-right-block {
    margin: 0 auto;
  position: relative;
  width: 460px;
  height: 664px;
  left: 132px;
  top: -479px;
  background: rgb(231, 230, 230);
}
#yyw-vp-avatar {
  position: relative;
  left: -175px;
  top: 40px;
}
#yyw-vp-info {
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
#yyw-vpname {
  margin-right: 20px;
}
#yyw-vptime {
  margin-left: 5px;
  color: rgb(168, 166, 166);
}
.yyw-icon-like {
  position: relative;
  width: 25px;
  height: 25px;
  top: 22px;
  left: -60px;
}
.yyw-icon-alreadylike {
  position: absolute;
  width: 25px;
  height: 25px;
  top: 146.5px;
  left: 104.1px;
}
.yyw-icon-com{
  position: relative;
  width: 25px;
  height: 25px;
  top: 20px;
  left: -60px;
}
#yyw-like-num {
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
#yyw-com-num {
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
#yyw-vp-avatar2 {
  position: relative;
  left: -210px;
  top: 130px;
}
#yyw-com-con {
  position: relative;
  width: 267px;
  height: 85px;
  left: 110px;
  top: 75px;

  background: #f9f4f4;
  border: 1px solid #d8cece;
  box-sizing: border-box;
}
#yyw-make-com {
  position: relative;
  width: 122px;
  height: 31px;
  left: 295px;
  top: 105px;
  cursor: pointer;

  background: #da4646;
  border-radius: 4px;
}
#yyw-make-com > span {
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
.yyw-text {
  font-size: 14px;
}

.yyw-item {
  padding: 3px 0;
}
.yyw-com-item {
  padding: 3px 0;
  font-size: 14px;
  margin-bottom:5px;
  height: 48px;
  background: #ffffff;
  box-shadow: 0px 2px 1px rgba(0, 0, 0, 0.25);
}
.yyw-comment-block {
  position: relative;
  top: 140px;
  width: 450px;
  height: 245px;
  overflow: auto;
}
#yyw-com-avatar {
  float: left;
  position: relative;
  top: 2px;
  left: 5px;
}
#yyw-coms-name {
  float: left;
  position: relative;
  top: 8px;
  left: 10px;
}
#yyw-coms-con {
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
#yyw-com-del {
  float: right;
  position: relative;
  top: 18px;
}
#yyw-view-photo {
  position: relative;
  z-index: 3;
  left: 190px;
  top: -1300px;
}
.yyw-page-block-p {
  position: relative;
  left: -13px;
}
a {
  text-decoration: none;
}
.router-link-active {
  text-decoration: none;
}
#yyw-pfooter{
  margin:0 auto;
  position: relative;
  top:280px;
}
</style>
